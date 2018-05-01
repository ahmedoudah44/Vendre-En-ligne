namespace Echri3endy_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VenderPourProduit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VenderPourProduits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Messager = c.String(),
                        DemandeVnDate = c.DateTime(nullable: false),
                        produitId = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produits", t => t.produitId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.produitId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenderPourProduits", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.VenderPourProduits", "produitId", "dbo.Produits");
            DropIndex("dbo.VenderPourProduits", new[] { "userId" });
            DropIndex("dbo.VenderPourProduits", new[] { "produitId" });
            DropTable("dbo.VenderPourProduits");
        }
    }
}
