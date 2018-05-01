namespace Echri3endy_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditeProduit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produits", "prix", c => c.Double(nullable: false));
            AddColumn("dbo.Produits", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Produits", "UserID");
            AddForeignKey("dbo.Produits", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produits", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Produits", new[] { "UserID" });
            DropColumn("dbo.Produits", "UserID");
            DropColumn("dbo.Produits", "prix");
        }
    }
}
