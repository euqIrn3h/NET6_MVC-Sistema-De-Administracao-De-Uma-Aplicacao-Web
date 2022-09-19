using System.ComponentModel.DataAnnotations;


namespace Admin_MVC.Business.Dtos
{
    public class ProdutoApiDto
    {
        public int Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required]
        public int idCategoria { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Required]
        [MaxLength(500)]
        public string linkShoppe { get; set; }
        [Required]
        public string PathFotoPrimaria { get; set; }
        [Required]
        public string PathFotoSecundaria { get; set; }
    }
}
