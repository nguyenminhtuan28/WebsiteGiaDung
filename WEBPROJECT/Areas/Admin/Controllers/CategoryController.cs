using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBPROJECT.Models;
using WEBPROJECT.Repository;

namespace WEBPROJECT.Areas.Admin.Controllers
{

    [Area("Admin")]


    public class CategoryController : Controller
    {
        private readonly Datacontext _datacontext;

       
        public CategoryController(Datacontext context)
        {
            _datacontext = context;
        }
        public async Task<IActionResult> Index()
        {
           

            return View(await _datacontext.Categories.OrderByDescending(p=>p.Id).ToListAsync());
        }
        public async Task<IActionResult> Edit(int Id)
        {
            CategoryModel category = await _datacontext.Categories.FindAsync(Id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.Replace(" ", "-");
                var slug = await _datacontext.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "danh muc da co trong databse");
                    return View(category);
                }


                _datacontext.Update(category);
                await _datacontext.SaveChangesAsync();
                TempData["success"] = "cap nhat danh muc thanh cong";
                return RedirectToAction("Index");


            }
            else
            {
                TempData["error"] = "co vai thu bi loi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("/n ", errors);
                return BadRequest(errorMessage);
            }

            return View(category);
        }
        [HttpGet]

        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.Replace(" ", "-");
                var slug = await _datacontext.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "danh muc da co trong databse");
                    return View(category);
                }


                _datacontext.Add(category);
                await _datacontext.SaveChangesAsync();
                TempData["success"] = "thẻm danh muc thanh cong";
                return RedirectToAction("Index");


            }
            else
            {
                TempData["error"] = "co vai thu bi loi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("/n ", errors);
                return BadRequest(errorMessage);
            }
            
            return View(category);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            CategoryModel category = await _datacontext.Categories.FindAsync(Id);
           

            _datacontext.Categories.Remove(category);
            await _datacontext.SaveChangesAsync();
            TempData["success"] = "xoa danh muc thanh cong";
            return RedirectToAction("Index");


        }
    }
}
