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
    public class ActividadCumplimientoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ActividadCumplimiento
        public ActionResult Index()
        {
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);

            

            return View();
        }

        // GET: ActividadCumplimiento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActividadCumplimiento/Create
        public ActionResult Create()
        {
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);

            var model = new ViewModelActividadCumplimiento();

            return View(model);
            
        }

        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia")] ViewModelActividadCumplimiento model)
        {
            try
            {
<<<<<<< HEAD

                // TODO: Add insert logic here

=======
                Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
                ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
                // TODO: Add insert logic here
                ActiCumplimiento actcumplimiento = new ActiCumplimiento
                {
                    Acum_Desc = model.NombreActividad,
                    Acum_IniAct = model.FechaInicial,
                    Acum_FinAct = model.FechaFinal,
                    Frec_Id = 1,
                    Peri_Id = 1
                };
                
                db.Tb_ActiCumplimiento.Add(actcumplimiento);
                db.SaveChanges();
>>>>>>> 6803b099c3c6332f9350332c05118910bee033b6
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActividadCumplimiento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActividadCumplimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
