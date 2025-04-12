using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;



namespace WebApplication4.BusinessLogic.Core
{
    public class UserService
    {
        private readonly ShopDBContext _dbContext;

        public UserService()
        {
            _dbContext = new ShopDBContext();
        }

        public void AddUser(DBUserTable user)
        {
            user.Password = HashPassword(user.Password);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        
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



        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            var inputHash = HashPassword(inputPassword);
            return inputHash == storedPassword; 
        }


        public DBUserTable Authenticate(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email); if (user != null && VerifyPassword(password, user.Password))
            {
                return user; 
            }

            return null;
            ;
        }


        public void UpdatePassword(int id, string newPassword)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.Password = HashPassword(newPassword); 
                _dbContext.SaveChanges();
            }
        }

        




    }
}