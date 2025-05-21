using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;
using WebApplication4.Helpers;




namespace WebApplication4.BusinessLogic.Core
{
    public class UserService: IUserService
    {
        private readonly ShopDBContext _dbContext = new ShopDBContext();
        public void AddUser(DBUserTable user)
        {
          user.IsBlocked = false;
          user.Password = HashHelper.GenerateHash(user.Password);
          _dbContext.Users.Add(user);
          _dbContext.SaveChanges();
        
        }

          public bool UserExists(string email, string username)
          {
               return _dbContext.Users.Any(u => u.Email == email || u.Name == username);
          }
          public DBUserTable Authenticate(string email, string password)
          {   
               var hashPassword = HashHelper.GenerateHash(password);
               return _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == hashPassword);
          }

          public DBUserTable GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

          public void BlockUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.IsBlocked = true; 
                _dbContext.SaveChanges();
            }
        }

        
        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public DBUserTable GetUserByEmail(string email)
        {

            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }



        

    }
}