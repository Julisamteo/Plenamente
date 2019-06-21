using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plenamente.Models.ViewModel
{
    public class CicloPHVAViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public List<CriteriosViewModel> Criterios { get; set; }
    }
    public class CriteriosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Registro { get; set; }
        public List<EstandaresViewModel> Estandares { get; set; }
    }
    public class EstandaresViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Registro { get; set; }
        public List<ElementoViewModel> Elementos { get; set; }
    }
    public class ElementoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Verificar { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Periodo { get; set; }
        public string Observaciones { get; set; }
        public DateTime Registro { get; set; }
        public string Video { get; set; }
        public string Recurso { get; set; }
        public string Reursob { get; set; }
        public string Reursoc { get; set; }
        public string Reursod { get; set; }
        public string Reursoe { get; set; }
        public string Reursof { get; set; }

        public short Categoria { get; set; }
        public List<Cumplimiento> Cumplimientos { get; set; }
        public bool ExisteCumplimiento => Cumplimientos.Count() > 0;
        public string MasInformacion { get; set; }
    }
    public class CumplimientoViewModel
    {
        public CumplimientoViewModel() { }
        public int Id { get; set; }
        [Display(Name = "Cumple")]
        public bool Cumple { get; set; }
        [Display(Name = "No cumple")]
        public bool Nocumple { get; set; }
        [Display(Name = "No aplica")]
        public bool NoAplica { get; set; }
        [Display(Name = "Justifica")]
        public bool Justifica { get; set; }
        [Display(Name = "No justifica")]
        public bool Nojustifica { get; set; }
        [Required(ErrorMessage = "Las observaciones son requeridas.")]
        [StringLength(256)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        public int? ItemEstandarId { get; set; }
        public ElementoViewModel ItemEstandar { get; set; }
        public int? Nit { get; set; }
        public int AutoEvaluacionId { get; set; }
        public DateTime Registro { get; set; }
        public List<AcumMes> AcumMes { get; set; }
        //=> Cumplimiento.AcumMes?.ToList();
        public List<Evidencia> Evidencias { get; set; }
        //=> Cumplimiento.Evidencias?.ToList();
    }
}