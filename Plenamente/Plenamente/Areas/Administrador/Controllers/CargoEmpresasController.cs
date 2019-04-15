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
    public class CargoEmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/CargoEmpresas
        public ActionResult Index()
        {
            var tb_CargoEmpresa = db.Tb_CargoEmpresa.Include(c => c.Empresa);
            return View(tb_CargoEmpresa.ToList());
        }

        // GET: Administrador/CargoEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/CargoEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cemp_Id,Cemp_Nom,Empr_Nit,Cemp_Registro")] CargoEmpresa cargoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_CargoEmpresa.Add(cargoEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // POST: Administrador/CargoEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cemp_Id,Cemp_Nom,Empr_Nit,Cemp_Registro")] CargoEmpresa cargoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(cargoEmpresa);
        }

        // POST: Administrador/CargoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            db.Tb_CargoEmpresa.Remove(cargoEmpresa);
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
