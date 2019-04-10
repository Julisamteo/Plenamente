using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Persona : IdentityUser
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
        [ForeignKey("SedeCiudad")]
        public int Sciu_Id { get; set; }
        [ForeignKey("Ciudad")]
        public int Ciud_Id { get; set; }
        [ForeignKey("CargoEmpresa")]
        public int Cemp_Id { get; set; }
        [ForeignKey("AreaEmpresa")]
        public int Aemp_Id { get; set; }
        [ForeignKey("CateLicencia")]
        public int Cate_Id { get; set; }
        [ForeignKey("Persona")]
        public int Id { get; set; }
        [ForeignKey("Genero")]
        public int Gene_Id { get; set; }
        [ForeignKey("JornadaEmpresa")]
        public int Jemp_Id { get; set; }
        [ForeignKey("TipoVinculacion")]
        public int Tvin_Id { get; set; }
        [ForeignKey("EPS")]
        public int Eps_Id { get; set; }
        [ForeignKey("AFP")]
        public int Afp_Id { get; set; }
        [ForeignKey("ARL")]
        public int Arl_Id { get; set; }
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        [ForeignKey("EstadoPersona")]
        public int Espe_Id { get; set; }

        [ForeignKey("Tdoc_Id")]
        public virtual TipoDocumento TipoDocumento { get; set; }
        [ForeignKey("Sciu_Id")]
        public virtual SedeCiudad SedeCiudad { get; set; }
        [ForeignKey("Ciud_Id")]
        public virtual Ciudad Ciudad { get; set; }
        [ForeignKey("Cemp_Id")]
        public virtual CargoEmpresa CargoEmpresa { get; set; }
        [ForeignKey("Aemp_Id")]
        public virtual AreaEmpresa AreaEmpresa { get; set; }
        [ForeignKey("Cate_Id")]
        public virtual CateLicencia CateLicencia { get; set; }
        [ForeignKey("Id")]
        public virtual IdentityUser IdentityUser { get; set; }
        [ForeignKey("Gene_Id")]
        public virtual Genero Genero { get; set; }
        [ForeignKey("Jemp_Id")]
        public virtual JornadaEmpresa JornadaEmpresa { get; set; }
        [ForeignKey("Tvin_Id")]
        public virtual TipoVinculacion TipoVinculacion { get; set; }
        [ForeignKey("Eps_Id")]
        public virtual EPS EPS { get; set; }
        [ForeignKey("Afp_Id")]
        public virtual AFP AFP { get; set; }
        [ForeignKey("Arl_Id")]
        public virtual ARL ARL { get; set; }
        [ForeignKey("Empr_Nit")]
        public virtual Empresa Empresa { get; set; }
        [ForeignKey("Espe_Id")]
        public virtual EstadoPersona EstadoPersona { get; set; }        
    }
}