namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizacionTablas : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Sciu_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Aemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Jemp_Id" });
            AlterColumn("dbo.AspNetUsers", "Pers_Licencia", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_LicVence", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Retiro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Sciu_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Aemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cate_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Jemp_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Sciu_Id");
            CreateIndex("dbo.AspNetUsers", "Cemp_Id");
            CreateIndex("dbo.AspNetUsers", "Aemp_Id");
            CreateIndex("dbo.AspNetUsers", "Cate_Id");
            CreateIndex("dbo.AspNetUsers", "Jemp_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Jemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Aemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Sciu_Id" });
            AlterColumn("dbo.AspNetUsers", "Jemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Cate_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Aemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Cemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Sciu_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Pers_Retiro", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "Pers_LicVence", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "Pers_Licencia", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Jemp_Id");
            CreateIndex("dbo.AspNetUsers", "Cate_Id");
            CreateIndex("dbo.AspNetUsers", "Aemp_Id");
            CreateIndex("dbo.AspNetUsers", "Cemp_Id");
            CreateIndex("dbo.AspNetUsers", "Sciu_Id");
        }
    }
}
