using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Resultado
    {
        [Key]
        public int Resu_Id { get; set; }
        public DateTime Resu_Respuesta { get; set; }

        /*Llave Foranea a la tabla Encuesta*/
        [ForeignKey("Encuesta")]
        public int Encu_Id { get; set; }
        public Encuesta Encuesta { get; set; }
      
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; } 
        /*Llave Foranea a la tabla Respuesta*/
        [ForeignKey("Respuesta")]
        public int Resp_Id { get; set; }
        public Respuesta Respuesta { get; set; }
    }
}