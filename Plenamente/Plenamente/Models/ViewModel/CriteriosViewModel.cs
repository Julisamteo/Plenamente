using System;
using System.Collections.Generic;
using System.Linq;

namespace Plenamente.Models.ViewModel
{
    public class CriteriosViewModel
    {
        public Criterio Criterio { get; set; }
        public CriteriosViewModel() { }
        public int Id { get => Criterio.Crit_Id; set => Criterio.Crit_Id = value; }
        public string Nombre { get => Criterio.Crit_Nom; set => Criterio.Crit_Nom = value; }
        public float Porcentaje { get => Criterio.Crit_Porcentaje; set => Criterio.Crit_Porcentaje = value; }
        public DateTime Registro { get => Criterio.Crit_Registro; set => Criterio.Crit_Registro = value; }
        public List<EstandaresViewModel> Estandares => Criterio.Estandars.Select(e => new EstandaresViewModel { Estandar = e }).ToList();
    }
    public class EstandaresViewModel
    {
        public Estandar Estandar { get; set; }
        public EstandaresViewModel() { }
        public int Id { get => Estandar.Esta_Id; set => Estandar.Esta_Id = value; }
        public string Nombre { get => Estandar.Esta_Nom; set => Estandar.Esta_Nom = value; }
        public float Porcentaje { get => Estandar.Esta_Porcentaje; set => Estandar.Esta_Porcentaje = value; }
        public DateTime Registro { get => Estandar.Esta_Registro; set => Estandar.Esta_Registro = value; }
        public List<ElementoViewModel> Elementos => Estandar.itemEstandars.Select(e => new ElementoViewModel { ItemEstandar = e }).ToList();
    }
    public class ElementoViewModel
    {
        public ItemEstandar ItemEstandar { get; set; }
        public ElementoViewModel() { }
        public int Id { get => ItemEstandar.Iest_Id; set => ItemEstandar.Iest_Id = value; }
        public string Descripcion { get => ItemEstandar.Iest_Desc; set => ItemEstandar.Iest_Desc = value; }
        public string Verificar { get => ItemEstandar.Iest_Verificar; set => ItemEstandar.Iest_Verificar = value; }
        public float Porcentaje { get => ItemEstandar.Iest_Porcentaje; set => ItemEstandar.Iest_Porcentaje = value; }
        public DateTime Periodo { get => ItemEstandar.Iest_Peri; set => ItemEstandar.Iest_Peri = value; }
        public string Observaciones { get => ItemEstandar.Iest_Observa; set => ItemEstandar.Iest_Observa = value; }
        public DateTime Registro { get => ItemEstandar.Iest_Registro; set => ItemEstandar.Iest_Registro = value; }
        public string Video { get => ItemEstandar.Iest_Video; set => ItemEstandar.Iest_Video = value; }
        public string Recurso { get => ItemEstandar.Iest_Recurso; set => ItemEstandar.Iest_Recurso = value; }
        public string Reursob { get => ItemEstandar.Iest_Rescursob; set => ItemEstandar.Iest_Rescursob = value; }
        public List<CumplimientoViewModel> Cumplimientos => ItemEstandar.Cumplimientos.Select(e => new CumplimientoViewModel { Cumplimiento = e }).ToList();
    }
    public class CumplimientoViewModel
    {
        public Cumplimiento Cumplimiento { get; set; }
        public CumplimientoViewModel() { }
        public int Id { get => Cumplimiento.Cump_Id; set => Cumplimiento.Cump_Id = value; }
        public bool Cumple { get => Cumplimiento.Cump_Cumple; set => Cumplimiento.Cump_Cumple = value; }
        public bool Nocumple { get => Cumplimiento.Cump_Nocumple; set => Cumplimiento.Cump_Nocumple = value; }
        public bool Justifica { get => Cumplimiento.Cump_Justifica; set => Cumplimiento.Cump_Justifica = value; }
        public bool Nojustifica { get => Cumplimiento.Cump_Nojustifica; set => Cumplimiento.Cump_Nojustifica = value; }
        public string Observaciones { get => Cumplimiento.Cump_Observ; set => Cumplimiento.Cump_Observ = value; }
        public int? ItemEstandarId { get => Cumplimiento.Iest_Id; set => Cumplimiento.Iest_Id = value; }
        public int? Nit { get => Cumplimiento.Empr_Nit; set => Cumplimiento.Empr_Nit = value; }
        public int AutoEvaluacionId { get => Cumplimiento.Auev_Id; set => Cumplimiento.Auev_Id = value; }
        public DateTime Registro { get => Cumplimiento.Cump_Registro; set => Cumplimiento.Cump_Registro = value; }
        public List<AcumMes> AcumMes => Cumplimiento.AcumMes.ToList();
        public List<Evidencia> Evidencias => Cumplimiento.Evidencias.ToList();
    }
}