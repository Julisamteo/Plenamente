using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Frecuencia
    {
        [Key]
        public int Frec_Id { get; set; }
        public string Frec_Nom { get; set; }
        public DateTime Frec_Registro { get; set; }

        // Permite a acticumplimiento acceder a la Data
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
    }
}