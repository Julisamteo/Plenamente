using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Empresa
    {
        [Key]
        public int Empr_Nit { get; set; }
        public string Empr_Nom { get; set; }
        public string Empr_Dir { get; set; }
        // ForeignKey ARL
        public int Arl_Id { get; set; }
        public Arl Arl { get; set; }
        //ForeignKey ClaseArl
        public int Carl_Id { get; set; }
        public ClaseArl claseArl { get; set; }
        public int Empr_Afiarl { get; set; }
        public int Empr_Ttrabaja { get; set; }
        public int Empr_Itrabaja { get; set; }
        public DateTime Empr_Registro { get; set; }

        // Permite que Cargo Empresa acceda a la data
        public ICollection<CargoEmpresa> CargoEmpresas { get; set; }
        // Permite que Persona acceda a la Data
        public ICollection<Persona> Personas { get; set; }
        // Permite que SedeCiudad Acceda a la Data
        public ICollection<SedeCiudad> SedeCiudades { get; set; }
        // Permite que Jornada Empresa Acceda a la Data
        public ICollection<JornadaEmpresa> jornadaEmpresas { get; set; }
        // Permite que Area Empresa Acceda a la Data
        public ICollection<AreaEmpresa> AreaEmpresas { get; set; }
    }
}