using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using System.Web.HttpContext;
namespace WebApplication4.BusinessLogic.DBModel.AuthSession
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            // Verifică dacă utilizatorul e autentificat
            if (!user.Identity.IsAuthenticated)
            {
                // Redirecționează la login dacă nu e autentificat
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }

            // Verifică dacă e în rolul Admin sau Moderator
            if (!user.IsInRole("Admin") && !user.IsInRole("Moderator"))
            {
                // Redirecționează la o pagină de acces restricționat
                filterContext.Result = new RedirectResult("/Account/AccessDenied");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }

}
