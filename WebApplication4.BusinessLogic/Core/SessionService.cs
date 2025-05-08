using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Domain.Entities;
using WebApplication4.Helpers;

namespace WebApplication4.BusinessLogic.Core
{

     public class SessionService: ISessionService
     {
          private readonly ShopDBContext _context = new ShopDBContext();

          public void StoreSession(string email, string cookieValue)
          {
               var existingSession = _context.Session.FirstOrDefault(s => s.Username == email);

               if (existingSession != null)
               {
                    existingSession.CookieString = cookieValue;
                    existingSession.ExpireTime = DateTime.Now.AddMinutes(60);
                    _context.SaveChanges();
               }
               else
               {
                    _context.Session.Add(new DBSessionTable
                    {
                         Username = email,
                         CookieString = cookieValue,
                         ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    _context.SaveChanges();
               }
          }

          public DBUserTable ValidateSession(string cookieValue)
          {
               var session = _context.Session.FirstOrDefault(s => s.CookieString == cookieValue);
               return session != null && session.ExpireTime > DateTime.Now ?
                      _context.Users.FirstOrDefault(u => u.Email == session.Username) : null;
          }

          public void Logout(string v)
          {
               HttpContext.Current.Session.Clear();
          }
     }
}
