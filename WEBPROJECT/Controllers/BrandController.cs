using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBPROJECT.Models;
using WEBPROJECT.Repository;
using System.Linq;
using System.Threading.Tasks;
namespace WEBPROJECT.Controllers
{
    public class BrandController : Controller
    {
        private readonly Datacontext _datacontext;
        public BrandController(Datacontext context)
        {
            _datacontext = context;
        }

        public async Task<IActionResult> Index(string Slug = "")
        {
            BrandModel brand =  _datacontext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
            if (brand == null) return RedirectToAction("Index");

            var productbyBrand = _datacontext.Products.Where(p => p.BrandId == brand.Id);

            return View(await productbyBrand.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
