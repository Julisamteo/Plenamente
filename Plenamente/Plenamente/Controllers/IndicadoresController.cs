using Plenamente.Models;
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

    }
}