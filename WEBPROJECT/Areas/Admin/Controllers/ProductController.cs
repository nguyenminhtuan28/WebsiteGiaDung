using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBPROJECT.Models;
using WEBPROJECT.Repository;

namespace WEBPROJECT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly Datacontext _datacontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(Datacontext context, IWebHostEnvironment webHostEnvironment)
        {
            _datacontext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            var products = await _datacontext.Products
                .OrderByDescending(p => p.Id)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();

            return View(products);
        }

        // GET: Admin/Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_datacontext.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_datacontext.Brands, "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                product.Slug=product.Name.Replace(" ", "-");
                var slug= await _datacontext.Products.FirstOrDefaultAsync(p=>p.Slug==product.Slug);
                if (slug!=null)
                {
                    ModelState.AddModelError("", "san pham da co trong databse");
                    return View(product);
                }
                
                    if(product.ImageUpload!=null)
                    {
                        string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath,"media/product");
                        string imageName=Guid.NewGuid().ToString()+"_"+product.ImageUpload.FileName;
                        string filePath=Path.Combine(uploadsDir,imageName);
                        FileStream fs=new FileStream(filePath, FileMode.Create);
                        await product.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        product.Image=imageName;
                    }    
                
                _datacontext.Add(product);
                await _datacontext.SaveChangesAsync();
                TempData["success"] = "thẻm san pham thanh cong";
                return RedirectToAction("Index");

                
            }
            else
            {
                TempData["error"] = "co vai thu bi loi";
                List<string> errors = new List<string>();
                foreach(var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }   
                string errorMessage=string.Join("/n ", errors);
                return BadRequest(errorMessage);
            }    
            ViewBag.Categories = new SelectList(_datacontext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_datacontext.Brands, "Id", "Name", product.BrandId);
            return View(product);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            ProductModel product=await _datacontext.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_datacontext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_datacontext.Brands, "Id", "Name", product.BrandId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,ProductModel product)
        {
            ViewBag.Categories = new SelectList(_datacontext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_datacontext.Brands, "Id", "Name", product.BrandId);
            var existed_product=_datacontext.Products.Find(product.Id);
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "-");
                var slug = await _datacontext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "san pham da co trong databse");
                    return View(product);
                }

                if (product.ImageUpload != null)
                {

                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/product");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    string oldfilePath = Path.Combine(uploadsDir, product.Image);
                    try
                    {
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "co loi xay ra khi them san pham");
                    }

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    existed_product.Image = imageName;

                }
                existed_product.Name= product.Name;
                existed_product.Description= product.Description;
                existed_product.Price= product.Price;
                existed_product.CategoryId= product.CategoryId;
                existed_product.BrandId= product.BrandId;

                _datacontext.Update(existed_product);
                await _datacontext.SaveChangesAsync();
                TempData["success"] = "cap nhat san pham thanh cong";
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
            ViewBag.Categories = new SelectList(_datacontext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_datacontext.Brands, "Id", "Name", product.BrandId);
            return View(product);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            ProductModel product = await _datacontext.Products.FindAsync(Id);
            string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/product");
            string oldfilePath = Path.Combine(uploadsDir, product.Image);
            try
            {
                if (System.IO.File.Exists(oldfilePath))
                {
                    System.IO.File.Delete(oldfilePath);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "co loi xay ra khi them san pham");
            }
           
            _datacontext.Products.Remove(product);
            await _datacontext.SaveChangesAsync();
            TempData["error"] = "xoa san pham thanh cong";
            return RedirectToAction("Index");
            

        }
    }
}
