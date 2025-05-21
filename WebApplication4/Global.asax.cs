using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication4.BusinessLogic.DBModel.Seed;  
using WebApplication4.Domain.Entities; 

namespace WebApplication4
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
          
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

         
        }
    }
}

