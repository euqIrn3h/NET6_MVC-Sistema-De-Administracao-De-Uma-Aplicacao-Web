using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Data;
using Admin_MVC.Models;
using AutoMapper;
using Admin_MVC.Business.Dtos;
using CryptSharp;

namespace Admin_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
              return _context.Usuarios != null ? 
                          View(await _context.Usuarios.ToListAsync()) :
                          Problem("Entity set 'DataContext.Usuarios'  is null.");
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarioData = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioData == null)
            {
                return NotFound();
            }

            return View(usuarioData);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            var usuario = new UsuarioDto();
            return View(usuario);
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Email,Senha,Nome,NivelAcesso")] UsuarioDto usuarioDto)
        {
            if (String.IsNullOrEmpty(usuarioDto.Senha))
                ModelState.AddModelError("Senha", "Senha não poe estar vazia !!!");

            if ( _context.Usuarios.Where(x => x.Email == usuarioDto.Email).FirstOrDefault() != null)
                ModelState.AddModelError("Email", "Email já cadastrado !!!");

            if (ModelState.IsValid)
            {
                var usuario =_mapper.Map<UsuarioData>(usuarioDto);
                usuario.Senha = Crypter.MD5.Crypt(usuario.Senha);

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioDto);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarioData = await _context.Usuarios.FindAsync(id);
            
            if (usuarioData == null)
            {
                return NotFound();
            }
            var usuarioDto = _mapper.Map<UsuarioDto>(usuarioData);

            return View(usuarioDto);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Senha,Nome,Descricao,NivelAcesso")] UsuarioDto usuarioDto)
        {
            if (String.IsNullOrEmpty(usuarioDto.Senha))
                ModelState.AddModelError("Senha", "Senha não poe estar vazia !!!");

            //Verificando que o email não pertence a outro usuario
            var emailUsuarios = _context.Usuarios.Where(x => x.Email == usuarioDto.Email).AsNoTracking().FirstOrDefault();
            if (emailUsuarios != null && emailUsuarios.Id != id)
                ModelState.AddModelError("Email", "Email já cadastrado !!!");

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _mapper.Map<UsuarioData>(usuarioDto);
                    usuario.Senha = Crypter.MD5.Crypt(usuario.Senha);

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioDataExists(usuarioDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioDto);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarioData = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioData == null)
            {
                return NotFound();
            }

            return View(usuarioData);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DataContext.Usuarios'  is null.");
            }
            var usuarioData = await _context.Usuarios.FindAsync(id);
            if (usuarioData != null)
            {
                _context.Usuarios.Remove(usuarioData);
                var listaProdutosUsuario = _context.Produtos.Where(x => x.IdUsuario == id).ToList();
                foreach(var produto in listaProdutosUsuario)
                {
                    _context.Produtos.Remove(produto);
                    var fileNamePrimario = $"FotoP-{produto.IdUsuario}-{produto.Nome}.png";
                    var fileNameSecundario = $"FotoS-{produto.IdUsuario}-{produto.Nome}.png";
                    var filePathPrimario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNamePrimario);
                    var filePathSecundario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNameSecundario);

                    System.IO.File.Delete(filePathPrimario);
                    System.IO.File.Delete(filePathSecundario);
                }
                var fileName = $"FotoU-{usuarioData.Id}.png";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);

                System.IO.File.Delete(filePath);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioDataExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
