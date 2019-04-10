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
        //Nombre de la colección definido en el prural de EstadoPersona <EstadoPersonas>
        public ICollection<EstadoPersona> EstadoPersonas { get; set; }
    }
}