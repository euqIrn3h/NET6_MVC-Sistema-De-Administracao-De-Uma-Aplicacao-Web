using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Business.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string Senha { get; set; }
    }
}
