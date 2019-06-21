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
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   labels = db.Tb_CicloPHVA.Select(a => a.Nombre).ToArray(),
                   datasets =
                   new List<ChartDatasetsViewModel>{
                        new ChartDatasetsViewModel
                        {
                            label = "Promedio autoevaluaciones",
                            data = db.Tb_CicloPHVA.Select(a => a.Id > 0 ? 1 : 0).ToArray(),
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
                var cumplimientos = db.Tb_ActiCumplimiento.Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null)).ToList();
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
            ChartDataViewModel datos =
              new ChartDataViewModel
              {
                  labels = db.Tb_ItemEstandar.Select(a => a.Esta_Id.ToString()).ToArray(),
                  datasets =
                  new List<ChartDatasetsViewModel>{
                      new ChartDatasetsViewModel{
                          label = "Última autoevaluación",
                          data = db.Tb_ItemEstandar.Select(a => a.Cumplimientos.Any(c => c.Cump_Cumple || c.Cump_Justifica || c.Cump_NoAplica) ? a.Iest_Porcentaje : 0).ToArray(),//db.Tb_Cumplimiento.Where(a => a.Cump_Cumple || a.Cump_Justifica || a.Cump_NoAplica ? 1 : 0).Select(a => a.Criterios.Sum(c => c.Crit_Porcentaje)).ToArray(),
                          fill = false,
                          borderWidth = 1
                      }},
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