using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;

namespace WebApplication4.BusinessLogic.Core
{
     public class FavoriteServices
     {
          private readonly ShopDBContext _context;
          private readonly SessionService _sessionService;

          public FavoriteServices(ShopDBContext context, SessionService sessionService)
          {
               _context = context ?? throw new ArgumentNullException(nameof(context));
               _sessionService = sessionService;
          }

          public List<DBFavoriteItemsTable> GetFavoriteItems(int userId)
          {
               return _context.FavoriteItems
                   .Where(f => f.UserId == userId)
                   .Include(f => f.Product) 
                   .ToList();
          }

          public void AddToFavorites(int userId, int productId)
          {
               var product = _context.Products.FirstOrDefault(p => p.Id == productId);
               if (product == null) return;

               var existingItem = _context.FavoriteItems.FirstOrDefault(f => f.ProductId == productId && f.UserId == userId);
               if (existingItem == null)
               {
                    _context.FavoriteItems.Add(new DBFavoriteItemsTable
                    {
                         UserId = userId,
                         ProductId = productId,
                         Product = product 
                    });

                    _context.SaveChanges();
               }
          }

          public void RemoveFromFavorites(int userId, int productId)
          {
               var item = _context.FavoriteItems.FirstOrDefault(f => f.ProductId == productId && f.UserId == userId);
               if (item != null)
               {
                    _context.FavoriteItems.Remove(item);
                    _context.SaveChanges();
               }
          }
     }
}
