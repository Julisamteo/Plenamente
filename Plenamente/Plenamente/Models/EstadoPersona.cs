using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class EstadoPersona
    {
        [Key]
        public int Stem_Id { get; set; }
        public string Stem_Nom { get; set; }
        public DateTime Stem_Registro { get; set; }
        //Permite a persona acceder a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}