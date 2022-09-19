using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Business.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required]
        public int idCategoria { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public string Valor { get; set; }
        [Required]
        [MaxLength(500)]
        public string linkShoppe { get; set; }
        [Required]
        public IFormFile FotoPrimaria { get; set; }
        [Required]
        public IFormFile FotoSecundaria { get; set; }
    }
}
