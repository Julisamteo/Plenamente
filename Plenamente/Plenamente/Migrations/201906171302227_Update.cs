namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Usersplandetrabajoes", newName: "UsuariosPlandetrabajoes");
            DropIndex("dbo.ActiCumplimientoes", new[] { "ProgamacionTareas_Id" });
            RenameColumn(table: "dbo.ProgamacionTareas", name: "ProgamacionTareas_Id", newName: "ActiCumplimiento_Id");
            CreateIndex("dbo.ProgamacionTareas", "ActiCumplimiento_Id");
            DropColumn("dbo.ActiCumplimientoes", "ProgamacionTareas_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActiCumplimientoes", "ProgamacionTareas_Id", c => c.Int());
            DropIndex("dbo.ProgamacionTareas", new[] { "ActiCumplimiento_Id" });
            RenameColumn(table: "dbo.ProgamacionTareas", name: "ActiCumplimiento_Id", newName: "ProgamacionTareas_Id");
            CreateIndex("dbo.ActiCumplimientoes", "ProgamacionTareas_Id");
            RenameTable(name: "dbo.UsuariosPlandetrabajoes", newName: "Usersplandetrabajoes");
        }
    }
}
