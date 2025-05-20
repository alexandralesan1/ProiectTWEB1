namespace WebApplication4.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBFavoriteItemsTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DBProductTables", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.DBProductTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(nullable: false),
                        Category = c.Int(nullable: false),
                        Brand = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        SpecialCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DBRoleTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DBUserTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        IsBlocked = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                        DBRoleTable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DBRoleTables", t => t.DBRoleTable_Id)
                .Index(t => t.DBRoleTable_Id);
            
            CreateTable(
                "dbo.DBSessionTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        CookieString = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        ExpireTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBUserTables", "DBRoleTable_Id", "dbo.DBRoleTables");
            DropForeignKey("dbo.DBFavoriteItemsTables", "Product_Id", "dbo.DBProductTables");
            DropIndex("dbo.DBUserTables", new[] { "DBRoleTable_Id" });
            DropIndex("dbo.DBFavoriteItemsTables", new[] { "Product_Id" });
            DropTable("dbo.DBSessionTables");
            DropTable("dbo.DBUserTables");
            DropTable("dbo.DBRoleTables");
            DropTable("dbo.DBProductTables");
            DropTable("dbo.DBFavoriteItemsTables");
        }
    }
}
