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
        [Display(Name = "Usuarios")]
        public string IdUser { get; set; }

        [Display(Name = "Actividad")]
        public string NombreActividad { get; set; }

        [Display(Name = "Meta (%)")]
        public float Meta { get; set; }

        [Display(Name = "Fecha Programada ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        [Display(Name = "Fecha de finalización")]
        [Required(ErrorMessage = "Your elegant error message goes here")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        [Display(Name = "Asignación de recursos")]
        public string asigrecursos { get; set; }
        [Display(Name = "Finalizar actividad")]
        public bool Finalizada { get; set; }
        public int idPlanDeTrabajo { get; set; }
    }
}