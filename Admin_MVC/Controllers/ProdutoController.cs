using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Data;
using Admin_MVC.Models;
using Admin_MVC.Business.Dtos;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Admin_MVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProdutoController(DataContext context, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Produto
        public IActionResult Index(int id)
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return _context.Produtos != null ?
                        View(_context.Produtos.Where(x => x.IdUsuario == id).ToList()) :
                        Problem("Entity set 'DataContext.Produtos'  is null.");
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produtoData = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoData == null)
            {
                return NotFound();
            }

            return View(produtoData);
        }

        // GET: Produto/Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            var produto = new ProdutoDto();
            produto.IdUsuario = id;

            //Caregando Categorias
            var categorias = _context.Categorias
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();
            ViewBag.Categorias = categorias;

            return View(produto);
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,IdUsuario,idCategoria,Valor,linkShoppe,FotoPrimaria,FotoSecundaria")] ProdutoDto produtoDto)
        {
            //Caregando Categorias
            var categorias = _context.Categorias
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();
            ViewBag.Categorias = categorias;

            if (ModelState.IsValid)
            {
                var fileNamePrimario = $"FotoP-{produtoDto.IdUsuario}-{produtoDto.Nome}.png";
                var fileNameSecundario = $"FotoS-{produtoDto.IdUsuario}-{produtoDto.Nome}.png";
                var filePathPrimario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNamePrimario);
                var filePathSecundario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNameSecundario);
                using (var fileStream = new FileStream(filePathPrimario, FileMode.Create))
                {
                    await produtoDto.FotoPrimaria.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(filePathSecundario, FileMode.Create))
                {
                    await produtoDto.FotoSecundaria.CopyToAsync(fileStream);
                }

                var produto = new ProdutoData {
                    Nome = produtoDto.Nome,
                    IdUsuario = produtoDto.IdUsuario,
                    idCategoria = produtoDto.idCategoria,
                    Valor = Convert.ToDecimal(produtoDto.Valor),
                    linkShoppe = produtoDto.linkShoppe,
                    PathFotoPrimaria = "http://empoderadas.softadworks.com/admin/images/" + fileNamePrimario,
                    PathFotoSecundaria = "http://empoderadas.softadworks.com/admin/images/" + fileNameSecundario
                };

                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new {id = produtoDto.IdUsuario });
            }
            return View(produtoDto);
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produtoData = await _context.Produtos.FindAsync(id);
            if (produtoData == null)
            {
                return NotFound();
            }

            var produto = _mapper.Map<ProdutoDto>(produtoData);

            //Caregando Categorias
            var categorias = _context.Categorias
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.Categorias = categorias;

            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,Nome,idCategoria,Valor,linkShoppe,FotoPrimaria,FotoSecundaria")] ProdutoDto produtoDto)
        {
            if (id != produtoDto.Id)
            {
                return NotFound();
            }

            //Caregando Categorias
            var categorias = _context.Categorias
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.Categorias = categorias;


            if (ModelState.IsValid)
            {
                try
                {
                    var fileNamePrimario = $"FotoP-{produtoDto.IdUsuario}-{produtoDto.Nome}.png";
                    var fileNameSecundario = $"FotoS-{produtoDto.IdUsuario}-{produtoDto.Nome}.png";
                    var filePathPrimario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNamePrimario);
                    var filePathSecundario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNameSecundario);
                    using (var fileStream = new FileStream(filePathPrimario, FileMode.Create))
                    {
                        await produtoDto.FotoPrimaria.CopyToAsync(fileStream);
                    }
                    using (var fileStream = new FileStream(filePathSecundario, FileMode.Create))
                    {
                        await produtoDto.FotoSecundaria.CopyToAsync(fileStream);
                    }

                    var produto = new ProdutoData
                    {
                        Id = produtoDto.Id,
                        Nome = produtoDto.Nome,
                        IdUsuario = produtoDto.IdUsuario,
                        idCategoria = produtoDto.idCategoria,
                        Valor = Convert.ToDecimal(produtoDto.Valor),
                        linkShoppe = produtoDto.linkShoppe,
                        PathFotoPrimaria = "http://empoderadas.softadworks.com/admin/images/" + fileNamePrimario,
                        PathFotoSecundaria = "http://empoderadas.softadworks.com/admin/images/" + fileNameSecundario
                    };

                    _context.Update(produto);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoDataExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {id = produtoDto.IdUsuario});
            }
            return View(produtoDto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produtoData = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoData == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = _context.Categorias.ToList();

            return View(produtoData);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'DataContext.Produtos'  is null.");
            }
            var produtoData = await _context.Produtos.FindAsync(id);
            if (produtoData != null)
            {
                _context.Produtos.Remove(produtoData);
            }

            var fileNamePrimario = $"FotoP-{produtoData.IdUsuario}-{produtoData.Nome}.png";
            var fileNameSecundario = $"FotoS-{produtoData.IdUsuario}-{produtoData.Nome}.png";
            var filePathPrimario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNamePrimario);
            var filePathSecundario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileNameSecundario);
            
            System.IO.File.Delete(filePathPrimario);
            System.IO.File.Delete(filePathSecundario);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = Convert.ToInt16(Request.Cookies["UserId"])});
        }

        private bool ProdutoDataExists(int id)
        {
            return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
