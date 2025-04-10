using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;

namespace WebApplication4.Controllers
{
    public class Home1Controller : Controller
    {
          private readonly ProductService _productService;
          public Home1Controller()
          {
               _productService = new ProductService();
          }
          public ActionResult Home1()
          {
               var products = _productService.GetAllProducts();
               return View(products);
          }

     }
}