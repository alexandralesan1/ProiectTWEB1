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
        internal static object CartItems;

        public CartController()
        {
            _productService = new ProductService();
            _context = new ShopDBContext();
        }

          public ActionResult Cart()
          {
               decimal cartTotalPrice = _context.CartItems.Sum(item => item.FinalPrice);
               ViewBag.CartTotalPrice = cartTotalPrice;
               return View(_context.CartItems);
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