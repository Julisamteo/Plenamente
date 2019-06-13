using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class AutoEvaluacionViewModel
    {
        public AutoEvaluacion AutoEvaluacion { get; set; }

        public AutoEvaluacionViewModel()
        {
            AutoEvaluacion = new AutoEvaluacion();
        }
        public int Id { get => AutoEvaluacion.Auev_Id; set => AutoEvaluacion.Auev_Id = value; }
        [Display(Name = "No")]
        public int IdentificadorIncremental { get; set; }
        [Display(Name = "Nombre")]
        public string NameAutoEvaluacion { get=>AutoEvaluacion.Auev_Nom; set => AutoEvaluacion.Auev_Nom = value; }
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime Auev_Inicio { get => AutoEvaluacion.Auev_Inicio; set => AutoEvaluacion.Auev_Inicio = value; }
        [Display(Name = "Fecha de finalizacion")]
        [DataType(DataType.Date)]
        public DateTime Auev_Fin { get => AutoEvaluacion.Auev_Fin; set => AutoEvaluacion.Auev_Fin = value; }
    }
}