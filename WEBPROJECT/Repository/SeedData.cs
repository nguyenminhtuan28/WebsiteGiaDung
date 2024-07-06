using Microsoft.EntityFrameworkCore;
using System.Linq;
using WEBPROJECT.Models;

namespace WEBPROJECT.Repository
{
    public class SeedData
    {
        public static void SeedingData(Datacontext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any() || !_context.Categories.Any() || !_context.Brands.Any())
            {
                CategoryModel macbook = new CategoryModel { Name = "macbook", Slug = "macbook", Description = "macbook is brand biggest in the world", Status = 1 };
                CategoryModel pc = new CategoryModel { Name = "pc", Slug = "pc", Description = "pc is brand biggest in the world", Status = 1 };
                BrandModel apple = new BrandModel { Name = "apple", Slug = "apple", Description = "apple is brand biggest in the world", Status = 1 };
                BrandModel samsung = new BrandModel { Name = "samsung", Slug = "samsung", Description = "samsung is brand biggest in the world", Status = 1 };
                _context.Products.AddRange(
                    new ProductModel { Name = "MACBOOK", Slug = "MACBOOK", Description = "MACBOOK is LAPTOP", Image = "1.jpg", Category = macbook, Price = 111223, Brand = apple },
                    new ProductModel { Name = "pc", Slug = "pc", Description = "pc is LAPTOP", Image = "2.jpg", Category = pc, Price = 111223, Brand = samsung }
                );
                _context.SaveChanges();
            }
        }
    }
}
