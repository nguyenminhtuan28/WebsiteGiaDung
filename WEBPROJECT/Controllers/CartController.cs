using Microsoft.AspNetCore.Mvc;
using WEBPROJECT.Models;
using WEBPROJECT.Models.ViewModels;
using WEBPROJECT.Repository;

namespace WEBPROJECT.Controllers
{
	public class CartController : Controller
	{
		private readonly Datacontext _datacontext;
		public CartController(Datacontext _context)
		{
			_datacontext = _context;
		}

		public IActionResult Index()
		{
			List<CartItemModel> cartitem=HttpContext.Session.GetJson<List<CartItemModel>>("Cart")??new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartitem,
				GrandTotal = cartitem.Sum(x => x.Quantity * x.Price)
			};

			return View(cartVM);
		}
		public ActionResult CheckOut()
		{
			return View("~/View/CheckOut/Index.cshtml");
		}
		public async Task<IActionResult> Add(int Id)
		{
			ProductModel product = await _datacontext.Products.FindAsync(Id);

			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("cart") ?? new List<CartItemModel>();

			CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItems == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItems.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
            TempData["success"] = "ADD TO CART SUCCESSFULLY!!!!";
			return Redirect(Request.Headers["Referer"].ToString());
		}
        public async Task<ActionResult> Decrease(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id);
            }

            if (cart.Count == 0)
            {
				HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);

            }
            TempData["success"] = "Decrease ITEM SUCCESSFULLY!!!!";

            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Increase(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            if (cartItem.Quantity >= 1)
            {
                ++cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);

            }
            TempData["success"] = "Increase ITEM SUCCESSFULLY!!!!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            cart.RemoveAll(p => p.ProductId == Id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["success"] = "REMOVE ITEM SUCCESSFULLY!!!!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear(int Id)
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }


    }
}
