namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosCumplimientos : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cumplimientoes", new[] { "Iest_Id" });
            DropIndex("dbo.Cumplimientoes", new[] { "Empr_Id" });
            DropIndex("dbo.Cumplimientoes", new[] { "Tdca_Id" });
            RenameColumn(table: "dbo.Cumplimientoes", name: "Empr_Id", newName: "Empr_Nit");
            AlterColumn("dbo.Cumplimientoes", "Iest_Id", c => c.Int());
            AlterColumn("dbo.Cumplimientoes", "Empr_Nit", c => c.Int());
            AlterColumn("dbo.Cumplimientoes", "Tdca_Id", c => c.Int());
            CreateIndex("dbo.Cumplimientoes", "Iest_Id");
            CreateIndex("dbo.Cumplimientoes", "Empr_Nit");
            CreateIndex("dbo.Cumplimientoes", "Tdca_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cumplimientoes", new[] { "Tdca_Id" });
            DropIndex("dbo.Cumplimientoes", new[] { "Empr_Nit" });
            DropIndex("dbo.Cumplimientoes", new[] { "Iest_Id" });
            AlterColumn("dbo.Cumplimientoes", "Tdca_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Cumplimientoes", "Empr_Nit", c => c.Int(nullable: false));
            AlterColumn("dbo.Cumplimientoes", "Iest_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Cumplimientoes", name: "Empr_Nit", newName: "Empr_Id");
            CreateIndex("dbo.Cumplimientoes", "Tdca_Id");
            CreateIndex("dbo.Cumplimientoes", "Empr_Id");
            CreateIndex("dbo.Cumplimientoes", "Iest_Id");
        }
    }
}
