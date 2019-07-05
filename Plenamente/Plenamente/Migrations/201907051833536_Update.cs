namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            AddColumn("dbo.Empresas", "Empr_NewNit", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "Empr_RepresentanteLegal", c => c.String(nullable: false));
            AddColumn("dbo.Empresas", "Empr_CargoRepresentante", c => c.String());
            AddColumn("dbo.Empresas", "Empre_RepresentanteDoc", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "Empr_ResponsableSST", c => c.String(nullable: false));
            AddColumn("dbo.Empresas", "Empre_ResponsableDoc", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "TipoEmpresa_Id", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Pers_Cargo", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_Dir", c => c.String(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_telefono", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Nom1", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Apel1", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Dir", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Cemeg", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Empresas", "TipoEmpresa_Id");
            CreateIndex("dbo.AspNetUsers", "Pers_Doc", unique: true);
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
            AddForeignKey("dbo.Empresas", "TipoEmpresa_Id", "dbo.TipoEmpresas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empresas", "TipoEmpresa_Id", "dbo.TipoEmpresas");
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Pers_Doc" });
            DropIndex("dbo.Empresas", new[] { "TipoEmpresa_Id" });
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Pers_Cemeg", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Pers_Dir", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Pers_Apel1", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Pers_Nom1", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_telefono", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_Dir", c => c.String());
            AlterColumn("dbo.Empresas", "Empr_Nom", c => c.String());
            DropColumn("dbo.AspNetUsers", "Pers_Cargo");
            DropColumn("dbo.Empresas", "TipoEmpresa_Id");
            DropColumn("dbo.Empresas", "Empre_ResponsableDoc");
            DropColumn("dbo.Empresas", "Empr_ResponsableSST");
            DropColumn("dbo.Empresas", "Empre_RepresentanteDoc");
            DropColumn("dbo.Empresas", "Empr_CargoRepresentante");
            DropColumn("dbo.Empresas", "Empr_RepresentanteLegal");
            DropColumn("dbo.Empresas", "Empr_NewNit");
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
        }
    }
}
