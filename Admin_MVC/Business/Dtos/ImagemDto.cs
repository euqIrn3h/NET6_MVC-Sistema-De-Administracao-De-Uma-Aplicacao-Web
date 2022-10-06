namespace Admin_MVC.Business.Dtos
{
    public class ImagemDto
    {
        public int Id { get; set; }
        public IFormFile PathLogo { get; set; }
        public IFormFile PathCarroselPrimario { get; set; }
        public IFormFile PathLogoSecundario { get; set; }
    }
}
