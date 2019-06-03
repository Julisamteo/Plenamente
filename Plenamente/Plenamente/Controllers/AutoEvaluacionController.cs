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
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult AutoevaluacionSST()
        {
            List<CriteriosViewModel> list = db.Tb_Criterio.Select(c => new CriteriosViewModel(c)).ToList();
            return View(list);
        }
        [Authorize]
        public ActionResult Cumplimiento(int id)
        {
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            return View(new CumplimientoViewModel(cumplimiento));
        }
    }
}