using System;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;
using WebApplication4.Helpers;

namespace WebApplication4.Web.Controllers
{
     public class LoginController : Controller
     {

          private readonly UserService _userService;
          private readonly SessionService _sessionService;

          public LoginController()
          {
               _userService = new UserService();
               _sessionService = new SessionService();
          }

          public ActionResult Login()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Login(DBUserTable user)
          {
               var authenticatedUser = _userService.Authenticate(user.Email, user.Password);

               if (authenticatedUser != null)
               {
                    
                    if (authenticatedUser.IsBlocked)
                    {
                         ViewBag.LoginError = "Contul tău este blocat. Contactează suportul pentru detalii.";
                         return View();
                    }
                    HttpCookie authCookie = new HttpCookie("X-KEY")
                    {
                         Value = CookieGenerator.Create(authenticatedUser.Email),
                         Expires = DateTime.Now.AddMinutes(60),
                         HttpOnly = true
                    };
                    Response.Cookies.Add(authCookie);

                   
                    _sessionService.StoreSession(authenticatedUser.Email, authCookie.Value);
                    HttpContext.Session["UserSession"] = authenticatedUser;
                    HttpContext.Session["UserRole"] = authenticatedUser.Role;

                    return RedirectToAction("Home", "Home");
               }

               ViewBag.LoginError = "Email sau parolă invalidă.";
               return View();
          }

          public ActionResult Register()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Register(DBUserTable user)
          {
               if (_userService.UserExists(user.Email, user.Name))
               {
                    ViewBag.RegisterError = "Email or Username already exists!";
                    return View();
               }

               user.Role = UserRole.Buyer;
               _userService.AddUser(user);

               return RedirectToAction("Login");
          }

          public ActionResult Logout()
          {
               HttpContext.Session.Clear();
               _sessionService.Logout("X-KEY");
               return RedirectToAction("Login");
          }

          public ActionResult RegisterAdmin()
          {
               return View();
          }
          [HttpPost]
          public ActionResult RegisterAdmin(string username, string email, string password, string adminKey)
          {
               const string adminAccessKey = "admin_registration_key"; 

               if (adminKey != adminAccessKey)
               {
                    ViewBag.RegisterError = "Invalid admin registration key.";
                    return View("RegisterAdmin");
               }

               var newAdmin = new DBUserTable
               {
                    Name = username,
                    Email = email,
                    Password = password,
                    Role = UserRole.Admin 
               };

               if (_userService.UserExists(newAdmin.Email, newAdmin.Name))
               {
                    ViewBag.RegisterError = "Email or Username already exists!";
                    return View();
               }
               _userService.AddUser(newAdmin);
               return RedirectToAction("Login");
          }
     }
}


