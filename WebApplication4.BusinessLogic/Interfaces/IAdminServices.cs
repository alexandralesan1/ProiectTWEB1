using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;

namespace WebApplication4.BusinessLogic.Interfaces
{
     public interface IAdminServices
     {
          void BlockUser(int userId);
          void UnBlockUser(int userId);
          void DeleteUser(int userId);
          void AddProduct(DBProductTable product, HttpPostedFileBase imageFile);
          void DeleteProduct(int productId);
          List<DBUserTable> GetUsers();
          List<DBProductTable> GetProducts();
          DBUserTable GetAdminByEmail(string email);
          void UpdateProduct(DBProductTable updatedProduct, HttpPostedFileBase imageFile);
     
         
     }
}
