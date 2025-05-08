using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Entities;

namespace WebApplication4.BusinessLogic.Interfaces
{
     public interface ISessionService
     {
          void StoreSession(string email, string cookieValue);
          DBUserTable ValidateSession(string cookieValue);
          void Logout(string v);
     }
}
