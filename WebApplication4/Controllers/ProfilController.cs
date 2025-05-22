using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;



namespace WebApplication4.Web.Controllers
{
     public class ProfilController : Controller
     {
          public ActionResult Profil()
          {
               var userSession = System.Web.HttpContext.Current.Session["UserSession"];
               var userRole = System.Web.HttpContext.Current.Session["UserRole"];

               if (userSession == null || userRole == null || (UserRole)userRole != UserRole.Buyer)
               {
                    return RedirectToAction("Home", "Home"); 
               }

               var user = (DBUserTable)userSession;
               return View(user);
          }
     }
}