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

     public BaseController()
     {
          _sessionService = new SessionService();
         
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
