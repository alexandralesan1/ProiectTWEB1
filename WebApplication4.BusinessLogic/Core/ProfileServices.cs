using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication4.BusinessLogic.DBModel.Seed;
using WebApplication4.Domain.Entities;

namespace WebApplication4.BusinessLogic.Core
{
     public class ProfileServices
     {
          private readonly ShopDBContext _context;

          public ProfileServices(ShopDBContext context)
          {
               _context = context ?? throw new ArgumentNullException(nameof(context));
          }
          public void UpdateUserProfile(DBUserTable updatedUser, HttpPostedFileBase profileImage)
          {
               var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
               if (user != null)
               {
                    user.Name = updatedUser.Name;
                    user.Email = updatedUser.Email;
                    user.Phone = updatedUser.Phone;

                    _context.SaveChanges();
               }
          }
     }
}
