using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Usersplandetrabajo
    {
        [Key]
        public int UsPl_Id { get; set; }

        [ForeignKey("PlanTrabajo")]
        public int PlaT_Id { get; set; }
        public PlandeTrabajo PlandeTrabajo { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}