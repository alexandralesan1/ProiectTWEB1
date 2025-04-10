using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;

namespace WebApplication4.Controllers
{
    public class FilterController : Controller
    {
        private readonly ProductService _productService;

        public FilterController()
        {
            _productService = new ProductService();
        }
        public ActionResult Filter(string category)
        {
            var products = string.IsNullOrEmpty(category)
                ? _productService.GetAllProducts()
                : _productService.GetFilteredProducts(category);

            return View(products);
        }
    }
}