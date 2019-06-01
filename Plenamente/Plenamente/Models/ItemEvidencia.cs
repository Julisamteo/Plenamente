using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ItemEvidencia
    {
        public int Id { get; set; }
        public int Empresa_Empr_Nit { get; set; }
        public int ItemEstandar_Iest_Id { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ItemEstandar ItemEstandar { get; set; }
    }
}