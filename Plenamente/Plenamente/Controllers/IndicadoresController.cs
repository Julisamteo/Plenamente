using Plenamente.Models;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PromedioAutoevaluaciones()
        {
            ChartDataViewModel datos =
                new ChartDataViewModel
                {
                    label = "Promedio autoevaluaciones",
                    labels = db.Tb_CicloPHVA.Select(a => a.Nombre).ToArray(),
                    other = db.Tb_CicloPHVA.Select(a => a.Id.ToString()).ToArray(),
                    data = db.Tb_CicloPHVA.Select(a => a.Id > 0 ? 1 : 0).ToArray(),//db.Tb_Cumplimiento.Where(a => a.Cump_Cumple || a.Cump_Justifica || a.Cump_NoAplica ? 1 : 0).Select(a => a.Criterios.Sum(c => c.Crit_Porcentaje)).ToArray(),
                    fill = false,
                    borderWidth = 1
                };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AvanceAutoevaluaciones()
        {
            ChartDataViewModel datos =
              new ChartDataViewModel
              {
                  label = "Promedio criterios",
                  labels = db.Tb_Estandar.Select(a => a.Esta_Id.ToString()).ToArray(),
                  other = db.Tb_Estandar.Select(a => a.Esta_Id.ToString()).ToArray(),
                  data = db.Tb_Estandar.Select(a => a.itemEstandars.Any(c => c.Cumplimientos.Any(d => d.Cump_Cumple || d.Cump_Justifica || d.Cump_NoAplica)) ? a.Esta_Porcentaje : 0).ToArray(),//db.Tb_Cumplimiento.Where(a => a.Cump_Cumple || a.Cump_Justifica || a.Cump_NoAplica ? 1 : 0).Select(a => a.Criterios.Sum(c => c.Crit_Porcentaje)).ToArray(),
                  fill = false,
                  borderWidth = 1
              };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UltimaAutoevaluacion()
        {
            ChartDataViewModel datos =
              new ChartDataViewModel
              {
                  label = "Última autoevaluación",
                  labels = db.Tb_ItemEstandar.Select(a => a.Esta_Id.ToString()).ToArray(),
                  other = db.Tb_ItemEstandar.Select(a => a.Iest_Id.ToString()).ToArray(),
                  data = db.Tb_ItemEstandar.Select(a => a.Cumplimientos.Any(c => c.Cump_Cumple || c.Cump_Justifica || c.Cump_NoAplica) ? a.Iest_Porcentaje : 0).ToArray(),//db.Tb_Cumplimiento.Where(a => a.Cump_Cumple || a.Cump_Justifica || a.Cump_NoAplica ? 1 : 0).Select(a => a.Criterios.Sum(c => c.Crit_Porcentaje)).ToArray(),
                  fill = false,
                  borderWidth = 1
              };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        private struct ChartDataViewModel
        {
            public string label { get; set; }
            public string[] labels { get; set; }
            public object data { get; set; }
            public string[] other { get; set; }
            public string[] backgroundColor { get; set; }
            public string[] borderColor { get; set; }
            public short borderWidth { get; set; }
            public bool fill { get; set; }
        };
    }
}