using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;
using WebApplication4.Domain.Enums;
using WebApplication4.BusinessLogic.Interfaces;
using System.IO;


namespace WebApplication4.BusinessLogic.Core
{
     public class AdminServices: IAdminServices
     {
          private readonly ShopDBContext _context;

          public AdminServices(ShopDBContext context)
          {
               _context = context ?? throw new ArgumentNullException(nameof(context));
          }
          public void BlockUser(int userId)
          {
               var user = _context.Users.FirstOrDefault(u => u.Id == userId);
               if (user != null)
               {
                    user.IsBlocked = true;
                    _context.SaveChanges();
               }
          }

          public void UnBlockUser(int userId)
          {
               var user = _context.Users.FirstOrDefault(u => u.Id == userId);
               if (user != null)
               {
                    user.IsBlocked = false;
                    _context.SaveChanges();
               }
          }

          public void DeleteUser(int userId)
          {
               var user = _context.Users.FirstOrDefault(u => u.Id == userId);
               if (user != null)
               {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
               }
          }

          public void AddProduct(DBProductTable product, HttpPostedFileBase imageFile)
          {
               if (imageFile != null && imageFile.ContentLength > 0)
               {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(imageFile.FileName);
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/") + fileName;
                    imageFile.SaveAs(path);
                    product.ImageUrl = "/Content/images/" + fileName;
               }

               product.InStock = true;
               _context.Products.Add(product);
               _context.SaveChanges(); 
          }
          public void DeleteProduct(int productId)
          {
               var product = _context.Products.FirstOrDefault(p => p.Id == productId);
               if (product != null)
               {
                    string imagePath = System.Web.HttpContext.Current.Server.MapPath(product.ImageUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                         System.IO.File.Delete(imagePath);
                    }

                    _context.Products.Remove(product);
                    _context.SaveChanges();
               }
          }

          public List<DBUserTable> GetUsers()
          {
               return _context.Users.ToList();
          }

          public List<DBProductTable> GetProducts()
          {
               return _context.Products.ToList(); 
          }

          public DBUserTable GetAdminByEmail(string email)
          {
               return _context.Users.FirstOrDefault(u => u.Email == email && u.Role == UserRole.Admin);
          }


          public void UpdateProduct(DBProductTable updatedProduct, HttpPostedFileBase imageFile)
          {
               var product = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
               if (product != null)
               {
                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;
                    product.Description = updatedProduct.Description;
                    product.Category = updatedProduct.Category;
                    product.Brand = updatedProduct.Brand;
                    product.Country = updatedProduct.Country;
                    product.SpecialCategory = updatedProduct.SpecialCategory;

           
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                         string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(imageFile.FileName);
                         string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/") + fileName;
                         imageFile.SaveAs(path);

                   
                         if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(product.ImageUrl)))
                         {
                              System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(product.ImageUrl));
                         }

                         product.ImageUrl = "/Content/images/" + fileName;
                    }

                    _context.SaveChanges();
               }
          }

          public DBProductTable GetProductById(int id)
          {
               return _context.Products.FirstOrDefault(u => u.Id == id);
          }

          public void UpdateAdminProfile(DBUserTable updatedAdmin)
          {
               var admin = _context.Users.FirstOrDefault(u => u.Id == updatedAdmin.Id);
               if (admin != null)
               {
                    admin.Name = updatedAdmin.Name;
                    admin.Email = updatedAdmin.Email;
                    _context.SaveChanges();
               }
          }


          public void AddNews(string title, string content, HttpPostedFileBase imageFile)
          {
               string imageUrl = null;

               if (imageFile != null && imageFile.ContentLength > 0)
               {
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string folderPath = HttpContext.Current.Server.MapPath("~/Content/news/");
                    Directory.CreateDirectory(folderPath);
                    string fullPath = Path.Combine(folderPath, fileName);
                    imageFile.SaveAs(fullPath);

                    imageUrl = "/Content/news/" + fileName;
               }

               var news = new DBNewsTable
               {
                    Title = title,
                    Content = content,
                    CreatedAt = DateTime.Now,
                    ImageUrl = imageUrl
               };

               _context.News.Add(news);
               _context.SaveChanges();
          }

          public List<DBNewsTable> GetAllNews()
          {
               return _context.News.OrderByDescending(n => n.CreatedAt).ToList();
          }

          public void DeleteNews(int id)
          {
               var news = _context.News.FirstOrDefault(n => n.Id == id);
               if (news != null)
               {
                    if (!string.IsNullOrEmpty(news.ImageUrl))
                    {
                         string path = System.Web.HttpContext.Current.Server.MapPath(news.ImageUrl);
                         if (System.IO.File.Exists(path))
                              System.IO.File.Delete(path);
                    }

                    _context.News.Remove(news);
                    _context.SaveChanges();
               }
          }
     }
}
