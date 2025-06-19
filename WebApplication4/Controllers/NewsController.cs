using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication4.BusinessLogic.DBModel.Seed;

namespace WebApplication4.Controllers
{
     public class NewsController : Controller
     {
          private readonly ShopDBContext _context = new ShopDBContext();

          public ActionResult News()
          {
               var latest = _context.News.OrderByDescending(n => n.CreatedAt).FirstOrDefault();
               var others = _context.News
                   .Where(n => n.Id != latest.Id)
                   .OrderByDescending(n => n.CreatedAt)
                   .Take(10)
                   .ToList();

               return View("NewsPage", Tuple.Create(latest, others));
          }

          public ActionResult Details(int id)
          {
               var selected = _context.News.FirstOrDefault(n => n.Id == id);
               var others = _context.News.Where(n => n.Id != id).OrderByDescending(n => n.CreatedAt).ToList();
               return View("NewsPage", Tuple.Create(selected, others));
          }
     }
}