using System.Collections.Generic;
using System;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;
using System.Linq;
using System.Data.Entity;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.BusinessLogic.Interfaces;

public class CartServices
{
     private readonly ShopDBContext _CartContext;
     private readonly SessionService _sessionService;

     public CartServices(ShopDBContext context, SessionService sessionService)
     {
          _CartContext = context ?? throw new ArgumentNullException(nameof(context));
          _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
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
          var cartItems = _CartContext.CartItems.Where(c => c.UserId == userId).ToList();
          return cartItems.Any() ? cartItems.Sum(item => item.FinalPrice) : 0;
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
}