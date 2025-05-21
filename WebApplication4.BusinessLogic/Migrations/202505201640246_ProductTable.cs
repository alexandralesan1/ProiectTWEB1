namespace WebApplication4.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DBProductTables", "InStock", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DBProductTables", "InStock");
        }
    }
}
