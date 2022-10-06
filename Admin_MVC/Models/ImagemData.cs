using System.ComponentModel.DataAnnotations;
namespace Admin_MVC.Models
{
    public class ImagemData
    {
        [Key]
        public int Id { get; set; }

        public string PathLogo { get; set; }
        public string PathCarroselPrimario { get; set; }
        public string PathLogoSecundario { get; set; }
    }
}
