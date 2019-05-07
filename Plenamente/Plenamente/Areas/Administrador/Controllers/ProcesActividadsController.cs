using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class ProcesActividadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ProcesActividads
        public ActionResult Index()
        {
            return View(db.Tb_ProcesActividad.ToList());
        }

        // GET: Administrador/ProcesActividads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            if (procesActividad == null)
            {
                return HttpNotFound();
            }
            return View(procesActividad);
        }

        // GET: Administrador/ProcesActividads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/ProcesActividads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pact_Id,Pact_Nombre,Pact_Registro")] ProcesActividad procesActividad)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ProcesActividad.Add(procesActividad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(procesActividad);
        }

        // GET: Administrador/ProcesActividads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            if (procesActividad == null)
            {
                return HttpNotFound();
            }
            return View(procesActividad);
        }

        // POST: Administrador/ProcesActividads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pact_Id,Pact_Nombre,Pact_Registro")] ProcesActividad procesActividad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procesActividad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procesActividad);
        }

        // GET: Administrador/ProcesActividads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            if (procesActividad == null)
            {
                return HttpNotFound();
            }
            return View(procesActividad);
        }

        // POST: Administrador/ProcesActividads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            db.Tb_ProcesActividad.Remove(procesActividad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
