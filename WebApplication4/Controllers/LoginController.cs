using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Controllers;
using WebApplication4.Domain.Entities;



namespace WebApplication4.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;

        public LoginController()
        {
            _userService = new UserService();
        }

      
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(DBUserTable user)
        {

            _userService.AddUser(user);

            return RedirectToAction("Login");


        }
    }


}


