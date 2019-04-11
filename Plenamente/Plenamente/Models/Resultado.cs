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
        [ForeignKey("idEncuesta")]
        public int Encu_Id { get; set; }
        public Encuesta Encuesta { get; set; }
        /*Llave Foranea a la tabla Persona*/
        [ForeignKey("documentoPersona")]
        public int Pers_Doc { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        /*Llave Foranea a la tabla Respuesta*/
        [ForeignKey("idRespuesta")]
        public int Resp_Id { get; set; }
        public Respuesta Respuesta { get; set; }
    }
}