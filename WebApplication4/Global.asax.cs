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

            var context = new ShopDBContext(); 
            if (!context.Roles.Any())  
            {
                context.Roles.Add(new DBRoleTable { Id = 1, Name = "Admin" });
                context.Roles.Add(new DBRoleTable { Id = 2, Name = "Client" });
                context.Roles.Add(new DBRoleTable { Id = 3, Name = "Guest" });
                context.SaveChanges();  
            }
        }
    }
}

