using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class AutoEvaluacion
    {
        public IEnumerable<Criterio> Criterios1 { get; set; }
        public IEnumerable<Estandar> Estandars11 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars111 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos111 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars112 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos112 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars113 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos113 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars114 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos114 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars115 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos115 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars116 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos116 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars117 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos117 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars118 {get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos118 { get; set; }

        public IEnumerable<Estandar> Estandars12 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars121 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos121 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars122 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos122 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars123 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos123 { get; set; }


        public IEnumerable<Criterio> Criterios2 { get; set; }
        public IEnumerable<Estandar> Estandars21 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars211 { get; set; }
        public IEnumerable<Cumplimiento> cumplimientos { get; set; }


        public IEnumerable<Criterio> Criterios3 { get; set; }
        public IEnumerable<Criterio> Criterios4 { get; set; }
        public IEnumerable<Criterio> Criterios5 { get; set; }
        public IEnumerable<Criterio> criterios6 { get; set; }
        public IEnumerable<Criterio> Criterios7 { get; set; }





        public virtual ICollection<Cumplimiento> Cumplimientos { get; set; }
        public IEnumerable<Estandar> Estandars { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars { get; set; }
        


    }
}