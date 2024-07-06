using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBPROJECT.Models;
using WEBPROJECT.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace WEBPROJECT.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Datacontext _datacontext;
        public CategoryController(Datacontext context)
        {
            _datacontext = context;
        }

        public async Task<IActionResult> Index(string Slug = "")
        {
            var category = await _datacontext.Categories.FirstOrDefaultAsync(c => c.Slug == Slug);
            if (category == null) return RedirectToAction("Index");

            var productbyCategory = _datacontext.Products.Where(p => p.CategoryId == category.Id);

            return View(await productbyCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
