using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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


            if (!_dbContext.Roles.Any())
            {
                _dbContext.Roles.Add(new DBRoleTable { Id = 1, Name = "Admin" });
                _dbContext.Roles.Add(new DBRoleTable { Id = 2, Name = "Client" });
                _dbContext.Roles.Add(new DBRoleTable { Id = 3, Name = "Guest" });
                _dbContext.SaveChanges();
            }
        }

        public void AddUser(DBUserTable user, string role = "Guest")
        {
            user.Password = HashGen(user.Password);
            user.Role = role;   
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



        //private string HashPassword(string password)
        //{
        //    using (var sha256 = System.Security.Cryptography.SHA256.Create())
        //    {
        //        var bytes = Encoding.UTF8.GetBytes(password);
        //        var hash = sha256.ComputeHash(bytes);
        //        return Convert.ToBase64String(hash);
        //    }
        //}

        //private bool VerifyPassword(string inputPassword, string storedPassword)
        //{
        //    var inputHash = HashPassword(inputPassword);
        //    return inputHash == storedPassword; 
        //}


        //public DBUserTable Authenticate(string email, string password)
        //{
        //    var user = _dbContext.Users.FirstOrDefault(u => u.Email == email); if (user != null && VerifyPassword(password, user.Password))
        //    {
        //        return user; 
        //    }

        //    return null;
        //    ;
        //}


        //public void UpdatePassword(int id, string newPassword)
        //{
        //    var user = GetUserById(id);
        //    if (user != null)
        //    {
        //        user.Password = HashPassword(newPassword); 
        //        _dbContext.SaveChanges();
        //    }
        //}



        public static string HashGen(string password)
        {
           
            using (MD5 md5 = MD5.Create())
            {
                var originalBytes = Encoding.Default.GetBytes(password + "twutm2025");
                var encodedBytes = md5.ComputeHash(originalBytes);

                
                return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
            }
        }

        public DBUserTable AuthenticateAndUpdateLogin(string email, string password, string loginIp, DateTime loginDateTime)
        {
            DBUserTable result;

            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!validate.IsValid(email))
            {
               
                return null; 
            }

          
            var pass = HashGen(password);

           
            using (var db = new ShopDBContext()) 
            {
                result = db.Users.FirstOrDefault(u => u.Email == email && u.Password == pass);
            }

           
            if (result == null)
            {
               
                return null;
            }

           
            using (var db = new ShopDBContext()) 
            {
                result.LasIp = loginIp;
                result.LastLogin = loginDateTime;

               
                db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

         
            return result; 
        }




    }
}