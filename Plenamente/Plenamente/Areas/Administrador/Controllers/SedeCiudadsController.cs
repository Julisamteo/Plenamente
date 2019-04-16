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
    public class SedeCiudadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/SedeCiudads
        public ActionResult Index()
        {
            var tb_SedeCiudad = db.Tb_SedeCiudad.Include(s => s.Ciudad);
            return View(tb_SedeCiudad.ToList());
        }

        // GET: Administrador/SedeCiudads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            if (sedeCiudad == null)
            {
                return HttpNotFound();
            }
            return View(sedeCiudad);
        }

        // GET: Administrador/SedeCiudads/Create
        public ActionResult Create()
        {
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom");
            return View();
        }

        // POST: Administrador/SedeCiudads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sciu_Id,Sciu_Nom,Ciud_Id,Empr_id,Sciu_Registro")] SedeCiudad sedeCiudad)
        {
            if (ModelState.IsValid)
            {
                db.Tb_SedeCiudad.Add(sedeCiudad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", sedeCiudad.Ciud_Id);
            return View(sedeCiudad);
        }

        // GET: Administrador/SedeCiudads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            if (sedeCiudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", sedeCiudad.Ciud_Id);
            return View(sedeCiudad);
        }

        // POST: Administrador/SedeCiudads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sciu_Id,Sciu_Nom,Ciud_Id,Empr_id,Sciu_Registro")] SedeCiudad sedeCiudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sedeCiudad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", sedeCiudad.Ciud_Id);
            return View(sedeCiudad);
        }

        // GET: Administrador/SedeCiudads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            if (sedeCiudad == null)
            {
                return HttpNotFound();
            }
            return View(sedeCiudad);
        }

        // POST: Administrador/SedeCiudads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            db.Tb_SedeCiudad.Remove(sedeCiudad);
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
