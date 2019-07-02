using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Empresa
    {
        [Key]
        public int Empr_Nit { get; set; }
        [Display(Name = "Nombre Empresa")]
        [Required]
        public string Empr_Nom { get; set; }
        [Required]
        public string Empr_Dir { get; set; }
        // ForeignKey ARL
        [Required]
        public int Arl_Id { get; set; }
        public Arl Arl { get; set; }
        //ForeignKey ClaseArl
        [Required]
        public int Carl_Id { get; set; }
        public ClaseArl ClaseArl { get; set; }
        [Required]
        public int Empr_Afiarl { get; set; }
        [Required]
        public int Empr_Ttrabaja { get; set; }
        public int Empr_Itrabaja { get; set; }
        [Required]
        public string Empr_telefono { get; set; }
        public DateTime Empr_Registro { get; set; }
        //Index(IsUnique = true)]
        [Required]
        public int Empr_NewNit { get; set; }
        [Required]
        public string Empr_RepresentanteLegal { get; set; }
        public string Empr_CargoRepresentante { get; set; }
        [Required]
        public int Empre_RepresentanteDoc { get; set; }
        [Required]
        public string Empr_ResponsableSST { get; set; }
        [Required]
        public int Empre_ResponsableDoc { get; set; }


        // Permite que Cargo Empresa acceda a la data
        public ICollection<CargoEmpresa> CargoEmpresas { get; set; }
        // Permite que Persona acceda a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        // Permite que SedeCiudad Acceda a la Data
        public ICollection<SedeCiudad> SedeCiudades { get; set; }
        // Permite que Jornada Empresa Acceda a la Data
        public ICollection<JornadaEmpresa> JornadaEmpresas { get; set; }
        // Permite que Area Empresa Acceda a la Data
        public ICollection<AreaEmpresa> AreaEmpresas { get; set; }
        // Permite a Politica Acceder a la Data
        public ICollection<Politica> Politicas { get; set; }
        // Permite a proactempresa Acceder a la Data
        public ICollection<ProcactEmpresa> ProcactEmpresas { get; set; }
        // Permite a ReglaHigiene Acceder a la Data
        public ICollection<ReglaHigiene> ReglaHigienes { get; set; }
        // Permite a ReglaInterno Aceeder a la Data
        public ICollection<ReglaInterno> ReglaInternos { get; set; }
        // Permite a ObjEmpresa accede a la Data
        public ICollection<ObjEmpresa> ObjEmpresas { get; set; }
        // Permite a Encuesta Aceeder a la Data
        public ICollection<Encuesta> Encuestas { get; set; }
        // Permit a Acticumplimiento acceder a la Data 
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
        //Permite a cumplimineto acceder a la Data
        public ICollection<Cumplimiento> Cumplimientos { get; set; }
        // Permite a Zonaempresa acceder a la Data
        public ICollection<ZonaEmpresa> ZonaEmpresas { get; set; }
        // Permite a EportEmpresa acceder a la Data
        public ICollection<EprotEmpresa> EprotEmpresas { get; set; }
        // Permite a AutoEvaluacion acceder a la Data
        public ICollection<AutoEvaluacion> AutoEvaluaciones { get; set; }

        //[DefaultValue(1)]
        //public short? TipoEmpresa_Id { get; set; }
        public virtual TipoEmpresa TipoEmpresa { get; set; }
    }
}