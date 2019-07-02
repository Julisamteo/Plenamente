namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredColumns : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            AlterColumn("dbo.Empresas", "Empr_Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_Dir", c => c.String(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_telefono", c => c.String(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_RepresentanteLegal", c => c.String(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_ResponsableSST", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Nom1", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Apel1", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Dir", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Cemeg", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Pers_Cemeg", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Pers_Dir", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Pers_Apel1", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Pers_Nom1", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_ResponsableSST", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_RepresentanteLegal", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_telefono", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_Dir", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_Nom", c => c.String());
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
        }
    }
}
