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
    public class CartController : Controller
    {
        private readonly ShopDBContext _context;
        private readonly ProductService _productService;

        public CartController()
        {
            _productService = new ProductService();
            _context = new ShopDBContext();
        }

          public ActionResult Cart()
          {
               var cartItems = _context.CartItems.ToList();
               decimal cartTotalPrice = cartItems.Sum(item => item.FinalPrice);

               HttpContext.Session["CartTotalPrice"] = cartTotalPrice;
               ViewBag.CartTotalPrice = cartTotalPrice;

               return View(cartItems);
          }

          [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                var existingItem = _context.CartItems.FirstOrDefault(c => c.Product.Id == id);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _context.CartItems.Add(new DBCartItemsTable { Product = product, Quantity = 1 });
                }
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var item = _context.CartItems.FirstOrDefault(c => c.Product.Id == id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
            }
            return RedirectToAction("Cart");
        }
    }
}