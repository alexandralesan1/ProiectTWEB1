using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
          private readonly ProductService _productService;
          private readonly ShopDBContext _context;
          public HomeController()
          {
               _productService = new ProductService();
               _context = new ShopDBContext();
          }
          public ActionResult Home()
          {
               var newProducts = _context.Products
                   .OrderByDescending(p => p.Id)
                   .Take(8)
                   .ToList();

               var latestNews = _context.News
                   .OrderByDescending(n => n.CreatedAt)
                   .FirstOrDefault();

               var model = new HomePageViewModel
               {
                    NewProducts = newProducts,
                    LatestNews = latestNews
               };

               return View(model);
          }

     }
}