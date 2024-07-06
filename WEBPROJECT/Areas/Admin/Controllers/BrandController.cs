using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBPROJECT.Models;
using WEBPROJECT.Repository;

namespace WEBPROJECT.Areas.Admin.Controllers
    
{
    [Area("Admin")]

    public class BrandController : Controller
    {
       
        private readonly Datacontext _datacontext;


        public BrandController(Datacontext context)
        {
            _datacontext = context;
        }
        public async Task<IActionResult> Index()
        {


            return View(await _datacontext.Brands.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
