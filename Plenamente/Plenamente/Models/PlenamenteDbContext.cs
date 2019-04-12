using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class PlenamenteDbContext : DbContext
    {
        public DbSet<ActiCumplimiento> Tb_ActiCumplimiento { get; set; }
        public DbSet<AcumMes> Tb_Acumes { get; set; }
        public DbSet<Afp> Tb_Afp { get; set; }
        public DbSet<AreaEmpresa> Tb_AreaEmpresa { get; set; }
        public DbSet<Arl> Tb_Arl { get; set; }
        public DbSet<CargoEmpresa> Tb_CargoEmpresa { get; set; }
        public DbSet<CateLicencia> Tb_CateLicencia { get; set; }
        public DbSet<Ciudad> Tb_Ciudad { get; set; }
        public DbSet<ClaseArl> Tb_ClaseArl { get; set; }
        public DbSet<Criterio> Tb_Criterio { get; set; }
        public DbSet<Cumplimiento> Tb_Cumplimiento { get; set; }
        public DbSet<EleProteccion> Tb_EleProteccion { get; set; }
        public DbSet<Empresa> Tb_Empresa { get; set; }
        public DbSet<Encuesta> Tb_Encuesta { get; set; }
        public DbSet<EprotEmpresa> Tb_EprotEmpresa { get; set; }
        public DbSet<Eps> Tb_Eps { get; set; }
        public DbSet<EstadoPersona> Tb_EstadoPersona { get; set; }
        public DbSet<Estandar> Tb_Estandar { get; set; }
        public DbSet<Frecuencia> Tb_Frecuencia { get; set; }
        public DbSet<Genero> Tb_Genero { get; set; }
        public DbSet<ItemEstandar> Tb_ItemEstandar { get; set; }
        public DbSet<JornadaEmpresa> Tb_JornadaEmpresa { get; set; }
        public DbSet<Mes> Tb_Mes { get; set; }
        public DbSet<ObjEmpresa> Tb_ObjEmpresa { get; set; }
        public DbSet<Periodo> Tb_Periodo { get; set; }
        public DbSet<Politica> Tb_politica { get; set; }
        public DbSet<ProcactEmpresa> Tb_ProcactEmpresa { get; set; }
        public DbSet<ProcesActividad> Tb_ProcesActividad { get; set; }
        public DbSet<ReglaHigiene> Tb_ReglaHigiene { get; set; }
        public DbSet<ReglaInterno> Tb_ReglaInterno { get; set; }
        public DbSet<Respuesta> Tb_Respuesta { get; set; }
        public DbSet<Resultado> Tb_Resultado { get; set; }
        public DbSet<SedeCiudad> Tb_SedeCiudad { get; set; }
        public DbSet<TipoDocCarga> Tb_TipoDocCarga { get; set; }
        public DbSet<TipoDocumento> Tb_TipoDocumento { get; set; }
        public DbSet<TipoVinculacion> Tb_TipoVinculacion { get; set; }
        public DbSet<ZonaEmpresa> Tb_ZonaEmpresa { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            ////Llave reflexiva
            //modelBuilder.Entity<ApplicationUser>().
            //    HasOptional(u => u.Jefe).WithMany().HasForeignKey(x => x.Jefe_Id);


            base.OnModelCreating(modelBuilder);
        }
    }
}