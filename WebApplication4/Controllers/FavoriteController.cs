
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Controllers
{
     public class FavoriteController : Controller
     {
          private readonly FavoriteServices _favoriteService;
          private readonly SessionService _sessionService;

          public FavoriteController()
          {
               _sessionService = new SessionService();
               _favoriteService = new FavoriteServices(new ShopDBContext(), _sessionService);
          }

          public ActionResult Favorite()
          {
               var userId = _sessionService.GetLoggedInUserId();
               if (userId == null)
               {
                    return RedirectToAction("Login", "Login");
               }

               var userFavorites = _favoriteService.GetFavoriteItems(userId.Value);
               return View(userFavorites);
          }

          [HttpPost]
          public ActionResult AddToFavorites(int id)
          {
               var userId = _sessionService.GetLoggedInUserId();
               if (userId == null)
               {
                    return RedirectToAction("Login", "Login");
               }

               _favoriteService.AddToFavorites(userId.Value, id);
               return RedirectToAction("Favorite");
          }

          [HttpPost]
          public ActionResult RemoveFromFavorites(int id)
          {
               var userId = _sessionService.GetLoggedInUserId();
               if (userId == null)
               {
                    return RedirectToAction("Login", "Login");
               }

               _favoriteService.RemoveFromFavorites(userId.Value, id);
               return RedirectToAction("Favorite");
          }
     }


}