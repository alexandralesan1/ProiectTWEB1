using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;

public class BaseController : Controller
{
     private readonly SessionService _sessionService;
     private readonly CartServices _cartService;

     public BaseController()
     {
          _sessionService = new SessionService();
          _cartService = new CartServices(new ShopDBContext()); // Inject DB context into service
     }

     protected override void OnActionExecuting(ActionExecutingContext filterContext)
     {
          var apiCookie = Request.Cookies["X-KEY"];
          if (apiCookie != null)
          {
               var profile = _sessionService.ValidateSession(apiCookie.Value);
               if (profile != null)
               {
                    HttpContext.Session["UserSession"] = profile;
                    HttpContext.Session["LoginStatus"] = "login";
               }
               else
               {
                    HttpContext.Session.Clear();
                    ExpireCookie("X-KEY");
               }
          }
          else
          {
               HttpContext.Session["LoginStatus"] = "logout";
          }

          var userSession = System.Web.HttpContext.Current.Session["UserSession"] as DBUserTable;
          if (userSession != null)
          {
               ViewBag.CartTotalPrice = _cartService.GetCartTotal(userSession.Id);
          }
          else
          {
               ViewBag.CartTotalPrice = 0;
          }

          base.OnActionExecuting(filterContext);
     }

     private void ExpireCookie(string cookieName)
     {
          if (Request.Cookies.AllKeys.Contains(cookieName))
          {
               var cookie = Request.Cookies[cookieName];
               cookie.Expires = DateTime.Now.AddDays(-1);
               Response.Cookies.Add(cookie);
          }
     }
}
