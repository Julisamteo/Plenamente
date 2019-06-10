using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Autoevaluacion_itemEstandar
    {
        public int Id { get; set; }
        [ForeignKey("AutoEvaluacion")]
        public int Id_autoevaluacion { get; set; }
        [ForeignKey("ItemEstandar")]
        public int Id_itemestandar { get; set; }
        public bool Cumplimiento { get; set; }
        public bool Estado { get; set; }
        public double Value { get; set; }
        public AutoEvaluacion AutoEvaluacion { get; set; }
        public ItemEstandar ItemEstandar { get; set; }
    }
}