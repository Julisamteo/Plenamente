using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class AutoevaluacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Autoevaluacion
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult AutoevaluacionSST()
        {
            List<CriteriosViewModel> list =
                db.Tb_Criterio
                    .Select(c =>
                        new CriteriosViewModel
                        {
                            Id = c.Crit_Id,
                            Nombre = c.Crit_Nom,
                            Porcentaje = c.Crit_Porcentaje,
                            Registro = c.Crit_Registro,
                            Estandares =
                            c.Estandars.Select(e =>
                                new EstandaresViewModel
                                {
                                    Id = e.Esta_Id,
                                    Nombre = e.Esta_Nom,
                                    Porcentaje = e.Esta_Porcentaje,
                                    Registro = e.Esta_Registro,
                                    Elementos =
                                        e.itemEstandars.Select(i =>
                                            new ElementoViewModel
                                            {
                                                Id = i.Iest_Id,
                                                Descripcion = i.Iest_Desc,
                                                Observaciones = i.Iest_Observa,
                                                Porcentaje = i.Iest_Porcentaje,
                                                Recurso = i.Iest_Recurso,
                                                Registro = i.Iest_Registro,
                                                Reursob = i.Iest_Rescursob,
                                                Verificar = i.Iest_Verificar,
                                                Video = i.Iest_Video,
                                                Periodo = i.Iest_Peri
                                            }).ToList()
                                }).ToList(),
                        }).ToList();
            return View(list);
        }
    }
}