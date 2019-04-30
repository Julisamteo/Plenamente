using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Cumplimiento
    {
        [Key]
        public int Cump_Id { get; set; }
        public byte[] Cump_Evidencia { get; set; }
        // ForeignKey
        [ForeignKey("ItemEstandar")]
        public int Iest_Id { get; set; }
        public ItemEstandar ItemEstandar { get; set; }
        //// ForeignKey
        //[ForeignKey("ApplicationUser")]
        //public int Id { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        //ForeignKey
       [ForeignKey("Empresa")]
       public int Empr_Id { get; set; }
       public Empresa Empresa { get; set; }
        //Foreign Key
        [ForeignKey("TipoDocCarga")]
        public int Tdca_Id { get; set; }
        public TipoDocCarga TipoDocCarga { get; set; }

        public DateTime Cump_Registro { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }
    }
}