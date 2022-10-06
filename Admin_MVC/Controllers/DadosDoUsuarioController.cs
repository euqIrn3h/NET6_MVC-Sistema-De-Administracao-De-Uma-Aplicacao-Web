using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Data;
using Admin_MVC.Models;
using AutoMapper;
using Admin_MVC.Business.Dtos;
using CryptSharp;

namespace Admin_MVC.Controllers
{
    public class DadosDoUsuarioController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DadosDoUsuarioController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int id){
            return _context.Usuarios.Find(id) != null ? 
                          View( _mapper.Map<DadosDoUsuarioDto>( _context.Usuarios.Find(id))) :
                          Problem("Usuário Inexistente");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var dadosDoUsuarioDto = _mapper.Map<DadosDoUsuarioDto>( await _context.Usuarios.FindAsync(id));

            if (dadosDoUsuarioDto == null)
            {
                return NotFound();
            }
            return View(dadosDoUsuarioDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Email,Nome,Descricao,FotoPerfil")] DadosDoUsuarioDto dadosDoUsuarioDto)
        {
            //Verificando que o email não pertence a outro usuario
            var emailUsuarios = _context.Usuarios.Where(x => x.Email == dadosDoUsuarioDto.Email).AsNoTracking().FirstOrDefault();
            if (emailUsuarios != null && emailUsuarios.Id != dadosDoUsuarioDto.Id)
                ModelState.AddModelError("Email", "Email já cadastrado !!!");

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioData = _context.Usuarios.Where(x => x.Id == dadosDoUsuarioDto.Id).FirstOrDefault();
                    usuarioData.Email = dadosDoUsuarioDto.Email;
                    usuarioData.Nome = dadosDoUsuarioDto.Nome;
                    usuarioData.Descricao = dadosDoUsuarioDto.Descricao;

                    var fileName = $"FotoU-{usuarioData.Id}.png";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await dadosDoUsuarioDto.FotoPerfil.CopyToAsync(fileStream);
                    }

                    usuarioData.PathFotoPerfil = "https://empoderadas.softadworks.com/admin/images/" + fileName;

                    _context.Update(usuarioData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Usuarios.Find(dadosDoUsuarioDto.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {id = dadosDoUsuarioDto.Id});
            }
            return View(dadosDoUsuarioDto);
        }

        [HttpGet]
        public IActionResult AlterarSenha(int Id)
        {
            var usuario = new AlteraSenhaDto();
            usuario.Id = Id;
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AlterarSenha([Bind("Id,Senha,ConfirmaSenha")] AlteraSenhaDto UsuarioDto)
        {
            if (UsuarioDto.Senha != UsuarioDto.ConfirmaSenha || String.IsNullOrEmpty(UsuarioDto.Senha))
                ModelState.AddModelError("Senha", "As senhas tem que ser iguais e não podem estar vazias !!!");

            if (ModelState.IsValid)
            {
                var usuarioData = _context.Usuarios.Where(x => x.Id == UsuarioDto.Id).FirstOrDefault();
                usuarioData.Senha = Crypter.MD5.Crypt(UsuarioDto.Senha);

                _context.Update(usuarioData);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new {id = UsuarioDto.Id });
            }

            return View(UsuarioDto);
        }

    }
}