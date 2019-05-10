using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Respuesta
    {
        public Respuesta()
        {
            Resp_Registro = DateTime.Now;
        }
        [Key]
        public int Resp_Id { get; set; }
        public string Resp_Nom { get; set; }

        public string Resp_Tipo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Resp_Registro { get; set; }

        public int Quem_Id { get; set; }
        public TipoRespuesta respuestasQuemadas { get; set; }

        /*Llave Foranea a la tabla Pregunta*/
        [ForeignKey("Pregunta")]
        public int Preg_Id { get; set; }
        public Pregunta Pregunta { get; set; }

        // Permite que Resultado acceda a la data
        public ICollection<Resultado> Resultados { get; set; }
        public ICollection<TipoRespuesta> TipoRespuestas { get; set; }
    }
}