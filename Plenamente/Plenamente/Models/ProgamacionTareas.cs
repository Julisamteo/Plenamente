using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ProgamacionTareas
    {   [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Estado { get; set; }
        [ForeignKey("ActiCumplimiento")]
        public int ActiCumplimiento_Id { get; set; }
        public ActiCumplimiento ActiCumplimiento { get; set; }

    }
}