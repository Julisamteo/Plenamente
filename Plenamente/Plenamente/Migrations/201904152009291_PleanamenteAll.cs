namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PleanamenteAll : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Empresa_Empr_Nit" });
            DropIndex("dbo.AspNetUsers", new[] { "AreaEmpresa_Aemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Arl_Arl_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CargoEmpresa_Cemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "JornadaEmpresa_Jemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "SedeCiudad_Sciu_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Ciudad_Ciud_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Afp_Afp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CateLicencia_Cate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Eps_Eps_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "EstadoPersona_Espe_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Genero_Gene_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "TipoDocumento_Tdoc_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "TipoVinculacion_Tvin_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Empresa_Empr_Nit", newName: "Empr_Nit");
            RenameColumn(table: "dbo.AspNetUsers", name: "AreaEmpresa_Aemp_Id", newName: "Aemp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Arl_Arl_Id", newName: "Arl_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "CargoEmpresa_Cemp_Id", newName: "Cemp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "JornadaEmpresa_Jemp_Id", newName: "Jemp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "SedeCiudad_Sciu_Id", newName: "Sciu_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Ciudad_Ciud_Id", newName: "Ciud_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Afp_Afp_Id", newName: "Afp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "CateLicencia_Cate_Id", newName: "Cate_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Eps_Eps_Id", newName: "Eps_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "EstadoPersona_Espe_Id", newName: "Espe_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Genero_Gene_Id", newName: "Gene_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "TipoDocumento_Tdoc_Id", newName: "Tdoc_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "TipoVinculacion_Tvin_Id", newName: "Tvin_Id");
            AlterColumn("dbo.AspNetUsers", "Empr_Nit", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Aemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Arl_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Jemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Sciu_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Ciud_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Afp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cate_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Eps_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Espe_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Gene_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Tvin_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
            CreateIndex("dbo.AspNetUsers", "Sciu_Id");
            CreateIndex("dbo.AspNetUsers", "Ciud_Id");
            CreateIndex("dbo.AspNetUsers", "Cemp_Id");
            CreateIndex("dbo.AspNetUsers", "Aemp_Id");
            CreateIndex("dbo.AspNetUsers", "Cate_Id");
            CreateIndex("dbo.AspNetUsers", "Gene_Id");
            CreateIndex("dbo.AspNetUsers", "Jemp_Id");
            CreateIndex("dbo.AspNetUsers", "Tvin_Id");
            CreateIndex("dbo.AspNetUsers", "Eps_Id");
            CreateIndex("dbo.AspNetUsers", "Afp_Id");
            CreateIndex("dbo.AspNetUsers", "Arl_Id");
            CreateIndex("dbo.AspNetUsers", "Empr_Nit");
            CreateIndex("dbo.AspNetUsers", "Espe_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Espe_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Empr_Nit" });
            DropIndex("dbo.AspNetUsers", new[] { "Arl_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Afp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Eps_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Tvin_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Jemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Gene_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Aemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Ciud_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Sciu_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            AlterColumn("dbo.AspNetUsers", "Tvin_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Gene_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Espe_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Eps_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Cate_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Afp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Ciud_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Sciu_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Jemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Cemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Arl_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Aemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Empr_Nit", c => c.Int());
            RenameColumn(table: "dbo.AspNetUsers", name: "Tvin_Id", newName: "TipoVinculacion_Tvin_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Tdoc_Id", newName: "TipoDocumento_Tdoc_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Gene_Id", newName: "Genero_Gene_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Espe_Id", newName: "EstadoPersona_Espe_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Eps_Id", newName: "Eps_Eps_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Cate_Id", newName: "CateLicencia_Cate_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Afp_Id", newName: "Afp_Afp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Ciud_Id", newName: "Ciudad_Ciud_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Sciu_Id", newName: "SedeCiudad_Sciu_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Jemp_Id", newName: "JornadaEmpresa_Jemp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Cemp_Id", newName: "CargoEmpresa_Cemp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Arl_Id", newName: "Arl_Arl_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Aemp_Id", newName: "AreaEmpresa_Aemp_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Empr_Nit", newName: "Empresa_Empr_Nit");
            CreateIndex("dbo.AspNetUsers", "TipoVinculacion_Tvin_Id");
            CreateIndex("dbo.AspNetUsers", "TipoDocumento_Tdoc_Id");
            CreateIndex("dbo.AspNetUsers", "Genero_Gene_Id");
            CreateIndex("dbo.AspNetUsers", "EstadoPersona_Espe_Id");
            CreateIndex("dbo.AspNetUsers", "Eps_Eps_Id");
            CreateIndex("dbo.AspNetUsers", "CateLicencia_Cate_Id");
            CreateIndex("dbo.AspNetUsers", "Afp_Afp_Id");
            CreateIndex("dbo.AspNetUsers", "Ciudad_Ciud_Id");
            CreateIndex("dbo.AspNetUsers", "SedeCiudad_Sciu_Id");
            CreateIndex("dbo.AspNetUsers", "JornadaEmpresa_Jemp_Id");
            CreateIndex("dbo.AspNetUsers", "CargoEmpresa_Cemp_Id");
            CreateIndex("dbo.AspNetUsers", "Arl_Arl_Id");
            CreateIndex("dbo.AspNetUsers", "AreaEmpresa_Aemp_Id");
            CreateIndex("dbo.AspNetUsers", "Empresa_Empr_Nit");
        }
    }
}
