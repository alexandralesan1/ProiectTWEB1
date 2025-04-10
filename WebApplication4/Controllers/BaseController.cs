using System.Linq;
using System.Web.Mvc;
using WebApplication4.Controllers;

public class BaseController : Controller
{
     protected override void OnActionExecuting(ActionExecutingContext filterContext)
     {
          decimal cartTotalPrice = CartController.CartItems.Sum(item => item.FinalPrice);
          ViewBag.CartTotalPrice = cartTotalPrice; 
          base.OnActionExecuting(filterContext);
     }
}