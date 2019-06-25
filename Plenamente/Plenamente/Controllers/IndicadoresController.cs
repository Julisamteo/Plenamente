using Plenamente.App_Tool;
using Plenamente.Models;
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
                   title = "Medición del ciclo PHVA",
                   labels = db.Tb_CicloPHVA.Select(a => a.Nombre).ToArray(),
                   datasets =
                   new List<ChartDatasetsViewModel>{
                        new ChartDatasetsViewModel
                        {
                            label = "Avance",
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
                List<ActiCumplimiento> cumplimientos =
                    db.Tb_ActiCumplimiento.Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null)).ToList();
                if (cumplimientos != null && cumplimientos.Count > 0)
                {
                    lst.AddRange(cumplimientos);
                }

                CultureInfo ci = new CultureInfo("es-CO");
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
            DateTime date = DateTime.Now;
            DateTime start = date.AddDays(-date.Day);
            DateTime end = start.AddMonths(1);
            List<ActiCumplimiento> lst =
                   db.Tb_ActiCumplimiento.Where(
                       a => a.Empr_Nit == AccountData.NitEmpresa
                       && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null)
                       && a.Acum_IniAct >= start
                       && a.Acum_IniAct < end).ToList();
            int total = lst.Count();
            int ended = lst.Where(a => !a.Finalizada).Count();
            ChartDataViewModel datos =
              new ChartDataViewModel
              {
                  title = $"Cumplimiento SG-SST {total} Actividades",
                  labels = new string[2] { "Finalizadas", "En ejecución" },
                  datasets =
                  new List<ChartDatasetsViewModel>{
                      new ChartDatasetsViewModel{
                          label = "Estado actividades",
                          data = new int[2]{ ended, total-ended  },
                          fill = false,
                          backgroundColor = new string[2] { "#FF1F17", "#6CB52D" },
                          borderColor = new string[2] { "#FF6963", "#65ac1e" },
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