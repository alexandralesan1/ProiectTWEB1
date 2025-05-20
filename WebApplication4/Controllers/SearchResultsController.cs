using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Controllers
{
    public class SearchResultsController : Controller
    {
        private readonly ProductService _productService = new ProductService();

        public ActionResult SearchResults(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Introduceți un termen pentru căutare.";
                return View(new List<DBProductTable>());
            }

            var results = _productService.SearchProducts(query);
            return View(results);
        }
    }
}
