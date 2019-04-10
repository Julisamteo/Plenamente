using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class CargoEmpresa
    {
        [Key]
        public int Cemp_Id { get; set; }
        public string Cemp_Nom { get; set; }
        //Foreign key Para empresa
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime Cemp_Registro { get; set; }

        public ICollection<Persona> Personas { get; set; }

    }
}