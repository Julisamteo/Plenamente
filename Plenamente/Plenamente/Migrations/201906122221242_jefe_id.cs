namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jefe_id : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            CreateTable(
                "dbo.ExpandedUserDTOes",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Password = c.String(),
                        Documento = c.Int(nullable: false),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                        Pers_Licencia = c.Int(),
                        Pers_LicVence = c.DateTime(precision: 7, storeType: "datetime2"),
                        Pers_Direccion = c.String(),
                        Pers_ContactoEmeg = c.String(),
                        Pers_TelefonoEmeg = c.Int(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        AccessFailedCount = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Tdoc_Id = c.Int(),
                        Sciu_Id = c.Int(),
                        Ciud_Id = c.Int(),
                        Cemp_Id = c.Int(),
                        Aemp_Id = c.Int(),
                        Cate_Id = c.Int(),
                        Gene_Id = c.Int(),
                        Jemp_Id = c.Int(),
                        Tvin_Id = c.Int(),
                        Eps_Id = c.Int(),
                        Afp_Id = c.Int(),
                        Arl_Id = c.Int(),
                        Empr_Nit = c.Int(),
                        Espe_Id = c.Int(),
                        Jefe_Id = c.String(),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            AddColumn("dbo.Afps", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.AreaEmpresas", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Arls", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.CargoEmpresas", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.CateLicencias", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Eps", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.EstadoPersonas", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Generoes", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.JornadaEmpresas", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.SedeCiudads", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Ciudads", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.TipoDocumentoes", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.TipoVinculacions", "ExpandedUserDTO_UserName", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Afps", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AreaEmpresas", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.Arls", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.CargoEmpresas", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.JornadaEmpresas", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.SedeCiudads", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.Ciudads", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.CateLicencias", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.Eps", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.EstadoPersonas", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.Generoes", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.TipoDocumentoes", "ExpandedUserDTO_UserName");
            CreateIndex("dbo.TipoVinculacions", "ExpandedUserDTO_UserName");
            AddForeignKey("dbo.Afps", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.AreaEmpresas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.Arls", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.CargoEmpresas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.CateLicencias", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.Ciudads", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.Eps", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.EstadoPersonas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.Generoes", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.AspNetUsers", "UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.JornadaEmpresas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.SedeCiudads", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.TipoDocumentoes", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
            AddForeignKey("dbo.TipoVinculacions", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes", "UserName");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoVinculacions", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.TipoDocumentoes", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.SedeCiudads", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.JornadaEmpresas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.AspNetUsers", "UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.Generoes", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.EstadoPersonas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.Eps", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.ExpandedUserDTOes", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.Ciudads", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.CateLicencias", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.CargoEmpresas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.Arls", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.AreaEmpresas", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropForeignKey("dbo.Afps", "ExpandedUserDTO_UserName", "dbo.ExpandedUserDTOes");
            DropIndex("dbo.TipoVinculacions", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.TipoDocumentoes", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.Generoes", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.EstadoPersonas", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.Eps", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.CateLicencias", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.Ciudads", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.SedeCiudads", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.JornadaEmpresas", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.CargoEmpresas", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.Arls", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.AreaEmpresas", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Afps", new[] { "ExpandedUserDTO_UserName" });
            DropIndex("dbo.ExpandedUserDTOes", new[] { "Empr_Nit" });
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.TipoVinculacions", "ExpandedUserDTO_UserName");
            DropColumn("dbo.TipoDocumentoes", "ExpandedUserDTO_UserName");
            DropColumn("dbo.Ciudads", "ExpandedUserDTO_UserName");
            DropColumn("dbo.SedeCiudads", "ExpandedUserDTO_UserName");
            DropColumn("dbo.JornadaEmpresas", "ExpandedUserDTO_UserName");
            DropColumn("dbo.Generoes", "ExpandedUserDTO_UserName");
            DropColumn("dbo.EstadoPersonas", "ExpandedUserDTO_UserName");
            DropColumn("dbo.Eps", "ExpandedUserDTO_UserName");
            DropColumn("dbo.CateLicencias", "ExpandedUserDTO_UserName");
            DropColumn("dbo.CargoEmpresas", "ExpandedUserDTO_UserName");
            DropColumn("dbo.Arls", "ExpandedUserDTO_UserName");
            DropColumn("dbo.AreaEmpresas", "ExpandedUserDTO_UserName");
            DropColumn("dbo.Afps", "ExpandedUserDTO_UserName");
            DropTable("dbo.ExpandedUserDTOes");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
        }
    }
}
