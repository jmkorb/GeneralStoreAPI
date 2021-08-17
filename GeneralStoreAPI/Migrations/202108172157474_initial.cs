namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Sku", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "CustomerId");
            CreateIndex("dbo.Transactions", "Sku");
            AddForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "Sku", "dbo.Products", "Sku");
            DropColumn("dbo.Transactions", "ProductSKU");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "ProductSKU", c => c.String());
            DropForeignKey("dbo.Transactions", "Sku", "dbo.Products");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "Sku" });
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropColumn("dbo.Transactions", "Sku");
        }
    }
}
