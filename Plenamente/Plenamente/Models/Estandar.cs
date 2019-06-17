using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Estandar
    {
        [Key]
        public int Esta_Id { get; set; }
        public string Esta_Nom { get; set; }
        public float Esta_Porcentaje { get; set; }

        [ForeignKey("Criterio")]
        public int Crit_Id { get; set; }
        public Criterio Criterio { get; set; }

        public DateTime Esta_Registro { get; set; }

        // Permite a ItemStandar
        public ICollection<ItemEstandar> itemEstandars { get; set; }
        public short Categoria { get; set; }
    }
}