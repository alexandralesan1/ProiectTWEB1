﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        public void AddFavoriteItem(DBFavoriteItemsTable favoriteItem)
        {
            _context.FavoriteItems.Add(favoriteItem);
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


        public List<DBProductTable> SearchProducts(string query)
        {
            return _context.Products
                .Where(p =>
                    p.Name.Contains(query) ||
                    p.Description.Contains(query) ||
                    p.Brand.ToString().Contains(query))
                .ToList();
        }


        public DBProductTable GetProductByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name == name);
        }

    }

}
