using System;
using System.Collections.Generic;
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
        public string Test { get; set; }


        [ForeignKey("TipoDocumento")]
        public int Tdoc_Id { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        [ForeignKey("SedeCiudad")]
        public int Sciu_Id { get; set; }
        public SedeCiudad SedeCiudad { get; set; }
        [ForeignKey("Ciudad")]
        public int Ciud_Id { get; set; }
        public Ciudad Ciudad { get; set; }
        [ForeignKey("CargoEmpresa")]
        public int Cemp_Id { get; set; }
        public CargoEmpresa CargoEmpresa { get; set; }
        [ForeignKey("AreaEmpresa")]
        public int Aemp_Id { get; set; }
        public AreaEmpresa AreaEmpresa { get; set; }
        [ForeignKey("CateLicencia")]
        public int Cate_Id { get; set; }
        public CateLicencia CateLicencia { get; set; }
        [ForeignKey("Genero")]
        public int Gene_Id { get; set; }
        public Genero Genero { get; set; }
        [ForeignKey("JornadaEmpresa")]
        public int Jemp_Id { get; set; }
        public JornadaEmpresa JornadaEmpresa { get; set; }
        [ForeignKey("TipoVinculacion")]
        public int Tvin_Id { get; set; }
        public TipoVinculacion TipoVinculacion { get; set; }
        [ForeignKey("EPS")]
        public int Eps_Id { get; set; }
        public Eps Eps { get; set; }
        [ForeignKey("AFP")]
        public int Afp_Id { get; set; }
        public Afp Afp { get; set; }
        [ForeignKey("ARL")]
        public int Arl_Id { get; set; }
        public Arl Arl { get; set; }
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        [ForeignKey("EstadoPersona")]
        public int Espe_Id { get; set; }
        public EstadoPersona EstadoPersona { get; set; }

        // Permite que Resultado acceda a la data
        public ICollection<Resultado> Resultados { get; set; }

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

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }
    }
}