using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class ViewModelActividadCumplimiento
    {
        public int IdEmpresa { get; set; }

        [Display(Name = "Actividad")]
        public string NombreActividad { get; set; }

        [Display(Name = "Meta")]
        public string Meta { get; set; }

        [Display(Name = "Fecha inicial")]
        public DateTime FechaInicial { get; set; }

        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        [Display(Name = "Hora")]
        public TimeSpan hora { get; set; }



        [Display(Name = "Frecuencia")]
        public string Frecuencia { get; set; }

    }
}