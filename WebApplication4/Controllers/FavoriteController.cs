//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace WebApplication4.Controllers
//{
//    public class FavoriteController : Controller
//    {
//        // GET: Favorite
//        public ActionResult Favorite()
//        {
//            return View();
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Controllers
{
    public class FavoriteController : Controller
    {
        private static List<DBFavoriteItemsTable> FavoriteItems = new List<DBFavoriteItemsTable>();
        private readonly ProductService _productService;

        public FavoriteController()
        {
            _productService = new ProductService();
        }

        public ActionResult Favorite()
        {
            return View(FavoriteItems);
        }

        [HttpPost]
        public ActionResult AddToFavorites(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                var existingItem = FavoriteItems.FirstOrDefault(f => f.Product.Id == id);
                if (existingItem == null)
                {
                    FavoriteItems.Add(new DBFavoriteItemsTable { Product = product });
                }
            }
            return RedirectToAction("Favorite");
        }

        [HttpPost]
        public ActionResult RemoveFromFavorites(int id)
        {
            var item = FavoriteItems.FirstOrDefault(f => f.Product.Id == id);
            if (item != null)
            {
                FavoriteItems.Remove(item);
            }
            return RedirectToAction("Favorite");
        }
    }


}