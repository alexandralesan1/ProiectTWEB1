namespace WebApplication4.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumeMigrare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBCartItemsTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DBProductTables", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.DBProductTables", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBCartItemsTables", "Product_Id", "dbo.DBProductTables");
            DropIndex("dbo.DBCartItemsTables", new[] { "Product_Id" });
            DropColumn("dbo.DBProductTables", "Description");
            DropTable("dbo.DBCartItemsTables");
        }
    }
}
