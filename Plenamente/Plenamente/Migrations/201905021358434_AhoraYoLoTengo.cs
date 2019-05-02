namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AhoraYoLoTengo : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ActiCumplimientoes", name: "ApplicationUser_Id", newName: "Id");
            RenameColumn(table: "dbo.Cumplimientoes", name: "ApplicationUser_Id", newName: "Id");
            RenameColumn(table: "dbo.Resultadoes", name: "ApplicationUser_Id", newName: "Id");
            RenameIndex(table: "dbo.ActiCumplimientoes", name: "IX_ApplicationUser_Id", newName: "IX_Id");
            RenameIndex(table: "dbo.Cumplimientoes", name: "IX_ApplicationUser_Id", newName: "IX_Id");
            RenameIndex(table: "dbo.Resultadoes", name: "IX_ApplicationUser_Id", newName: "IX_Id");
            AddColumn("dbo.Cumplimientoes", "Cump_Nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cumplimientoes", "Cump_Nombre");
            RenameIndex(table: "dbo.Resultadoes", name: "IX_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Cumplimientoes", name: "IX_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.ActiCumplimientoes", name: "IX_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Resultadoes", name: "Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Cumplimientoes", name: "Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.ActiCumplimientoes", name: "Id", newName: "ApplicationUser_Id");
        }
    }
}
