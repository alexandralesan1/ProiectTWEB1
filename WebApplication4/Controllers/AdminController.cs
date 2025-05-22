using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
     public class AdminController : Controller
     {
          private readonly AdminServices _adminService;

          public AdminController()
          {
               _adminService = new AdminServices(new ShopDBContext());
          }



          public ActionResult AdminProfile()
          {
               var userSession = System.Web.HttpContext.Current.Session["UserSession"];
               var userRole = System.Web.HttpContext.Current.Session["UserRole"];

               if (userSession == null || userRole == null || (UserRole)userRole != UserRole.Admin)
               {
                    return RedirectToAction("Home", "Home");
               }
               var admin = (DBUserTable)userSession;
               return View(admin);
          }

          public ActionResult Products()
          {
               ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
               ViewBag.Brand = Enum.GetValues(typeof(ProductBrand));
               ViewBag.Country = Enum.GetValues(typeof(ProductCountry));
               ViewBag.SpecialCategory = Enum.GetValues(typeof(ProductSpecialCategory));
               var products = _adminService.GetProducts();
               return View(products);
          }

          public ActionResult Users()
          {
               var users = _adminService.GetUsers().Where(u => u.Role == UserRole.Buyer).ToList();
               return View(users);
          }

          [HttpPost]
          public ActionResult AddProduct(DBProductTable product, HttpPostedFileBase ImageFile)
          {
               _adminService.AddProduct(product, ImageFile);
               return RedirectToAction("Products");
          }

          [HttpPost]
          public ActionResult DeleteProduct(int id)
          {
               _adminService.DeleteProduct(id);
               return RedirectToAction("Products");
          }

          [HttpPost]
          public ActionResult BlockUser(int userId)
          {
               _adminService.BlockUser(userId);
               return RedirectToAction("Users");
          }

          [HttpPost]
          public ActionResult UnBlockUser(int userId)
          {
               _adminService.UnBlockUser(userId);
               return RedirectToAction("Users");
          }

          [HttpPost]
          public ActionResult DeleteUser(int userId)
          {
               _adminService.DeleteUser(userId);
               return RedirectToAction("Users");
          }

          public ActionResult EditProduct(int id)
          {
               var product = _adminService.GetProductById(id);
               if (product == null)
               {
                    return RedirectToAction("Products");
               }

               ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
               ViewBag.Brand = Enum.GetValues(typeof(ProductBrand));
               ViewBag.Country = Enum.GetValues(typeof(ProductCountry));
               ViewBag.SpecialCategory = Enum.GetValues(typeof(ProductSpecialCategory));

               return View(product);
          }

          [HttpPost]
          public ActionResult UpdateProduct(DBProductTable product, HttpPostedFileBase ImageFile)
          {
               _adminService.UpdateProduct(product, ImageFile);
               return RedirectToAction("Products");
          }
          //[HttpPost]
          //public ActionResult AddNews(string title, string content, HttpPostedFileBase imageFile)
          //{
          //     _adminService.AddNews(title, content, imageFile);
          //     return RedirectToAction("Dashboard");
          //}
     }
}