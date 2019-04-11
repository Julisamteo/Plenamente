using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ReglaInterno
    {
        [Key]
        public int Rint_Id { get; set; }
        public byte[] Rint_Archivo { get; set; }
        //Foreign Key Empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        //Fin Foreign Key
        public DateTime Rint_Registro { get; set; }
    }
}