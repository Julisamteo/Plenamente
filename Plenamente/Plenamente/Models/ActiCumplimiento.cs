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
        [Display(Name = "Descripción")]
        public string Acum_Desc { get; set; }
        [Display(Name = "Meta (%)")]
        public float Acum_Porcentest { get; set; }
        [Display(Name = "Cargue Evidencia")]
        public string Acum_Ejec { get; set; }
        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        public DateTime Acum_Registro { get; set; }
        [Display(Name = "Incio Actividad")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Acum_IniAct { get; set; }
        [Display(Name = "Finalización de Actividad")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Acum_FinAct { get; set; }

        [ForeignKey("ObjEmpresa")]
        [Display(Name = "Objetivos empresa")]
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
        public string DiasSemana { get; set; }
        public int Repeticiones { get; set; }

        public TimeSpan HoraAct { get; set; } 

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }      
        public ICollection<UsuariosPlandetrabajo> Usersplandetrabajo { get; set; }
        public ICollection<ProgamacionTareas> ProgamacionTareas { get; set; }


    }
}