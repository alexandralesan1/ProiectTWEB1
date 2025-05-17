
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

        public void AddFavoriteItem(DBFavoriteItemsTable favoriteItem)
        {
            _context.Favorites.Add(favoriteItem);
            _context.SaveChanges();
        }
        public List<DBProductTable> GetFilteredProducts(
      string category,
      string[] selectedBrands,
      string[] selectedCategories,
      string[] selectedCountries,
      string[] selectedSpecialPromotions)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category.ToString() == category);

            if (selectedBrands?.Length > 0)
                products = products.Where(p => selectedBrands.Contains(p.Brand.ToString()));

            if (selectedCategories?.Length > 0)
                products = products.Where(p => selectedCategories.Contains(p.Category.ToString()));

            if (selectedCountries?.Length > 0)
                products = products.Where(p => selectedCountries.Contains(p.Country.ToString()));

            if (selectedSpecialPromotions?.Length > 0)
                products = products.Where(p => selectedSpecialPromotions.Contains(p.SpecialCategory.ToString()));

            return products.ToList();
        }


    }

}
