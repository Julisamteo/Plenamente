namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tengoElPoderAbsolutoYMeLaPela : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ActiCumplimientoes", name: "UserId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.ActiCumplimientoes", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ActiCumplimientoes", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.ActiCumplimientoes", name: "ApplicationUser_Id", newName: "UserId");
        }
    }
}
