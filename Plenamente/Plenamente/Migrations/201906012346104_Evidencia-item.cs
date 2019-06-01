namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Evidenciaitem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemEvidencias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Empresa_Empr_Nit = c.Int(nullable: false),
                        ItemEstandar_Iest_Id = c.Int(nullable: false),
                       
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.Empresa_Empr_Nit)
                .ForeignKey("dbo.ItemEstandars", t => t.ItemEstandar_Iest_Id)
                .Index(t => t.Empresa_Empr_Nit)
                .Index(t => t.ItemEstandar_Iest_Id);
            
            CreateTable(
                "dbo.ItemEvidenciaUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemEvidencia_Id = c.Int(nullable: false),
                        Url = c.String(),                       
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemEvidencias", t => t.ItemEvidencia_Id)
                .Index(t => t.ItemEvidencia_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemEvidenciaUrls", "ItemEvidencia_Id", "dbo.ItemEvidencias");
            DropForeignKey("dbo.ItemEvidencias", "ItemEstandar_Iest_Id", "dbo.ItemEstandars");
            DropForeignKey("dbo.ItemEvidencias", "Empresa_Empr_Nit", "dbo.Empresas");
            DropIndex("dbo.ItemEvidenciaUrls", new[] { "ItemEvidencia_Id" });
            DropIndex("dbo.ItemEvidencias", new[] { "ItemEstandar_Iest_Id" });
            DropIndex("dbo.ItemEvidencias", new[] { "Empresa_Empr_Nit" });
            DropTable("dbo.ItemEvidenciaUrls");
            DropTable("dbo.ItemEvidencias");
        }
    }
}
