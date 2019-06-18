using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class ViewModelActividadCumplimiento
    {
        public int IdActiCumplimiento { get; set; }
        public int IdEmpresa { get; set; }

        [Display(Name = "Actividad")]
        public string NombreActividad { get; set; }

        [Display(Name = "Meta")]
        public float Meta { get; set; }

        [Display(Name = "Fecha inicial")]

        public DateTime FechaInicial { get; set; }

        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        [Display(Name = "Hora")]
        public TimeSpan hora { get; set; }
        [Display(Name = "Objetivo de la Empresa")]
        [Required]
        public int idObjetivo { get; set; }

        [Display(Name = "Periodicidad")]
        public string Frecuencia { get; set; }
                
        public string retornar { get; set; }

        public string Frecuencia_desc { get; set; }
        public int period { get; set; }
        public string weekly_0 { get; set; }
        public string weekly_1 { get; set; }
        public string weekly_2 { get; set; }
        public string weekly_3 { get; set; }
        public string weekly_4 { get; set; }
        public string weekly_5 { get; set; }
        public string weekly_6 { get; set; }
    }
}