using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class TipoDocCarga
    {
        [Key]
        public int Tdca_id { get; set; }
        public string Tdca_Nom { get; set; }
        public DateTime Tdca_Registro { get; set; }

        //Permite a cumplimineto acceder a la Data
        public ICollection<Cumplimiento> Cumplimientos { get; set; }
    }
}