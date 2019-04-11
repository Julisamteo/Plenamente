using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Periodo
    {
        [Key]
        public int Peri_Id { get; set; }
        public string Peri_Nom { get; set; }
        public DateTime Peri_Registro { get; set; }

        //Permite que Acticumplmientos acceda a la data
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
    }
}