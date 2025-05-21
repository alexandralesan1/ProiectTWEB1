using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;

namespace WebApplication4.BusinessLogic.Interfaces
{
     public interface IUserService
     {
          void AddUser(DBUserTable user);
          DBUserTable Authenticate(string email, string password);
          DBUserTable GetUserById(int id);
          void BlockUser(int id);
          void DeleteUser(int id);
          DBUserTable GetUserByEmail(string email);
     }
}
