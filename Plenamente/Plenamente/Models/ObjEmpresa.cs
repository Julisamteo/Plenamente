using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ObjEmpresa
    {
        [Key]
        public int Oemp_Id { get; set; }
        public string Oemp_Nombre { get; set; }
        public string Oemp_Descrip { get; set; }
        public DateTime Oemp_Registro { get; set; }

        // Foreign Key Empresa
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }

    }
}