using Microsoft.AspNetCore.Mvc;
using Admin_MVC.Models;
using Admin_MVC.Data;

namespace Admin_MVC.Controllers
{
    public class TermoController : Controller
    {
        private readonly DataContext _context;


        public TermoController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            var termo = _context.Termos.Find(1);

            return View(termo);
        }

        [HttpPost]
        public IActionResult Edit(TermoData termos)
        {
            try
            {
                _context.Update(termos);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }catch (Exception ex)
            {
                ModelState.AddModelError("Sobre", "Erro na página, tente novamente...");
            }
            return View(termos);
        }
    }
}
