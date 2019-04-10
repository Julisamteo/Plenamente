using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class AreaEmpresa
    {
        [Key]
        public int Aemp_Id { get; set; }
        public string Aemp_Nom { get; set; }
        //Foreign key Para empresa
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime Aemp_Registro { get; set; }
        //Permite a personas acceder a la Data
        public ICollection<Persona> Personas { get; set; }
    }
}