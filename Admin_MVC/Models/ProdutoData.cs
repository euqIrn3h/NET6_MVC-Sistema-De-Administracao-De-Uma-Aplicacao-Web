using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Models
{
    public class ProdutoData
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public int IdUsuario {get; set;}
        [Required]
        [MaxLength(100)]
        public string Nome {get; set;}
        [Required]
        public int idCategoria {get; set;}
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor {get; set;}
        [Required]
        [MaxLength(1000)]
        public string linkShoppe {get; set;}
        [Required]
        public string PathFotoPrimaria {get; set;}
        [Required]
        public string PathFotoSecundaria {get; set;}
    }
}