using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Plenamente.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        //public int Pers_Doc { get; set; }
        //public string Pers_Nom1 { get; set; }
        //public string Pers_Nom2 { get; set; }
        //public string Pers_Apel1 { get; set; }
        //public string Pers_Apel2 { get; set; }
        //public int Pers_Licencia { get; set; }
        //public DateTime Pers_LicVence { get; set; }
        //public byte[] Pers_Foto { get; set; }
        //public DateTime Pers_Ingreso { get; set; }
        //public DateTime Pers_Retiro { get; set; }
        //public string Pers_Dir { get; set; }
        //public string Pers_Cemeg { get; set; }
        //public int Pers_Temeg { get; set; }
        //public DateTime Pers_Registro { get; set; }


        //public int Tdoc_Id { get; set; }
        //public TipoDocumento TipoDocumento { get; set; }
        //public int Sciu_Id { get; set; }
        //public SedeCiudad SedeCiudad { get; set; }
        //public int Ciud_Id { get; set; }
        //public Ciudad Ciudad { get; set; }
        //public int Cemp_Id { get; set; }
        //public CargoEmpresa CargoEmpresa { get; set; }
        //public int Aemp_Id { get; set; }
        //public AreaEmpresa AreaEmpresa { get; set; }
        //public int Cate_Id { get; set; }
        //public CateLicencia CateLicencia { get; set; }
        //public int Gene_Id { get; set; }
        //public Genero Genero { get; set; }
        //public int Jemp_Id { get; set; }
        //public JornadaEmpresa JornadaEmpresa { get; set; }
        //public int Tvin_Id { get; set; }
        //public TipoVinculacion TipoVinculacion { get; set; }
        //public int Eps_Id { get; set; }
        //public Eps Eps { get; set; }
        //public int Afp_Id { get; set; }
        //public Afp Afp { get; set; }
        //public int Arl_Id { get; set; }
        //public Arl Arl { get; set; }
        //public int Empr_Nit { get; set; }
        //public Empresa Empresa { get; set; }
        //public int Espe_Id { get; set; }
        //public EstadoPersona EstadoPersona { get; set; }
        //public string Jefe_Id { get; set; }
        //public ApplicationUser Jefe { get; set; }

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
        //public DbSet<Afp> Tb_Afp { get; set; }
        //public DbSet<AreaEmpresa> Tb_AreaEmpresa { get; set; }
        //public DbSet<Arl> Tb_Arl { get; set; }
        //public DbSet<CargoEmpresa> Tb_CargoEmpresa { get; set; }
        //public DbSet<CateLicencia> Tb_CateLicencia { get; set; }
        //public DbSet<Ciudad> Tb_Ciudad { get; set; }
        //public DbSet<ClaseArl> Tb_ClaseArl { get; set; }
        //public DbSet<EleProteccion> Tb_EleProteccion { get; set; }
        //public DbSet<Empresa> Tb_Empresa { get; set; }
        //public DbSet<EprotEmpresa> Tb_EprotEmpresa { get; set; }
        //public DbSet<Eps> Tb_Eps { get; set; }
        //public DbSet<EstadoPersona> Tb_EstadoPersona { get; set; }
        //public DbSet<Genero> Tb_Genero { get; set; }
        //public DbSet<JornadaEmpresa> Tb_JornadaEmpresa { get; set; }
        //public DbSet<Politica> Tb_politica { get; set; }
        //public DbSet<ProcactEmpresa> Tb_ProcactEmpresa { get; set; }
        //public DbSet<ProcesActividad> Tb_ProcesActividad { get; set; }
        //public DbSet<ReglaHigiene> Tb_ReglaHigiene { get; set; }
        //public DbSet<ReglaInterno> Tb_ReglaInterno { get; set; }
        //public DbSet<SedeCiudad> Tb_SedeCiudad { get; set; }
        //public DbSet<TipoDocumento> Tb_TipoDocumento { get; set; }
        //public DbSet<TipoVinculacion> Tb_TipoVinculacion { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    ////Llave reflexiva
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasOptional(u => u.Jefe).WithMany().HasForeignKey(x => x.Jefe_Id);
        //    ////Foraneas obligatorias
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.Afp).WithMany().HasForeignKey(x => x.Afp_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.Arl).WithMany().HasForeignKey(x => x.Arl_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.Eps).WithMany().HasForeignKey(x => x.Eps_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.CargoEmpresa).WithMany().HasForeignKey(x => x.Cemp_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.Ciudad).WithMany().HasForeignKey(x => x.Ciud_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.Empresa).WithMany().HasForeignKey(x => x.Empr_Nit);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.EstadoPersona).WithMany().HasForeignKey(x => x.Espe_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.Genero).WithMany().HasForeignKey(x => x.Gene_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.TipoDocumento).WithMany().HasForeignKey(x => x.Tdoc_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasRequired(u => u.TipoVinculacion).WithMany().HasForeignKey(x => x.Tvin_Id);
        //    ////Foraneas no-obligatorias
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasOptional(u => u.SedeCiudad).WithMany().HasForeignKey(x => x.Sciu_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasOptional(u => u.AreaEmpresa).WithMany().HasForeignKey(x => x.Aemp_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasOptional(u => u.CateLicencia).WithMany().HasForeignKey(x => x.Cate_Id);
        //    //modelBuilder.Entity<ApplicationUser>().
        //    //    HasOptional(u => u.JornadaEmpresa).WithMany().HasForeignKey(x => x.Jemp_Id);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}