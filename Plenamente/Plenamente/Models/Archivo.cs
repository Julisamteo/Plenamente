using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Archivo
    {
        public Archivo()
        {
            // Genera automaticamente el campo tipo date.
            Cump_Registro = DateTime.Now;
        }

        public int Id { get; set; }
        public string Cump_Nombre { get; set; }
        public HttpPostedFileBase Cump_Aevidencia { get; set; }
        public DateTime Cump_Registro { get; set; }

        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
    }
}