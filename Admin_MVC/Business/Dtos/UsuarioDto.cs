using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Business.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "E-Mail inválido !")]
        [MaxLength(255)]
        public string Email {get; set;}
        [Required(ErrorMessage = "Necessário senha")]
        [MaxLength(255)]
        public string Senha {get; set;}
        [Required(ErrorMessage = "Necessário preencher nome")]
        [MaxLength(255)]
        public string Nome {get; set;}
        [Required(ErrorMessage = "Necessário Nível de Acesso")]
        public int NivelAcesso {get; set;}
    }
}