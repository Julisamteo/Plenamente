using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class EvidenciaCumplimientoViewModel
    {
       public Evidencia Evidencia { get; set; }    
        public EvidenciaCumplimientoViewModel()
        {            
            Evidencia = new Evidencia();
        }
        [Display(Name = "Numero de documento")]
        public int IdDocumento { get; set; }
        [Display(Name = "Tipo de documento")]
        public string TipoDocumento { get; set; }
        [Display(Name = "Responsable")]
        public string Responsable { get; set; }
        [Display(Name = "Fecha")]
        public int Fecha { get; set; }
        [Display(Name = "Manual")]
        public int Manual { get; set; }
        [Display(Name = "Registro")]
        public int Registro { get; set; }
        [Required]
        [Display(Name = "Archivo")]
        public HttpPostedFileBase Archivo { get; set; }
        public int IdCumplimiento { get; set; }
    }
}