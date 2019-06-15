using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.App_Tool;
using Plenamente.Models;

namespace Plenamente.Controllers
{
    public class PlandeTrabajoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlandeTrabajo
        public ActionResult Index()
        {

            var tb_PlandeTrabajo = db.Tb_PlandeTrabajo.Where(p => p.Emp_Id==AccountData.NitEmpresa).ToList();
            return View(tb_PlandeTrabajo);
        }

        // GET: PlandeTrabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
            if (plandeTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(plandeTrabajo);
        }

        // GET: PlandeTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.Emp_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: PlandeTrabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Plat_Id,Plat_Nom,Emp_Id")] PlandeTrabajo plandeTrabajo)
        {
            if (ModelState.IsValid)
            {
                var nombreplan = db.Tb_PlandeTrabajo.Where(c => c.Emp_Id == plandeTrabajo.Emp_Id && c.Plat_Nom == plandeTrabajo.Plat_Nom).ToList();
                if (nombreplan.Count>0)
                {
                    ViewBag.TextError = "Nombre del plan de trabajo repetido";
                    return View(plandeTrabajo);
                }
                db.Tb_PlandeTrabajo.Add(plandeTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(plandeTrabajo);
        }

        // GET: PlandeTrabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
            if (plandeTrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Emp_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", plandeTrabajo.Emp_Id);
            return View(plandeTrabajo);
        }

        // POST: PlandeTrabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Plat_Id,Plat_Nom,Emp_Id")] PlandeTrabajo plandeTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plandeTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Emp_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", plandeTrabajo.Emp_Id);
            return View(plandeTrabajo);
        }

        // GET: PlandeTrabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
            if (plandeTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(plandeTrabajo);
        }

        // POST: PlandeTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
            db.Tb_PlandeTrabajo.Remove(plandeTrabajo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ActividadesPlanTrabajo()
        {
           
            return View();
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
