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
        public ActiCumplimiento()
        {
            Acum_Registro = DateTime.Now;
        }
        [Key]
        [Display(Name = "Actividad Id")]
        public int Acum_Id { get; set; }

        [Display(Name = "Descripción")]
        public string Acum_Desc { get; set; }

        [Display(Name = "Meta")]
        public float Acum_Porcentest { get; set; }

        [Display(Name = "Cargue Evidencia")]
        public string Acum_Ejec { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        public DateTime Acum_Registro { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Incio")]
        public DateTime Acum_IniAct { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Finalización")]
        public DateTime Acum_FinAct { get; set; }

        
        [ForeignKey("ObjEmpresa")]
        [Display(Name = "Objetivo")]
        public int Oemp_Id { get; set; }
        public ObjEmpresa ObjEmpresa { get; set; }

        
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Responsable")]
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