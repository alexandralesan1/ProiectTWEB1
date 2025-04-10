using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public DBProductTable Product { get; set; }
        public int Quantity { get; set; }
        public decimal FinalPrice => Product.Price * Quantity;
    }
}
