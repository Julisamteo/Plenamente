using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Arl
    {
        [Key]
        public int Arl_Id { get; set; }
        public string Arl_Nom { get; set; }
        public DateTime Arl_Registro { get; set; }

        public ICollection<Empresa> Empresas { get; set; }
    }
}