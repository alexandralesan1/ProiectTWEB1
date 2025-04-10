using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;

namespace WebApplication4.Controllers
{
    public class HomeController : BaseController
    {
          private readonly ProductService _productService;
          public HomeController()
          {
               _productService = new ProductService();
          }
          public ActionResult Home()
          {
               var products = _productService.GetAllProducts();
               return View(products);
          }

     }
}