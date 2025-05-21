using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.BusinessLogic.Core;
using WebApplication4.Domain.Entities;

namespace WebApplication4.BusinessLogic.Interfaces
{
     public interface ICartServices
     {

       List<DBCartItemsTable> GetCartItems(int userId);

          decimal GetCartTotal(int userId);
          void AddToCart(int userId, int productId);
          void RemoveFromCart(int userId, int productId);
          void UpdateCartItem(int userId, int productId, int quantity);
          decimal GetItemFinalPrice(int userId, int productId);
          int? GetLoggedInUserId();
 

     }
}
