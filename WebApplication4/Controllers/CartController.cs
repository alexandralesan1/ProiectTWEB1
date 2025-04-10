using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Controllers
{
    public class CartController : Controller
    {
        private static List<CartItem> CartItems = new List<CartItem>();
        private readonly ProductService _productService;

        public CartController()
        {
            _productService = new ProductService();
        }

        public ActionResult Cart()
        {
            return View(CartItems);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                var existingItem = CartItems.FirstOrDefault(c => c.Product.Id == id);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    CartItems.Add(new CartItem { Product = product, Quantity = 1 });
                }
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var item = CartItems.FirstOrDefault(c => c.Product.Id == id);
            if (item != null)
            {
                CartItems.Remove(item);
            }
            return RedirectToAction("Cart");
        }
    }
}