using Microsoft.AspNetCore.Mvc;
using Admin_MVC.Business.Dtos;
using Admin_MVC.Data;
using Admin_MVC.Models;

namespace Admin_MVC.Controllers
{
    public class ImagemController : Controller
    {
        private readonly DataContext _context;


        public ImagemController(DataContext context)
        {
            _context = context;
        }


        public IActionResult Edit()
        {
            var imagem = new ImagemDto();

            return View(imagem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ImagemDto imagemDto)
        {
            if(imagemDto.PathLogo != null)
            {
                var filePathLogo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", "EmpoderadasLogo.png");
                var filePathCarroselPrimario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", "EmpoderadasCarroselPrimario.png");
                var filePathCarroselSecundario = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", "EmpoderadasCarroselSecundario.png");

                using (var fileStream = new FileStream(filePathLogo, FileMode.Create))
                {
                    await imagemDto.PathLogo.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(filePathCarroselPrimario, FileMode.Create))
                {
                    await imagemDto.PathCarroselPrimario.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(filePathCarroselSecundario, FileMode.Create))
                {
                    await imagemDto.PathLogoSecundario.CopyToAsync(fileStream);
                }

                try
                {
                    var imagem = new ImagemData
                    {
                        Id = 1,
                        PathLogo = "https://empoderadas.softadworks.com/admin/images/EmpoderadasLogo.png",
                        PathCarroselPrimario = "https://empoderadas.softadworks.com/admin/images/EmpoderadasCarroselPrimario.png",
                        PathLogoSecundario = "https://empoderadas.softadworks.com/admin/images/EmpoderadasCarroselSecundario.png"
                    };
                    _context.Update(imagem);
                    _context.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Sobre", "Erro na página, tente novamente...");
                }
            }

            return View(imagemDto);
        }
    }
}
