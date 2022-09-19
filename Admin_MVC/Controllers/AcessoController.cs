using Microsoft.AspNetCore.Mvc;
using Admin_MVC.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Data;
using Admin_MVC.Models;
using CryptSharp;
using System.Web;


namespace Admin_MVC.Controllers
{
    public class AcessoController : Controller
    {
        private readonly DataContext _context;

        public AcessoController(DataContext context)
        {
           _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.IsAcesso = true;
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginDto login)
        {
            ViewBag.IsAcesso = true;
            UsuarioData usuario = null;
            try
            {
               usuario  = _context.Usuarios.Where(x => x.Email == login.Email).FirstOrDefault();
                if(usuario == null)
                    ModelState.AddModelError("Email", "Email Inválido!!!");
            }
            catch
            {
                ModelState.AddModelError("Email", "Erro ao digitar email ou senha!!!");
            }

            if (ModelState.IsValid)
            { 
                if (Crypter.CheckPassword(login.Senha, usuario.Senha))
                {
                    var options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(2)
                    };
                    Response.Cookies.Append("UserId", Convert.ToString(usuario.Id), options);
                    Response.Cookies.Append("UserAuth", Convert.ToString(usuario.NivelAcesso),options);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Senha", "Senha Inválida!!!");
                }
            }

            return View();
        }

        public IActionResult Sair()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserAuth");
            return RedirectToAction("Index");
        }
    }
}
