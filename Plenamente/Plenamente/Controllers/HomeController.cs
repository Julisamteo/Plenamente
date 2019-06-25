using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<EventViewModel> lst = new List<EventViewModel>();
            try
            {
                lst =
                    db.Tb_ProgamacionTareas
                        .Where(a => a.ActiCumplimiento.Empr_Nit == AccountData.NitEmpresa
                                && a.Estado
                                && a.ActiCumplimiento.Usersplandetrabajo.Count > 0)
                        .Select(a =>
                            new EventViewModel
                            {
                                Id = a.Id,
                                Description = "Tarea programada",
                                Title = a.Descripcion,
                                Start = a.FechaHora,
                                BackgroundColor = "#7DDAFF",
                                BorderColor = "#9FBDC9",
                                EventRoute = "/ActividadCumplimiento/Create/" + a.Id
                            }).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View(lst);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}