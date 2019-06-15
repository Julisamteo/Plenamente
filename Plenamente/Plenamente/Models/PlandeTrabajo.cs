using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class PlandeTrabajo
    {
        [Key]
        public int Plat_Id { get; set; }
        [Display(Name = "Nombre Plan de trabajo")]
        public string Plat_Nom { get; set; }
         [ForeignKey("empresa")]
        public int Emp_Id { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<UsuariosPlandetrabajo> Usersplandetrabajo { get; set; }
        public Empresa empresa { get; set; }

    }
}