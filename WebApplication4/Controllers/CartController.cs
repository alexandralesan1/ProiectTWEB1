using System.Linq;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Core;

public class CartController : Controller
{
     private readonly ProductService _productService;
     private readonly CartServices _cartService;

     public CartController()
     {
          _productService = new ProductService();
          _cartService = new CartServices(new ShopDBContext());
     }


        public ActionResult Cart()
        {
            var cartItems = _context.CartItems.ToList();
            decimal cartTotalPrice = cartItems.Sum(item => item.FinalPrice);
            ViewBag.CartTotalPrice = cartTotalPrice;
            return View(cartItems);
        }


        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                var existingItem = _context.CartItems.FirstOrDefault(c => c.Product.Id == id);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _context.CartItems.Add(new DBCartItemsTable { Product = product, Quantity = 1 });
                }
            }
            return RedirectToAction("Cart");
        }

     [HttpPost]
     public ActionResult RemoveFromCart(int id)
     {
          var userId = _cartService.GetLoggedInUserId();
          if (userId == null)
          {
               return RedirectToAction("Login", "Login");
          }

          _cartService.RemoveFromCart(userId.Value, id);
          return RedirectToAction("Cart");
     }
}