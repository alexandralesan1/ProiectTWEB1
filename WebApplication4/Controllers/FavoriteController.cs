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
    public class FavoritesController : Controller
    {
        private static List<FavoriteItem> FavoriteItems = new List<FavoriteItem>();
        private readonly ProductService _productService;

        public FavoritesController()
        {
            _productService = new ProductService();
        }

        public ActionResult Favorites()
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
                    FavoriteItems.Add(new FavoriteItem { Product = product });
                }
            }
            return RedirectToAction("Favorites");
        }

        [HttpPost]
        public ActionResult RemoveFromFavorites(int id)
        {
            var item = FavoriteItems.FirstOrDefault(f => f.Product.Id == id);
            if (item != null)
            {
                FavoriteItems.Remove(item);
            }
            return RedirectToAction("Favorites");
        }
    }


}