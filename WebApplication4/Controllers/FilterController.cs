using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Domain.Entities; // Importă entitatea DBProductTable

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

       
            var brandList = products.Select(p => p.Brand)
                                    .Distinct()  
                                    .ToList();


            ViewBag.BrandList = brandList;

       


            var categoryList = products.Select(p => p.Category)
                                      .Distinct()
                                      .ToList();
            ViewBag.CategoryList = categoryList;  

         



            var countryList = products.Select(p => p.Country)
                                      .Distinct()
                                      .ToList();

            ViewBag.CountryList = countryList;  

            return View(products);
        }
    }
}
