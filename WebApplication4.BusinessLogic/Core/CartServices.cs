using System.Collections.Generic;
using System;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;
using System.Linq;
using System.Data.Entity;
using WebApplication4.BusinessLogic.Core;

public class CartServices
{
     private readonly ShopDBContext _CartContext;
     private readonly SessionService _sessionService;

     public CartServices(ShopDBContext context)
     {
          _CartContext = context ?? throw new ArgumentNullException(nameof(context));
     }

     public List<DBCartItemsTable> GetCartItems(int userId)
     {
          return _CartContext.CartItems
              .Where(c => c.UserId == userId)
              .Include(c => c.Product)
              .ToList();
     }

     public decimal GetCartTotal(int userId)
     {
          return _CartContext.CartItems
              .Where(c => c.UserId == userId)
              .Sum(item => item.FinalPrice); 
     }


     public void AddToCart(int userId, int productId)
     {
          var product = _CartContext.Products.FirstOrDefault(p => p.Id == productId);
          if (product == null) return;

          var existingItem = _CartContext.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
          if (existingItem != null)
          {
               existingItem.Quantity++;
          }
          else
          {
               _CartContext.CartItems.Add(new DBCartItemsTable
               {
                    UserId = userId,
                    ProductId = productId,
                    Product = product,
                    Quantity = 1
               });
          }

          _CartContext.SaveChanges();
     }

     public void RemoveFromCart(int userId, int productId)
     {
          var item = _CartContext.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
          if (item != null)
          {
               _CartContext.CartItems.Remove(item);
               _CartContext.SaveChanges();
          }
     }

     public void UpdateCartItem(int userId, int productId, int quantity)
     {
          var cartItem = _CartContext.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
          if (cartItem != null)
          {
               cartItem.Quantity = quantity;
               _CartContext.SaveChanges();
          }
     }

     public decimal GetItemFinalPrice(int userId, int productId)
     {
          var cartItem = _CartContext.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
          return cartItem != null ? cartItem.FinalPrice : 0;
     }

     public int? GetLoggedInUserId()
     {
          var authCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
          if (authCookie == null) return null;

          var userEmail = _sessionService.RetrieveSession(authCookie.Value);
          if (string.IsNullOrEmpty(userEmail)) return null;

          var user = _CartContext.Users.FirstOrDefault(u => u.Email == userEmail);
          return user?.Id;
     }


}
