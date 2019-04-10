using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Genero
    {
        [Key]
        public int Gene_Id { get; set; }
        public string Gene_Nom { get; set; }
        public DateTime Gene_Registro { get; set; }
        //Nombre de la colección definido en el prural de Genero <Generos>
        public ICollection<Genero> Generos { get; set; }
    }
}