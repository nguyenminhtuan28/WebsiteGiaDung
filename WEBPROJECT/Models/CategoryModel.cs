using System.ComponentModel.DataAnnotations;

namespace WEBPROJECT.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "yeu cau nhap ten danh mũc")]
        public string Name { get; set; }
        [Required (ErrorMessage = "yeu cau nhap mo ta danh mũc")]
        public string Description { get; set; }
        
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
