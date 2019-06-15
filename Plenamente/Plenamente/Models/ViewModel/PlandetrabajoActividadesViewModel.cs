using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class PlandetrabajoActividadesViewModel
    {      
        public int IdPlantTrabajo { get; set; }
        public string NombrePlanTrabajo { get; set; }
        public string IdUser { get; set; }
        public int IdActiCumplimiento { get; set; }
        public string NombreCumplimiento { get; set; }
    }
}