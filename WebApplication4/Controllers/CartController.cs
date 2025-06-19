using System.Linq;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;


public class CartController : Controller
{
     private readonly CartServices _cartService;
     private readonly SessionService _sessionService;

     public CartController()
     {
          _sessionService = new SessionService(); 
          _cartService = new CartServices(new ShopDBContext(), _sessionService); 
     }


     public ActionResult Cart()
     {
          var userId = _sessionService.GetLoggedInUserId();
          var userSession = System.Web.HttpContext.Current.Session["UserSession"];
          if (userSession == null)
          {
               return RedirectToAction("Login", "Login"); 
          }

          var userCartItems = _cartService.GetCartItems(userId.Value);
          ViewBag.CartTotalPrice = _cartService.GetCartTotal(userId.Value);

          return View(userCartItems);
     }

     [HttpPost]
     public ActionResult AddToCart(int id)
     {
          var userId = _sessionService.GetLoggedInUserId(); 
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
          var userId = _sessionService.GetLoggedInUserId();
          if (userId == null)
          {
               return RedirectToAction("Login", "Login");
          }

          _cartService.RemoveFromCart(userId.Value, id);
          return RedirectToAction("Cart");
     }
}