using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Business.Dtos
{
    public class AlteraSenhaDto
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Senha { get; set; }
        [MaxLength(255)]
        public string ConfirmaSenha { get; set; }
    }
}
