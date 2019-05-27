namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notificacions",
                c => new
                    {
                        Noti_Id = c.Int(nullable: false, identity: true),
                        Noti_Url = c.String(),
                        Noti_Inicio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Noti_Leido = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Noti_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Usersplandetrabajoes",
                c => new
                    {
                        Uspl_Id = c.Int(nullable: false, identity: true),
                        Plat_Id = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Uspl_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.PlandeTrabajoes", t => t.Plat_Id)
                .Index(t => t.Plat_Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PlandeTrabajoes",
                c => new
                    {
                        Plat_Id = c.Int(nullable: false, identity: true),
                        Plat_Nom = c.String(),
                    })
                .PrimaryKey(t => t.Plat_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usersplandetrabajoes", "Plat_Id", "dbo.PlandeTrabajoes");
            DropForeignKey("dbo.Usersplandetrabajoes", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notificacions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Usersplandetrabajoes", new[] { "Id" });
            DropIndex("dbo.Usersplandetrabajoes", new[] { "Plat_Id" });
            DropIndex("dbo.Notificacions", new[] { "UserId" });
            DropTable("dbo.PlandeTrabajoes");
            DropTable("dbo.Usersplandetrabajoes");
            DropTable("dbo.Notificacions");
        }
    }
}
