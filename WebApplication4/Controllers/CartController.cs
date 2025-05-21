using System.Linq;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;

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
          var userSession = (DBUserTable)System.Web.HttpContext.Current.Items["UserSession"];
          if (userSession == null)
          {
               return RedirectToAction("Login", "Login");
          }

          var userCartItems = _cartService.GetCartItems(userSession.Id);
          ViewBag.CartTotalPrice = _cartService.GetCartTotal(userSession.Id);

          return View(userCartItems);
     }
     [HttpPost]
     public ActionResult AddToCart(int id)
     {
          var userId = _cartService.GetLoggedInUserId(); 
          if (userId == null)
          {
               return RedirectToAction("Login", "Login");
          }

          _cartService.AddToCart(userId.Value, id);
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