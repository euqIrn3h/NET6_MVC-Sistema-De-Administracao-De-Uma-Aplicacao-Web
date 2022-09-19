using System.ComponentModel.DataAnnotations;

namespace Admin_MVC.Models
{
    public class CategoriaData
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [MaxLength(50)]
        public string Nome {get; set;}
    }
}