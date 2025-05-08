using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Controllers;
using WebApplication4.Domain.Entities;
using WebApplication4.Helpers;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication4.Web.Controllers
{
    public class Login2Controller : Controller
    {
        private readonly UserService _userService;

        public Login2Controller()
        {
            _userService = new UserService();
        }

        public ActionResult Login2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            string loginIp = Request.UserHostAddress;
            DateTime loginDateTime = DateTime.Now;

            var userLogin = _userService.AuthenticateAndUpdateLogin(email, password, loginIp, loginDateTime);

            if (userLogin != null)
            {
                // Crearea unui cookie de sesiune
                var apiCookie = new HttpCookie("X-KEY")
                {
                    Value = CookieGenerator.Create(userLogin.Email), // Folosește email-ul sau alt identificator pentru crearea cookie-ului
                    Expires = DateTime.Now.AddHours(1) // Setează expirarea cookie-ului (ex: 1 oră)
                };

                ControllerContext.HttpContext.Response.Cookies.Add(apiCookie); // Adaugă cookie-ul în răspuns

                // Salvarea sau actualizarea sesiunii în baza de date
                using (var db = new SessionContext())
                {
                    Session currentSession = db.Sessions.FirstOrDefault(e => e.Username == userLogin.Email); // Căutăm sesiunea existentă pe baza email-ului utilizatorului

                    if (currentSession != null)
                    {
                        // Dacă sesiunea există, actualizăm cookie-ul și timpul de expirare
                        currentSession.CookieString = apiCookie.Value;
                        currentSession.ExpireTime = DateTime.Now.AddMinutes(60); // Sesiunea va expira după 60 de minute

                        // Salvează modificările în baza de date
                        db.Entry(currentSession).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        // Poți adăuga logică aici pentru a crea o sesiune nouă dacă nu există
                        var newSession = new Session
                        {
                            Username = userLogin.Email,
                            CookieString = apiCookie.Value,
                            ExpireTime = DateTime.Now.AddMinutes(60)
                        };

                        db.Sessions.Add(newSession);
                        db.SaveChanges();
                    }
                }

                // Redirecționarea utilizatorului în funcție de rol
                if (userLogin.Role == "Admin")
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (userLogin.Role == "Client")
                {
                    return RedirectToAction("ClientHome", "Client");
                }
                else
                {
                    return RedirectToAction("GuestHome", "Home");
                }
            }
            else
            {
                ViewBag.Error = "Email sau parolă incorectă.";
                return View();
            }
        }







    }
}
