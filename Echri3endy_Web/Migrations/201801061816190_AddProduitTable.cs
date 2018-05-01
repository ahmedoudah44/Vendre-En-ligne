namespace Echri3endy_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProduitTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProduitTiter = c.String(),
                        ProduitConteni = c.String(),
                        ProduitImager = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produits", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Produits", new[] { "CategoryId" });
            DropTable("dbo.Produits");
        }
    }
}
