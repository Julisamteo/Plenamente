namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ActiCumplimientoes",
                c => new
                    {
                        Acum_Id = c.Int(nullable: false, identity: true),
                        Acum_Desc = c.String(),
                        Acum_Porcentest = c.Single(nullable: false),
                        Acum_Ejec = c.String(),
                        Acum_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Acum_IniAct = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Acum_FinAct = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Oemp_Id = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        Peri_Id = c.Int(nullable: false),
                        Empr_Nit = c.Int(nullable: false),
                        Frec_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Acum_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.Frecuencias", t => t.Frec_Id)
                .ForeignKey("dbo.ObjEmpresas", t => t.Oemp_Id)
                .ForeignKey("dbo.Periodoes", t => t.Peri_Id)
                .Index(t => t.Oemp_Id)
                .Index(t => t.Id)
                .Index(t => t.Peri_Id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Frec_Id);
            
            CreateTable(
                "dbo.AcumMes",
                c => new
                    {
                        Acme_Id = c.Int(nullable: false, identity: true),
                        Cump_Id = c.Int(nullable: false),
                        Acum_Id = c.Int(nullable: false),
                        Mes_Id = c.Int(nullable: false),
                        Acme_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Acme_Id)
                .ForeignKey("dbo.ActiCumplimientoes", t => t.Acum_Id)
                .ForeignKey("dbo.Cumplimientoes", t => t.Cump_Id)
                .ForeignKey("dbo.Mes", t => t.Mes_Id)
                .Index(t => t.Cump_Id)
                .Index(t => t.Acum_Id)
                .Index(t => t.Mes_Id);
            
            CreateTable(
                "dbo.Cumplimientoes",
                c => new
                    {
                        Cump_Id = c.Int(nullable: false, identity: true),
                        Cump_Cumple = c.Boolean(nullable: false),
                        Cump_Nocumple = c.Boolean(nullable: false),
                        Cump_Justifica = c.Boolean(nullable: false),
                        Cump_Nojustifica = c.Boolean(nullable: false),
                        Cump_Observ = c.String(),
                        Iest_Id = c.Int(),
                        Empr_Nit = c.Int(),
                        Auev_Id = c.Int(nullable: false),
                        Cump_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Cump_Id)
                .ForeignKey("dbo.AutoEvaluacions", t => t.Auev_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.ItemEstandars", t => t.Iest_Id)
                .Index(t => t.Iest_Id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Auev_Id);
            
            CreateTable(
                "dbo.AutoEvaluacions",
                c => new
                    {
                        Auev_Id = c.Int(nullable: false, identity: true),
                        Auev_Nom = c.String(),
                        Auev_Inicio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Auev_Fin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Empr_Nit = c.Int(nullable: false),
                        AutoEvaluacion_Auev_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Auev_Id)
                .ForeignKey("dbo.AutoEvaluacions", t => t.AutoEvaluacion_Auev_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit)
                .Index(t => t.AutoEvaluacion_Auev_Id);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Empr_Nit = c.Int(nullable: false, identity: true),
                        Empr_Nom = c.String(),
                        Empr_Dir = c.String(),
                        Arl_Id = c.Int(nullable: false),
                        Carl_Id = c.Int(nullable: false),
                        Empr_Afiarl = c.Int(nullable: false),
                        Empr_Ttrabaja = c.Int(nullable: false),
                        Empr_Itrabaja = c.Int(nullable: false),
                        Empr_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Empr_Nit)
                .ForeignKey("dbo.Arls", t => t.Arl_Id)
                .ForeignKey("dbo.ClaseArls", t => t.Carl_Id)
                .Index(t => t.Arl_Id)
                .Index(t => t.Carl_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Pers_Doc = c.Int(nullable: false),
                        Pers_Nom1 = c.String(),
                        Pers_Nom2 = c.String(),
                        Pers_Apel1 = c.String(),
                        Pers_Apel2 = c.String(),
                        Pers_Licencia = c.Int(),
                        Pers_LicVence = c.DateTime(precision: 7, storeType: "datetime2"),
                        Pers_Foto = c.Binary(),
                        Pers_Ingreso = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Pers_Retiro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Pers_Dir = c.String(),
                        Pers_Cemeg = c.String(),
                        Pers_Temeg = c.Int(nullable: false),
                        Pers_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                        Jefe_Id = c.String(maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Afps", t => t.Afp_Id)
                .ForeignKey("dbo.AreaEmpresas", t => t.Aemp_Id)
                .ForeignKey("dbo.Arls", t => t.Arl_Id)
                .ForeignKey("dbo.CargoEmpresas", t => t.Cemp_Id)
                .ForeignKey("dbo.CateLicencias", t => t.Cate_Id)
                .ForeignKey("dbo.Ciudads", t => t.Ciud_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.Eps", t => t.Eps_Id)
                .ForeignKey("dbo.EstadoPersonas", t => t.Espe_Id)
                .ForeignKey("dbo.Generoes", t => t.Gene_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Jefe_Id)
                .ForeignKey("dbo.JornadaEmpresas", t => t.Jemp_Id)
                .ForeignKey("dbo.SedeCiudads", t => t.Sciu_Id)
                .ForeignKey("dbo.TipoDocumentoes", t => t.Tdoc_Id)
                .ForeignKey("dbo.TipoVinculacions", t => t.Tvin_Id)
                .Index(t => t.Tdoc_Id)
                .Index(t => t.Sciu_Id)
                .Index(t => t.Ciud_Id)
                .Index(t => t.Cemp_Id)
                .Index(t => t.Aemp_Id)
                .Index(t => t.Cate_Id)
                .Index(t => t.Gene_Id)
                .Index(t => t.Jemp_Id)
                .Index(t => t.Tvin_Id)
                .Index(t => t.Eps_Id)
                .Index(t => t.Afp_Id)
                .Index(t => t.Arl_Id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Espe_Id)
                .Index(t => t.Jefe_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Afps",
                c => new
                    {
                        Afp_Id = c.Int(nullable: false, identity: true),
                        Afp_Nom = c.String(),
                        Afp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Afp_Id);
            
            CreateTable(
                "dbo.AreaEmpresas",
                c => new
                    {
                        Aemp_Id = c.Int(nullable: false, identity: true),
                        Aemp_Nom = c.String(),
                        Empr_Nit = c.Int(nullable: false),
                        Aemp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Aemp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.Arls",
                c => new
                    {
                        Arl_Id = c.Int(nullable: false, identity: true),
                        Arl_Nom = c.String(),
                        Arl_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Arl_Id);
            
            CreateTable(
                "dbo.CargoEmpresas",
                c => new
                    {
                        Cemp_Id = c.Int(nullable: false, identity: true),
                        Cemp_Nom = c.String(),
                        Empr_Nit = c.Int(nullable: false),
                        Cemp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Cemp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.CateLicencias",
                c => new
                    {
                        Cate_Id = c.Int(nullable: false, identity: true),
                        Cate_Nom = c.String(),
                        Cate_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Cate_Id);
            
            CreateTable(
                "dbo.Ciudads",
                c => new
                    {
                        Ciud_Id = c.Int(nullable: false, identity: true),
                        Ciud_Nom = c.String(),
                        Ciud_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Ciud_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Eps",
                c => new
                    {
                        Eps_Id = c.Int(nullable: false, identity: true),
                        Eps_Nom = c.String(),
                        Eps_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Eps_Id);
            
            CreateTable(
                "dbo.EstadoPersonas",
                c => new
                    {
                        Espe_Id = c.Int(nullable: false, identity: true),
                        Espe_Nom = c.String(),
                        Espe_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Espe_Id);
            
            CreateTable(
                "dbo.Evidencias",
                c => new
                    {
                        Evid_Id = c.Int(nullable: false, identity: true),
                        Evid_Nombre = c.String(),
                        Evid_Archivo = c.String(),
                        Evid_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cump_Id = c.Int(nullable: false),
                        Tdca_id = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Evid_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Cumplimientoes", t => t.Cump_Id)
                .ForeignKey("dbo.TipoDocCargas", t => t.Tdca_id)
                .Index(t => t.Cump_Id)
                .Index(t => t.Tdca_id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TipoDocCargas",
                c => new
                    {
                        Tdca_id = c.Int(nullable: false, identity: true),
                        Tdca_Nom = c.String(),
                        Tdca_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Tdca_id);
            
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        Gene_Id = c.Int(nullable: false, identity: true),
                        Gene_Nom = c.String(),
                        Gene_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Gene_Id);
            
            CreateTable(
                "dbo.JornadaEmpresas",
                c => new
                    {
                        Jemp_Id = c.Int(nullable: false, identity: true),
                        Jemp_Nom = c.String(),
                        Empr_Nit = c.Int(nullable: false),
                        Jemp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Jemp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Resultadoes",
                c => new
                    {
                        Resu_Id = c.Int(nullable: false, identity: true),
                        Resu_Respuesta = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Encu_Id = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        Resp_Id = c.Int(nullable: false),
                        Pregunta_Preg_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Resu_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Preguntas", t => t.Pregunta_Preg_Id)
                .ForeignKey("dbo.Encuestas", t => t.Encu_Id)
                .ForeignKey("dbo.Respuestas", t => t.Resp_Id)
                .Index(t => t.Encu_Id)
                .Index(t => t.Id)
                .Index(t => t.Resp_Id)
                .Index(t => t.Pregunta_Preg_Id);
            
            CreateTable(
                "dbo.Encuestas",
                c => new
                    {
                        Encu_Id = c.Int(nullable: false, identity: true),
                        Encu_Nombre = c.String(),
                        Encu_Creacion = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Encu_Vence = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Encu_Estado = c.Boolean(nullable: false),
                        Encu_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Empr_Nit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Encu_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.Preguntas",
                c => new
                    {
                        Preg_Id = c.Int(nullable: false, identity: true),
                        Preg_Titulo = c.String(),
                        Preg_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Encu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Preg_Id)
                .ForeignKey("dbo.Encuestas", t => t.Encu_Id)
                .Index(t => t.Encu_Id);
            
            CreateTable(
                "dbo.Respuestas",
                c => new
                    {
                        Resp_Id = c.Int(nullable: false, identity: true),
                        Resp_Nom = c.String(),
                        Resp_Tipo = c.String(),
                        Resp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Quem_Id = c.Int(nullable: false),
                        Preg_Id = c.Int(nullable: false),
                        respuestasQuemadas_Quem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Resp_Id)
                .ForeignKey("dbo.Preguntas", t => t.Preg_Id)
                .ForeignKey("dbo.TipoRespuestas", t => t.respuestasQuemadas_Quem_Id)
                .Index(t => t.Preg_Id)
                .Index(t => t.respuestasQuemadas_Quem_Id);
            
            CreateTable(
                "dbo.TipoRespuestas",
                c => new
                    {
                        Quem_Id = c.Int(nullable: false, identity: true),
                        Quem_Nom = c.String(),
                        Respuesta_Resp_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Quem_Id)
                .ForeignKey("dbo.Respuestas", t => t.Respuesta_Resp_Id)
                .Index(t => t.Respuesta_Resp_Id);
            
            CreateTable(
                "dbo.SedeCiudads",
                c => new
                    {
                        Sciu_Id = c.Int(nullable: false, identity: true),
                        Sciu_Nom = c.String(),
                        Ciud_Id = c.Int(nullable: false),
                        Empr_Nit = c.Int(nullable: false),
                        Sciu_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Sciu_Id)
                .ForeignKey("dbo.Ciudads", t => t.Ciud_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Ciud_Id)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.TipoDocumentoes",
                c => new
                    {
                        Tdoc_Id = c.Int(nullable: false, identity: true),
                        Tdoc_Nom = c.String(),
                        Tdoc_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Tdoc_Id);
            
            CreateTable(
                "dbo.TipoVinculacions",
                c => new
                    {
                        Tvin_Id = c.Int(nullable: false, identity: true),
                        Tvin_Nom = c.String(),
                        Tvin_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Tvin_Id);
            
            CreateTable(
                "dbo.ClaseArls",
                c => new
                    {
                        Carl_Id = c.Int(nullable: false, identity: true),
                        Carl_Nom = c.String(),
                        Carl_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Carl_Id);
            
            CreateTable(
                "dbo.EprotEmpresas",
                c => new
                    {
                        Epem_Id = c.Int(nullable: false, identity: true),
                        Empr_Nit = c.Int(nullable: false),
                        Epro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Epem_Id)
                .ForeignKey("dbo.EleProteccions", t => t.Epro_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Epro_Id);
            
            CreateTable(
                "dbo.EleProteccions",
                c => new
                    {
                        Epro_Id = c.Int(nullable: false, identity: true),
                        Epro_Nom = c.String(),
                        Epro_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Epro_Id);
            
            CreateTable(
                "dbo.ObjEmpresas",
                c => new
                    {
                        Oemp_Id = c.Int(nullable: false, identity: true),
                        Oemp_Nombre = c.String(),
                        Oemp_Descrip = c.String(),
                        Oemp_Meta = c.String(),
                        Oemp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Empr_Nit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Oemp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.Politicas",
                c => new
                    {
                        Poli_Id = c.Int(nullable: false, identity: true),
                        Poli_Archivo = c.Binary(),
                        Empr_Nit = c.Int(nullable: false),
                        Poli_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Poli_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.ProcactEmpresas",
                c => new
                    {
                        Paem_Id = c.Int(nullable: false, identity: true),
                        Empr_Nit = c.Int(nullable: false),
                        Pact_Id = c.Int(nullable: false),
                        Paem_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Paem_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.ProcesActividads", t => t.Pact_Id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Pact_Id);
            
            CreateTable(
                "dbo.ProcesActividads",
                c => new
                    {
                        Pact_Id = c.Int(nullable: false, identity: true),
                        Pact_Nombre = c.String(),
                        Pact_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Pact_Id);
            
            CreateTable(
                "dbo.ReglaHigienes",
                c => new
                    {
                        Rhig_Id = c.Int(nullable: false, identity: true),
                        Rhig_Archivo = c.Binary(),
                        Empr_Nit = c.Int(nullable: false),
                        Rhig_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Rhig_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.ReglaInternoes",
                c => new
                    {
                        Rint_Id = c.Int(nullable: false, identity: true),
                        Rint_Archivo = c.Binary(),
                        Empr_Nit = c.Int(nullable: false),
                        Rint_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Rint_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.ZonaEmpresas",
                c => new
                    {
                        Zemp_Id = c.Int(nullable: false, identity: true),
                        Zemp_Nom = c.String(),
                        Zemp_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Empr_Nit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Zemp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.ItemEstandars",
                c => new
                    {
                        Iest_Id = c.Int(nullable: false, identity: true),
                        Iest_Desc = c.String(),
                        Iest_Verificar = c.String(),
                        Iest_Porcentaje = c.Single(nullable: false),
                        Esta_Id = c.Int(nullable: false),
                        Iest_Peri = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Iest_Observa = c.String(),
                        Iest_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Iest_Id)
                .ForeignKey("dbo.Estandars", t => t.Esta_Id)
                .Index(t => t.Esta_Id);
            
            CreateTable(
                "dbo.Estandars",
                c => new
                    {
                        Esta_Id = c.Int(nullable: false, identity: true),
                        Esta_Nom = c.String(),
                        Esta_Porcentaje = c.Single(nullable: false),
                        Crit_Id = c.Int(nullable: false),
                        Esta_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Esta_Id)
                .ForeignKey("dbo.Criterios", t => t.Crit_Id)
                .Index(t => t.Crit_Id);
            
            CreateTable(
                "dbo.Criterios",
                c => new
                    {
                        Crit_Id = c.Int(nullable: false, identity: true),
                        Crit_Nom = c.String(),
                        Crit_Porcentaje = c.Single(nullable: false),
                        Crit_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Crit_Id);
            
            CreateTable(
                "dbo.Mes",
                c => new
                    {
                        Mes_Id = c.Int(nullable: false, identity: true),
                        Mes_Nom = c.String(),
                        Mes_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Mes_Id);
            
            CreateTable(
                "dbo.Frecuencias",
                c => new
                    {
                        Frec_Id = c.Int(nullable: false, identity: true),
                        Frec_Nom = c.String(),
                        Frec_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Frec_Id);
            
            CreateTable(
                "dbo.Periodoes",
                c => new
                    {
                        Peri_Id = c.Int(nullable: false, identity: true),
                        Peri_Nom = c.String(),
                        Peri_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Peri_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActiCumplimientoes", "Peri_Id", "dbo.Periodoes");
            DropForeignKey("dbo.ActiCumplimientoes", "Oemp_Id", "dbo.ObjEmpresas");
            DropForeignKey("dbo.ActiCumplimientoes", "Frec_Id", "dbo.Frecuencias");
            DropForeignKey("dbo.ActiCumplimientoes", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.ActiCumplimientoes", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AcumMes", "Mes_Id", "dbo.Mes");
            DropForeignKey("dbo.AcumMes", "Cump_Id", "dbo.Cumplimientoes");
            DropForeignKey("dbo.Cumplimientoes", "Iest_Id", "dbo.ItemEstandars");
            DropForeignKey("dbo.ItemEstandars", "Esta_Id", "dbo.Estandars");
            DropForeignKey("dbo.Estandars", "Crit_Id", "dbo.Criterios");
            DropForeignKey("dbo.Cumplimientoes", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.Cumplimientoes", "Auev_Id", "dbo.AutoEvaluacions");
            DropForeignKey("dbo.AutoEvaluacions", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.ZonaEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.ReglaInternoes", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.ReglaHigienes", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.ProcactEmpresas", "Pact_Id", "dbo.ProcesActividads");
            DropForeignKey("dbo.ProcactEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.Politicas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.ObjEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.EprotEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.EprotEmpresas", "Epro_Id", "dbo.EleProteccions");
            DropForeignKey("dbo.Empresas", "Carl_Id", "dbo.ClaseArls");
            DropForeignKey("dbo.AspNetUsers", "Tvin_Id", "dbo.TipoVinculacions");
            DropForeignKey("dbo.AspNetUsers", "Tdoc_Id", "dbo.TipoDocumentoes");
            DropForeignKey("dbo.SedeCiudads", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.SedeCiudads", "Ciud_Id", "dbo.Ciudads");
            DropForeignKey("dbo.AspNetUsers", "Sciu_Id", "dbo.SedeCiudads");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Resultadoes", "Resp_Id", "dbo.Respuestas");
            DropForeignKey("dbo.Resultadoes", "Encu_Id", "dbo.Encuestas");
            DropForeignKey("dbo.Resultadoes", "Pregunta_Preg_Id", "dbo.Preguntas");
            DropForeignKey("dbo.TipoRespuestas", "Respuesta_Resp_Id", "dbo.Respuestas");
            DropForeignKey("dbo.Respuestas", "respuestasQuemadas_Quem_Id", "dbo.TipoRespuestas");
            DropForeignKey("dbo.Respuestas", "Preg_Id", "dbo.Preguntas");
            DropForeignKey("dbo.Preguntas", "Encu_Id", "dbo.Encuestas");
            DropForeignKey("dbo.Encuestas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.Resultadoes", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JornadaEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.AspNetUsers", "Jemp_Id", "dbo.JornadaEmpresas");
            DropForeignKey("dbo.AspNetUsers", "Jefe_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Gene_Id", "dbo.Generoes");
            DropForeignKey("dbo.Evidencias", "Tdca_id", "dbo.TipoDocCargas");
            DropForeignKey("dbo.Evidencias", "Cump_Id", "dbo.Cumplimientoes");
            DropForeignKey("dbo.Evidencias", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Espe_Id", "dbo.EstadoPersonas");
            DropForeignKey("dbo.AspNetUsers", "Eps_Id", "dbo.Eps");
            DropForeignKey("dbo.AspNetUsers", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Ciud_Id", "dbo.Ciudads");
            DropForeignKey("dbo.AspNetUsers", "Cate_Id", "dbo.CateLicencias");
            DropForeignKey("dbo.CargoEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.AspNetUsers", "Cemp_Id", "dbo.CargoEmpresas");
            DropForeignKey("dbo.Empresas", "Arl_Id", "dbo.Arls");
            DropForeignKey("dbo.AspNetUsers", "Arl_Id", "dbo.Arls");
            DropForeignKey("dbo.AreaEmpresas", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.AspNetUsers", "Aemp_Id", "dbo.AreaEmpresas");
            DropForeignKey("dbo.AspNetUsers", "Afp_Id", "dbo.Afps");
            DropForeignKey("dbo.AutoEvaluacions", "AutoEvaluacion_Auev_Id", "dbo.AutoEvaluacions");
            DropForeignKey("dbo.AcumMes", "Acum_Id", "dbo.ActiCumplimientoes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Estandars", new[] { "Crit_Id" });
            DropIndex("dbo.ItemEstandars", new[] { "Esta_Id" });
            DropIndex("dbo.ZonaEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.ReglaInternoes", new[] { "Empr_Nit" });
            DropIndex("dbo.ReglaHigienes", new[] { "Empr_Nit" });
            DropIndex("dbo.ProcactEmpresas", new[] { "Pact_Id" });
            DropIndex("dbo.ProcactEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.Politicas", new[] { "Empr_Nit" });
            DropIndex("dbo.ObjEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.EprotEmpresas", new[] { "Epro_Id" });
            DropIndex("dbo.EprotEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.SedeCiudads", new[] { "Empr_Nit" });
            DropIndex("dbo.SedeCiudads", new[] { "Ciud_Id" });
            DropIndex("dbo.TipoRespuestas", new[] { "Respuesta_Resp_Id" });
            DropIndex("dbo.Respuestas", new[] { "respuestasQuemadas_Quem_Id" });
            DropIndex("dbo.Respuestas", new[] { "Preg_Id" });
            DropIndex("dbo.Preguntas", new[] { "Encu_Id" });
            DropIndex("dbo.Encuestas", new[] { "Empr_Nit" });
            DropIndex("dbo.Resultadoes", new[] { "Pregunta_Preg_Id" });
            DropIndex("dbo.Resultadoes", new[] { "Resp_Id" });
            DropIndex("dbo.Resultadoes", new[] { "Id" });
            DropIndex("dbo.Resultadoes", new[] { "Encu_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.JornadaEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.Evidencias", new[] { "Id" });
            DropIndex("dbo.Evidencias", new[] { "Tdca_id" });
            DropIndex("dbo.Evidencias", new[] { "Cump_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.CargoEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.AreaEmpresas", new[] { "Empr_Nit" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "Jefe_Id" });
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
            DropIndex("dbo.Empresas", new[] { "Carl_Id" });
            DropIndex("dbo.Empresas", new[] { "Arl_Id" });
            DropIndex("dbo.AutoEvaluacions", new[] { "AutoEvaluacion_Auev_Id" });
            DropIndex("dbo.AutoEvaluacions", new[] { "Empr_Nit" });
            DropIndex("dbo.Cumplimientoes", new[] { "Auev_Id" });
            DropIndex("dbo.Cumplimientoes", new[] { "Empr_Nit" });
            DropIndex("dbo.Cumplimientoes", new[] { "Iest_Id" });
            DropIndex("dbo.AcumMes", new[] { "Mes_Id" });
            DropIndex("dbo.AcumMes", new[] { "Acum_Id" });
            DropIndex("dbo.AcumMes", new[] { "Cump_Id" });
            DropIndex("dbo.ActiCumplimientoes", new[] { "Frec_Id" });
            DropIndex("dbo.ActiCumplimientoes", new[] { "Empr_Nit" });
            DropIndex("dbo.ActiCumplimientoes", new[] { "Peri_Id" });
            DropIndex("dbo.ActiCumplimientoes", new[] { "Id" });
            DropIndex("dbo.ActiCumplimientoes", new[] { "Oemp_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Periodoes");
            DropTable("dbo.Frecuencias");
            DropTable("dbo.Mes");
            DropTable("dbo.Criterios");
            DropTable("dbo.Estandars");
            DropTable("dbo.ItemEstandars");
            DropTable("dbo.ZonaEmpresas");
            DropTable("dbo.ReglaInternoes");
            DropTable("dbo.ReglaHigienes");
            DropTable("dbo.ProcesActividads");
            DropTable("dbo.ProcactEmpresas");
            DropTable("dbo.Politicas");
            DropTable("dbo.ObjEmpresas");
            DropTable("dbo.EleProteccions");
            DropTable("dbo.EprotEmpresas");
            DropTable("dbo.ClaseArls");
            DropTable("dbo.TipoVinculacions");
            DropTable("dbo.TipoDocumentoes");
            DropTable("dbo.SedeCiudads");
            DropTable("dbo.TipoRespuestas");
            DropTable("dbo.Respuestas");
            DropTable("dbo.Preguntas");
            DropTable("dbo.Encuestas");
            DropTable("dbo.Resultadoes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.JornadaEmpresas");
            DropTable("dbo.Generoes");
            DropTable("dbo.TipoDocCargas");
            DropTable("dbo.Evidencias");
            DropTable("dbo.EstadoPersonas");
            DropTable("dbo.Eps");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Ciudads");
            DropTable("dbo.CateLicencias");
            DropTable("dbo.CargoEmpresas");
            DropTable("dbo.Arls");
            DropTable("dbo.AreaEmpresas");
            DropTable("dbo.Afps");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Empresas");
            DropTable("dbo.AutoEvaluacions");
            DropTable("dbo.Cumplimientoes");
            DropTable("dbo.AcumMes");
            DropTable("dbo.ActiCumplimientoes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
