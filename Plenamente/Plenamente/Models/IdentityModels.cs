using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Plenamente.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public int Pers_Doc { get; set; }
        public string Pers_Nom1 { get; set; }
        public string Pers_Nom2 { get; set; }
        public string Pers_Apel1 { get; set; }
        public string Pers_Apel2 { get; set; }
        public int Pers_Licencia { get; set; }
        public DateTime Pers_LicVence { get; set; }
        public byte[] Pers_Foto { get; set; }
        public DateTime Pers_Ingreso { get; set; }
        public DateTime Pers_Retiro { get; set; }
        public string Pers_Dir { get; set; }
        public string Pers_Cemeg { get; set; }
        public int Pers_Temeg { get; set; }
        public DateTime Pers_Registro { get; set; }


        
        public string Jefe_Id { get; set; }
        public ApplicationUser Jefe { get; set; }

        // Permite que Resultado acceda a la data
        public ICollection<Resultado> Resultados { get; set; }
        // Permite que ActiCumplimiento acceda a la data
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
        //Permite a cumplimineto acceder a la Data
        public ICollection<Cumplimiento> Cumplimientos { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet <ActiCumplimiento> Tb_ActiCumplimiento {get; set;}
        public DbSet <AcumMes> Tb_Acumes {get; set;}
        public DbSet<Afp> Tb_Afp { get; set; }
        public DbSet<AreaEmpresa> Tb_AreaEmpresa { get; set; }
        public DbSet<Arl> Tb_Arl { get; set; }
        public DbSet<CargoEmpresa> Tb_CargoEmpresa { get; set; }
        public DbSet<CateLicencia> Tb_CateLicencia { get; set; }
        public DbSet<Ciudad> Tb_Ciudad { get; set; }
        public DbSet<ClaseArl> Tb_ClaseArl { get; set; }
        public DbSet<Criterio> Tb_Criterio {get; set; }
        public DbSet<Cumplimiento> Tb_Cumplimiento {get; set;}
        public DbSet<EleProteccion> Tb_EleProteccion { get; set; }
        public DbSet<Empresa> Tb_Empresa { get; set; }
        public DbSet<Encuesta> Tb_Encuesta {get; set;}
        public DbSet<EprotEmpresa> Tb_EprotEmpresa { get; set; }
        public DbSet<Eps> Tb_Eps { get; set; }
        public DbSet<EstadoPersona> Tb_EstadoPersona { get; set; }
        public DbSet<Estandar>Tb_Estandar {get; set;}
        public DbSet<Frecuencia> Tb_Frecuencia {get; set;}
        public DbSet<Genero> Tb_Genero { get; set; }
        public DbSet <ItemEstandar>Tb_ItemEstandar {get; set;}
        public DbSet<JornadaEmpresa> Tb_JornadaEmpresa { get; set; }
        public DbSet<Mes>Tb_Mes {get; set; }
        public DbSet<ObjEmpresa> Tb_ObjEmpresa {get; set;}
        public DbSet<Periodo>Tb_Periodo {get; set;}
        public DbSet<Politica> Tb_politica { get; set; }
        public DbSet<ProcactEmpresa> Tb_ProcactEmpresa { get; set; }
        public DbSet<ProcesActividad> Tb_ProcesActividad { get; set; }
        public DbSet<ReglaHigiene> Tb_ReglaHigiene { get; set; }
        public DbSet<ReglaInterno> Tb_ReglaInterno { get; set; }
        public DbSet<Respuesta> Tb_Respuesta {get; set;}
        public DbSet<Resultado> Tb_Resultado {get; set;}
        public DbSet<SedeCiudad> Tb_SedeCiudad { get; set; }
        public DbSet<TipoDocCarga> Tb_TipoDocCarga {get; set;}
        public DbSet<TipoDocumento> Tb_TipoDocumento { get; set; }
        public DbSet<TipoVinculacion> Tb_TipoVinculacion { get; set; }
        public DbSet<ZonaEmpresa> Tb_ZonaEmpresa {get; set;}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //Llave reflexiva
            modelBuilder.Entity<ApplicationUser>().
                HasOptional(u => u.Jefe).WithMany().HasForeignKey(x => x.Jefe_Id);
 

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Plenamente.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}