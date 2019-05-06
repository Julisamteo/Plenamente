namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionSedeCiudad : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SedeCiudads", new[] { "Empresa_Empr_Nit" });
            RenameColumn(table: "dbo.SedeCiudads", name: "Empresa_Empr_Nit", newName: "Empr_Nit");
            AlterColumn("dbo.SedeCiudads", "Empr_Nit", c => c.Int(nullable: false));
            CreateIndex("dbo.SedeCiudads", "Empr_Nit");
            DropColumn("dbo.SedeCiudads", "Empr_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SedeCiudads", "Empr_Id", c => c.Int(nullable: false));
            DropIndex("dbo.SedeCiudads", new[] { "Empr_Nit" });
            AlterColumn("dbo.SedeCiudads", "Empr_Nit", c => c.Int());
            RenameColumn(table: "dbo.SedeCiudads", name: "Empr_Nit", newName: "Empresa_Empr_Nit");
            CreateIndex("dbo.SedeCiudads", "Empresa_Empr_Nit");
        }
    }
}
