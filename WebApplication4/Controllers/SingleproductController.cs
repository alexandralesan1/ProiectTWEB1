using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.BusinessLogic.Core;

namespace WebApplication4.Controllers
{
    public class SingleproductController : Controller
    {
        private readonly ProductService _productService = new ProductService();

        public ActionResult Singleproduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new HttpStatusCodeResult(400, "Numele produsului este invalid");
            }

            var product = _productService.GetProductByName(name);
            if (product == null)
            {
                return HttpNotFound($"Produsul '{name}' nu a fost găsit.");
            }

            return View(product);
        }
    }

}
