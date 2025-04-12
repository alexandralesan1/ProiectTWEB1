using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Controllers;
using WebApplication4.Domain.Entities;

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


        public ActionResult Login2(string email, string password)
        {
           
            var user = _userService.Authenticate(email, password);

            if (user == null)
            {
                
                ViewBag.Error = "Nu există niciun utilizator cu acest email sau parola este incorectă.";
                return View(); 
            }

            
            ViewBag.SuccessMessage = "Autentificarea a avut loc cu succes!";
            return RedirectToAction("Home", "Home");
        }



    }
}
