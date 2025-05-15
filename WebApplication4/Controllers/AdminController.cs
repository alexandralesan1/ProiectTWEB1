using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.AuthSession;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService _productService;
          private readonly UserService _userService;

        public AdminController()
        {
            _productService = new ProductService();
               _userService = new UserService();
        }

        //[AdminMod]
        public ActionResult Admin()
        {

            var products = _productService.GetAllProducts();

            ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
            ViewBag.Brand = Enum.GetValues(typeof(ProductBrand));
            ViewBag.Country = Enum.GetValues(typeof(ProductCountry));
            ViewBag.SpecialCategory = Enum.GetValues(typeof(ProductSpecialCategory));


               var userSession = System.Web.HttpContext.Current.Session["UserSession"];
               var userRole = System.Web.HttpContext.Current.Session["UserRole"];

               if (userSession == null && userRole == null && (UserRole)userRole != UserRole.Admin)
               {
                    return RedirectToAction("Home", "Home");
               }
               

               return View(products);
        }

        [HttpPost]
        public ActionResult AddProduct(DBProductTable product, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                ImageFile.SaveAs(path);
                product.ImageUrl = "/Content/images/" + fileName;
            }

            _productService.AddProduct(product);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Admin");
        }
    }
}