using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Ciudad
    {
       [Key]
       public int Ciud_Id { get; set; }
        public string Ciud_Nom { get; set; }
        public DateTime Ciud_Registro { get; set; }
        //Nombre de la colección definido en el prural de ciudad <Ciudades>
        public ICollection<Ciudad> Ciudades { get; set; }
    }
}