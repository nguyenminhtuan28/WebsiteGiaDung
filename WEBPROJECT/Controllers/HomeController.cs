using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WEBPROJECT.Models;
using WEBPROJECT.Repository;

namespace WEBPROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly Datacontext _datacontext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,Datacontext context)
        {
            _logger = logger;
            _datacontext = context;
        }

        public IActionResult Index()
        {
            var products=_datacontext.Products.Include("Category").Include("Brand").ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
