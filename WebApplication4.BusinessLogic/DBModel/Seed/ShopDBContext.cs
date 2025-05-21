using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Domain.Entities;
using WebApplication4.BusinessLogic.Core;

namespace WebApplication4.BusinessLogic.DBModel.Seed
{
    public class ShopDBContext : DbContext
    {
        public DbSet<DBProductTable> Products { get; set; }
        public DbSet<DBUserTable> Users { get; set; }
        public DbSet<DBFavoriteItemsTable> FavoriteItems { get; set; }

        public DbSet<DBSessionTable> Session { get; set; }

        public DbSet<DBRoleTable> Roles { get; set; }

        public DbSet <DBCartItemsTable> CartItems { get; set;}


        public ShopDBContext() : base("name=ShopDatabase") { }
        

    }
}
