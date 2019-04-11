using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Criterio
    {
        [Key]
        public int Crit_Id { get; set; }
        public int Crit_Nom { get; set; }
        public float Crit_Porcentaje { get; set; }
        public DateTime Crit_Registro { get; set; }

        // Permite a Estandar acceder a la Data
        public ICollection<Estandar> Estandars { get; set; }
    }
}