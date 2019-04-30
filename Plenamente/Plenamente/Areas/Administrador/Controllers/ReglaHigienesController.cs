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
    public class ReglaHigienesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ReglaHigienes
        public ActionResult Index()
        {
            var tb_ReglaHigiene = db.Tb_ReglaHigiene.Include(r => r.Empresa);
            return View(tb_ReglaHigiene.ToList());
        }

        // GET: Administrador/ReglaHigienes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            if (reglaHigiene == null)
            {
                return HttpNotFound();
            }
            return View(reglaHigiene);
        }

        // GET: Administrador/ReglaHigienes/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/ReglaHigienes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rhig_Id,Rhig_Archivo,Empr_Nit,Rhig_Registro")] ReglaHigiene reglaHigiene)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ReglaHigiene.Add(reglaHigiene);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaHigiene.Empr_Nit);
            return View(reglaHigiene);
        }

        // GET: Administrador/ReglaHigienes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            if (reglaHigiene == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaHigiene.Empr_Nit);
            return View(reglaHigiene);
        }

        // POST: Administrador/ReglaHigienes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rhig_Id,Rhig_Archivo,Empr_Nit,Rhig_Registro")] ReglaHigiene reglaHigiene)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reglaHigiene).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaHigiene.Empr_Nit);
            return View(reglaHigiene);
        }

        // GET: Administrador/ReglaHigienes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            if (reglaHigiene == null)
            {
                return HttpNotFound();
            }
            return View(reglaHigiene);
        }

        // POST: Administrador/ReglaHigienes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            db.Tb_ReglaHigiene.Remove(reglaHigiene);
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
