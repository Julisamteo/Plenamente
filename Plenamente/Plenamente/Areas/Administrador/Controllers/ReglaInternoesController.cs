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
    public class ReglaInternoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ReglaInternoes
        public ActionResult Index()
        {
            var tb_ReglaInterno = db.Tb_ReglaInterno.Include(r => r.Empresa);
            return View(tb_ReglaInterno.ToList());
        }

        // GET: Administrador/ReglaInternoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            if (reglaInterno == null)
            {
                return HttpNotFound();
            }
            return View(reglaInterno);
        }

        // GET: Administrador/ReglaInternoes/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/ReglaInternoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rint_Id,Rint_Archivo,Empr_Nit,Rint_Registro")] ReglaInterno reglaInterno)
        {

            if (ModelState.IsValid)
            {
             

                db.Tb_ReglaInterno.Add(reglaInterno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //byte[] uploadedRint_Archivo = new byte[ReglaInterno.Rint_Archivo]
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaInterno.Empr_Nit);
            return View(reglaInterno);
        }

        // GET: Administrador/ReglaInternoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            if (reglaInterno == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaInterno.Empr_Nit);
            return View(reglaInterno);
        }

        // POST: Administrador/ReglaInternoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rint_Id,Rint_Archivo,Empr_Nit,Rint_Registro")] ReglaInterno reglaInterno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reglaInterno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaInterno.Empr_Nit);
            return View(reglaInterno);
        }

        // GET: Administrador/ReglaInternoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            if (reglaInterno == null)
            {
                return HttpNotFound();
            }
            return View(reglaInterno);
        }

        // POST: Administrador/ReglaInternoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            db.Tb_ReglaInterno.Remove(reglaInterno);
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
