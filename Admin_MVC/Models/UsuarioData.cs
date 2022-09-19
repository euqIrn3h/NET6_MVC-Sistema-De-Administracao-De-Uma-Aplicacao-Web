using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Models
{
    public class UsuarioData
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [EmailAddress(ErrorMessage = "E-Mail inválido !")]
        [MaxLength(255)]
        public string Email {get; set;}
        [Required]
        [MaxLength(255)]
        public string Senha {get; set;}
        [Required]
        [MaxLength(255)]
        public string Nome {get; set;}
        public string? Descricao {get; set;}
        [MaxLength(255)]
        public string? PathFotoPerfil {get; set;}
        [Required]
        public int NivelAcesso {get; set;}
    }
}