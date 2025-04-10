using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Domain.Entities
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public DBProductTable Product { get; set; }
    }
}