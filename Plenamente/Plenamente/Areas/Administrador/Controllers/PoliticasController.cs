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
    public class PoliticasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Politicas
        public ActionResult Index()
        {
            var tb_politica = db.Tb_politica.Include(p => p.Empresa);
            return View(tb_politica.ToList());
        }

        // GET: Administrador/Politicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politica politica = db.Tb_politica.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            return View(politica);
        }

        // GET: Administrador/Politicas/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/Politicas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Poli_Id,Poli_Archivo,Empr_Nit,Poli_Registro")] Politica politica)
        {
            if (ModelState.IsValid)
            {
                db.Tb_politica.Add(politica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", politica.Empr_Nit);
            return View(politica);
        }

        // GET: Administrador/Politicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politica politica = db.Tb_politica.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", politica.Empr_Nit);
            return View(politica);
        }

        // POST: Administrador/Politicas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Poli_Id,Poli_Archivo,Empr_Nit,Poli_Registro")] Politica politica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(politica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", politica.Empr_Nit);
            return View(politica);
        }

        // GET: Administrador/Politicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politica politica = db.Tb_politica.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            return View(politica);
        }

        // POST: Administrador/Politicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Politica politica = db.Tb_politica.Find(id);
            db.Tb_politica.Remove(politica);
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
