namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPlantrabajo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usersplandetrabajoes", "Acum_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Usersplandetrabajoes", "Acum_Id");
            AddForeignKey("dbo.Usersplandetrabajoes", "Acum_Id", "dbo.ActiCumplimientoes", "Acum_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usersplandetrabajoes", "Acum_Id", "dbo.ActiCumplimientoes");
            DropIndex("dbo.Usersplandetrabajoes", new[] { "Acum_Id" });
            DropColumn("dbo.Usersplandetrabajoes", "Acum_Id");
        }
    }
}
