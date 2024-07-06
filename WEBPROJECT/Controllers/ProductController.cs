using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WEBPROJECT.Repository;

namespace WEBPROJECT.Controllers
{
    public class ProductController : Controller
    {
        private readonly Datacontext _datacontext;
        public ProductController(Datacontext context)
        {
            _datacontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
            var productById = await _datacontext.Products.FindAsync(Id);

            if (productById == null) return NotFound();

            return View(productById);
        }
    }
}
