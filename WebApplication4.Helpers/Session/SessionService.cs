using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Helpers.Session
{

    public static class SessionService
    {
        public static void CreateUserSession(DBUserTable user)
        {
            HttpCookie cookie = new HttpCookie("UserSession");
            cookie["UserId"] = user.Id.ToString();
            cookie.Expires = DateTime.Now.AddHours(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}

  

