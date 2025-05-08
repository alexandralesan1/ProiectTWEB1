
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.BusinessLogic.Interfaces;
using WebApplication4.Domain.Entities;


namespace WebApplication4.BusinessLogic.Core
{

    public class ProductService: IProductService
    {
        private readonly ShopDBContext _context = new ShopDBContext();


        public List<DBProductTable> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public DBProductTable GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(DBProductTable product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public List<DBProductTable> GetFilteredProducts(string category)
        {
            return _context.Products
                .Where(p => p.Category.ToString() == category)
                .ToList();
        }

    }

}
