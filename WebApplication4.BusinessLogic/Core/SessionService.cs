using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;
using WebApplication4.Helpers;

namespace WebApplication4.BusinessLogic.Core
{

     public class SessionService: ISessionService
     {
          private readonly ShopDBContext _context = new ShopDBContext();

          public void StoreSession(string email, string cookieValue)
          {
               var existingSession = _context.Session.FirstOrDefault(s => s.Email == email);

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
                         Email = email,
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
                      _context.Users.FirstOrDefault(u => u.Email == session.Email) : null;
          }
          public string RetrieveSession(string cookieValue)
          {
               if (string.IsNullOrEmpty(cookieValue)) return null; 

               var session = _context.Session.FirstOrDefault(s => s.CookieString == cookieValue);

               if (session == null)
               {
                    Console.WriteLine("Session not found for cookie: " + cookieValue);
                    return null;
               }

               if (session.ExpireTime <= DateTime.Now)
               {
                    Console.WriteLine("Session expired for user: " + session.Email);
                    return null;
               }

               return session.Email;
          }
          public void Logout(string v)
          {
               HttpContext.Current.Session.Clear();
          }


          public int? GetLoggedInUserId()
          {
               var authCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
               if (authCookie == null) return null; 

               var userEmail = RetrieveSession(authCookie.Value);
               if (string.IsNullOrEmpty(userEmail)) return null; 

               var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
               return user?.Id;
          }
     }
}
