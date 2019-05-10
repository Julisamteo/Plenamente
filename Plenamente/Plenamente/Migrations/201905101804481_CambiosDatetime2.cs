namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosDatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActiCumplimientoes", "Acum_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ActiCumplimientoes", "Acum_IniAct", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ActiCumplimientoes", "Acum_FinAct", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AcumMes", "Acme_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Cumplimientoes", "Cump_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "Pers_LicVence", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "Pers_Ingreso", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "Pers_Retiro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "Pers_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Afps", "Afp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AreaEmpresas", "Aemp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Empresas", "Empr_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Arls", "Arl_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CargoEmpresas", "Cemp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ClaseArls", "Carl_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Encuestas", "Encu_Creacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Encuestas", "Encu_Vence", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Encuestas", "Encu_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Preguntas", "Preg_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Respuestas", "Resp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Resultadoes", "Resu_Respuesta", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EleProteccions", "Epro_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.JornadaEmpresas", "Jemp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ObjEmpresas", "Oemp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Politicas", "Poli_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ProcactEmpresas", "Paem_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ProcesActividads", "Pact_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ReglaHigienes", "Rhig_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ReglaInternoes", "Rint_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SedeCiudads", "Sciu_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Ciudads", "Ciud_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ZonaEmpresas", "Zemp_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CateLicencias", "Cate_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Eps", "Eps_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EstadoPersonas", "Espe_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Generoes", "Gene_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TipoDocumentoes", "Tdoc_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TipoVinculacions", "Tvin_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ItemEstandars", "Iest_Peri", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ItemEstandars", "Iest_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Estandars", "Esta_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Criterios", "Crit_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TipoDocCargas", "Tdca_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Mes", "Mes_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Frecuencias", "Frec_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Periodoes", "Peri_Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Periodoes", "Peri_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Frecuencias", "Frec_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Mes", "Mes_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TipoDocCargas", "Tdca_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Criterios", "Crit_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Estandars", "Esta_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemEstandars", "Iest_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemEstandars", "Iest_Peri", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TipoVinculacions", "Tvin_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TipoDocumentoes", "Tdoc_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Generoes", "Gene_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EstadoPersonas", "Espe_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Eps", "Eps_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CateLicencias", "Cate_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ZonaEmpresas", "Zemp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Ciudads", "Ciud_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SedeCiudads", "Sciu_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ReglaInternoes", "Rint_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ReglaHigienes", "Rhig_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProcesActividads", "Pact_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProcactEmpresas", "Paem_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Politicas", "Poli_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ObjEmpresas", "Oemp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.JornadaEmpresas", "Jemp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EleProteccions", "Epro_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Resultadoes", "Resu_Respuesta", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Respuestas", "Resp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Preguntas", "Preg_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Encuestas", "Encu_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Encuestas", "Encu_Vence", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Encuestas", "Encu_Creacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ClaseArls", "Carl_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CargoEmpresas", "Cemp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Arls", "Arl_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Empresas", "Empr_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AreaEmpresas", "Aemp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Afps", "Afp_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "Pers_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Retiro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Ingreso", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_LicVence", c => c.DateTime());
            AlterColumn("dbo.Cumplimientoes", "Cump_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AcumMes", "Acme_Registro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActiCumplimientoes", "Acum_FinAct", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActiCumplimientoes", "Acum_IniAct", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActiCumplimientoes", "Acum_Registro", c => c.DateTime(nullable: false));
        }
    }
}
