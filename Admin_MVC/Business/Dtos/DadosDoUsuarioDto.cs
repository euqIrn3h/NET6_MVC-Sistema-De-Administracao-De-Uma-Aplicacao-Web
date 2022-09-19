using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Business.Dtos
{
    public class DadosDoUsuarioDto
    {
        public int Id {get; set;}
        [MaxLength(255)]
        public string Email {get; set;}
        [MaxLength(255)]
        public string Nome {get; set;}
        public string? Descricao {get; set;}
        public IFormFile? FotoPerfil {get; set;}
    }
}