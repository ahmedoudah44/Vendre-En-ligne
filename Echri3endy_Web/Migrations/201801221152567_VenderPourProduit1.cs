namespace Echri3endy_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VenderPourProduit1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.VenderPourProduits", new[] { "userId" });
            CreateIndex("dbo.VenderPourProduits", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.VenderPourProduits", new[] { "UserId" });
            CreateIndex("dbo.VenderPourProduits", "userId");
        }
    }
}
