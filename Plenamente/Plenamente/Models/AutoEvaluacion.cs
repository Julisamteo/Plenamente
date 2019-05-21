using Plenamente.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class AutoEvaluacion
    {
        [Key]
        public int Auev_Id { get; set; }
        public string Auev_Nom {get; set;}
        public DateTime Auev_Inicio {get; set;}
        public DateTime  Auev_Fin {get; set;}

        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }

        //Permite que Cumplimiento acceda a la Data
        public ICollection<AutoEvaluacion> AutoEvaluaciones { get; set; }
    }
}