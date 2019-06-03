using System;
using System.Collections.Generic;
using System.Linq;

namespace Plenamente.Models.ViewModel
{
    /// <summary></summary>
    public class CriteriosViewModel
    {
        public CriteriosViewModel(Criterio criterio)
        {
            Id = criterio.Crit_Id;
            Nombre = criterio.Crit_Nom;
            Porcentaje = criterio.Crit_Porcentaje;
            Registro = criterio.Crit_Registro;
            Estandares = criterio.Estandars.Select(e => new EstandaresViewModel(e)).ToList();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Registro { get; set; }
        public List<EstandaresViewModel> Estandares { get; set; }
    }
    public class EstandaresViewModel
    {
        public EstandaresViewModel(Estandar estandar)
        {
            Id = estandar.Esta_Id;
            Nombre = estandar.Esta_Nom;
            Porcentaje = estandar.Esta_Porcentaje;
            Registro = estandar.Esta_Registro;
            Elementos = estandar.itemEstandars.Select(i => new ElementoViewModel(i)).ToList();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Registro { get; set; }
        public List<ElementoViewModel> Elementos { get; set; }
    }
    public class ElementoViewModel
    {
        public ElementoViewModel(ItemEstandar item)
        {
            Id = item.Iest_Id;
            Descripcion = item.Iest_Desc;
            Observaciones = item.Iest_Observa;
            Porcentaje = item.Iest_Porcentaje;
            Recurso = item.Iest_Recurso;
            Registro = item.Iest_Registro;
            Reursob = item.Iest_Rescursob;
            Verificar = item.Iest_Verificar;
            Video = item.Iest_Video;
            Periodo = item.Iest_Peri;
            Cumplimientos = item.Cumplimientos.Select(c => new CumplimientoViewModel(c)).ToList();
        }
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
        public List<CumplimientoViewModel> Cumplimientos { get; set; }
    }
    /// <summary></summary>
    public class CumplimientoViewModel
    {
        public CumplimientoViewModel(Cumplimiento cumplimiento)
        {
            Id = cumplimiento.Cump_Id;
            Cumple = cumplimiento.Cump_Cumple;
            Nocumple = cumplimiento.Cump_Nocumple;
            Justifica = cumplimiento.Cump_Justifica;
            Nojustifica = cumplimiento.Cump_Nojustifica;
            Observaciones = cumplimiento.Cump_Observ;
            ItemEstandarId = cumplimiento.Iest_Id;
            Nit = cumplimiento.Empr_Nit;
            AutoEvaluacionId = cumplimiento.Auev_Id;
            Registro = cumplimiento.Cump_Registro;
        }
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        public bool Cumple { get; set; }
        public bool Nocumple { get; set; }
        public bool Justifica { get; set; }
        public bool Nojustifica { get; set; }
        public string Observaciones { get; set; }
        public int? ItemEstandarId { get; set; }
        public int? Nit { get; set; }
        public int AutoEvaluacionId { get; set; }
        public DateTime Registro { get; set; }
        public List<AcumMes> AcumMes { get; set; }
        public List<Evidencia> Evidencias { get; set; }
    }
}