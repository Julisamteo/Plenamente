using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ActiCumplimiento
    {
        [Key]
        public int Acum_Id { get; set; }
        public string Acum_Desc { get; set; }
        public float Acum_Porcentest { get; set; }
        public string Acum_Ejec { get; set; }
        public DateTime Acum_Registro { get; set; }
        public DateTime Acum_IniAct { get; set; }
        public DateTime Acum_FinAct { get; set; }

        [ForeignKey("ObjEmpresa")]
        public int Oemp_Id { get; set; }
        public ObjEmpresa ObjEmpresa { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Periodo")]
        public int Peri_Id { get; set; }
        public Periodo Periodo { get; set; }

        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }

        [ForeignKey("Frecuencia")]
        public int Frec_Id { get; set; }
        public Frecuencia Frecuencia { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }

    }
}