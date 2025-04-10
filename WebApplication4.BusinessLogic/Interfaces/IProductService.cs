using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Entities;

namespace WebApplication4.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        List<DBProductTable> GetAllProducts();
        DBProductTable GetProductById(int id);
        void AddProduct(DBProductTable product);
        void DeleteProduct(int id);
    }
}