using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    [Authorize]
    public class IndicadoresController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult PromedioAutoevaluaciones()
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (AccountData.NitEmpresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Cumplimientos.Count > 0 && a.Finalizada)
                    .OrderByDescending(a => a.Auev_Inicio)
                    .FirstOrDefault();
            decimal[] lst = new decimal[0];
            string[] labels = new string[0];
            if (evaluacion != null)
            {
                var values =
                   db.Tb_ItemEstandar
                          .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                          .GroupBy(a => a.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Count(), name = a.Key.Nombre })
                          .ToArray();
                labels = values.Select(v => v.name).ToArray();
                var temp =
                      db.Tb_Cumplimiento
                          .Where(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandar.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        lst[i] = (val.value * 100 / values[i].value);
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "Medición del ciclo PHVA",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            }},
               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult AvanceAutoevaluaciones()
        {
            List<ActiCumplimiento> lst = new List<ActiCumplimiento>();
            try
            {
                var cumplimientos =
                    db.Tb_ActiCumplimiento.Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null)).ToList();
                if (cumplimientos != null && cumplimientos.Count > 0)
                {
                    lst.AddRange(cumplimientos);
                }

                var ci = new CultureInfo("es-CO");
                ChartDataViewModel datos =
                  new ChartDataViewModel
                  {
                      title = "Alcance del plan de trabajo anual",
                      labels = lst.Select(a => a.Acum_IniAct.ToString("MMMM", ci)).Distinct().ToArray(),
                      datasets =
                          new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Planeadas",
                                data = lst.GroupBy(a => a.Acum_IniAct.ToString("MMMM", ci)).Select(a => a.Count()).ToArray(),
                                fill = false,
                                borderWidth = 1
                            },
                            new ChartDatasetsViewModel
                            {
                                label = "En ejecución",
                                data = lst.Where(a => !a.Finalizada).GroupBy(a => a.Acum_IniAct.ToString("MMMM", ci)).Select(a => a.Count()).ToArray(),
                                fill = false,
                                borderWidth = 1
                            }},
                  };
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult UltimaAutoevaluacion()
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (AccountData.NitEmpresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            int total =
                db.Tb_ItemEstandar
                    .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria)).Count();
            int terminadas = 0;
            if (AccountData.NitEmpresa > 0)
            {
                AutoEvaluacion evaluacion =
                    db.Tb_AutoEvaluacion
                        .Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Cumplimientos.Count > 0 && a.Finalizada)
                        .OrderByDescending(a => a.Auev_Inicio)
                        .FirstOrDefault();

                if (evaluacion != null)
                {
                    terminadas =
                     db.Tb_Cumplimiento
                       .Count(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica));
                }
            }

            ChartDataViewModel datos =
              new ChartDataViewModel
              {
                  title = "Cumplimiento SG-SST",
                  labels = new string[2] { "Cumplido", "No cumplido" },
                  datasets =
                  new List<ChartDatasetsViewModel>{
                      new ChartDatasetsViewModel{
                          label = "Estado actividades",
                          data = new int[2]{ terminadas, total-terminadas  },
                          fill = true,
                          borderWidth = 1,
                          backgroundColor = new string[2] { "#6DB52D", "#AE2429" },
                          borderColor = new string[2] { "#6DB52D", "#AE2429" }
                      }}
              };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        private struct ChartDataViewModel
        {
            public string title { get; set; }
            public string[] labels { get; set; }
            public List<ChartDatasetsViewModel> datasets { get; set; }
        };
        private struct ChartDatasetsViewModel
        {
            public string label { get; set; }
            public object data { get; set; }
            public string[] backgroundColor { get; set; }
            public string[] borderColor { get; set; }
            public short borderWidth { get; set; }
            public bool fill { get; set; }
        };
    }
}