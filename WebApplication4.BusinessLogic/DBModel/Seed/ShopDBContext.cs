using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Entities;

namespace WebApplication4.BusinessLogic.DBModel.Seed
{
    public class ShopDBContext : DbContext
    {
        public DbSet<DBProductTable> Products { get; set; }
        public DbSet<DBUserTable> Users { get; set; }

        public DbSet<DBFavProductTable> Favorites { get; set; }

        public ShopDBContext() : base("name=ShopDatabase") { }
        

    }
}
