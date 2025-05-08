using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Controllers;

public class BaseController : Controller
{
     private readonly SessionService _sessionService = new SessionService();

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

         
          
          
          
          
          
          //Cart Total Price
          if (CartController.CartItems != null && CartController.CartItems.Any())
          {
               ViewBag.CartTotalPrice = CartController.CartItems.Sum(item => item.FinalPrice);
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