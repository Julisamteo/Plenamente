using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class TipoRespuesta
    {
        [Key]
        public int Tres_Id { get; set; }
        public string Tres_Nom { get; set; }

        public ICollection<Respuesta> Respuestas { get; set; }
    }
}