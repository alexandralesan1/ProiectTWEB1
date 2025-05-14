using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Controllers
{
    public class FilterController : Controller
    {
        private readonly ProductService _productService;

        public FilterController()
        {
            _productService = new ProductService();
        }

        public ActionResult Filter(
            string category,
            string[] selectedBrands,
            string[] selectedCategories,
            string[] selectedCountries,
            string[] selectedSpecialPromotions)
        {
            var allProducts = _productService.GetFilteredProducts(
                category,
                selectedBrands,
                selectedCategories,
                selectedCountries,
                selectedSpecialPromotions
            );

            ViewBag.BrandList = allProducts.Select(p => p.Brand.ToString()).Distinct().ToList();
            ViewBag.CategoryList = allProducts.Select(p => p.Category.ToString()).Distinct().ToList();
            ViewBag.CountryList = allProducts.Select(p => p.Country.ToString()).Distinct().ToList();
            ViewBag.SpecialPromotionList = allProducts.Select(p => p.SpecialCategory.ToString()).Distinct().ToList();

            ViewBag.SelectedCategory = category;

            return View(allProducts);
        }
    }
}
