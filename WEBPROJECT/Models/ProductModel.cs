using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace WEBPROJECT.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "yeu cau nhap ten san pham")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "yeu cau nhap mo ta san pham")]
        public string Description { get; set; }
        [Required, MinLength(4, ErrorMessage = "yeu cau nhap gia san pham")]
        [Range(0.01,double.MaxValue)]
        [Column(TypeName ="decimal(8,2)")]
        public decimal Price { get; set; }
        public string Slug { get; set; }
        [Required,Range(1,int.MaxValue,ErrorMessage ="chon 1 thuong hieu")]
        public int BrandId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "chon 1 danh muc")]

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [FileExtensions]
        public IFormFile? ImageUpload { get; set; }
    }
}
