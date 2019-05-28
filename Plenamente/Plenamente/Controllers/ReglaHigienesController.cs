using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using Plenamente.Models.ViewModel;

namespace Plenamente.Controllers
{
    public class ReglaHigienesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReglaHigienes
        public ActionResult Index()
        {
            var tb_ReglaHigiene = db.Tb_ReglaHigiene.Include(r => r.Empresa);
            return View(tb_ReglaHigiene.ToList());
        }

        // GET: ReglaHigienes/Details/5
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

        // GET: ReglaHigienes/Create
        public ActionResult Create()
        {
            ApplicationDbContext entity = new ApplicationDbContext();

            List<Empresa> listE = entity.Tb_Empresa.ToList();
            ViewBag.EmpreList = new SelectList(listE, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: ReglaHigienes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rhig_Id,Rhig_Archivo,Rhig_Nom,Empr_Nit,Rhig_Registro")] ReglaHigiene reglaHigiene)
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

        // GET: ReglaHigienes/Edit/5
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

        // POST: ReglaHigienes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rhig_Id,Rhig_Archivo,Rhig_Nom,Empr_Nit,Rhig_Registro")] ReglaHigiene reglaHigiene)
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

        // GET: ReglaHigienes/Delete/5
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

        // POST: ReglaHigienes/Delete/5
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

        [HttpPost]
        public ActionResult SaveRecord(Rhigiene rhigiene)
        {
            try
            {
                ApplicationDbContext entity = new ApplicationDbContext();
                {
                    ReglaHigiene rh = new ReglaHigiene();
                    rh.Rhig_Nom = rhigiene.Rhig_Nom;
                    rh.Rhig_Archivo = SaveToPhysicalLocation(rhigiene.Rhig_Archivo);
                    rh.Rhig_Registro = rhigiene.Rhig_Registro;
                    rh.Empr_Nit = rhigiene.Empr_Nit;

                    entity.Tb_ReglaHigiene.Add(rh);
                    entity.SaveChanges();

                    int latest = rh.Rhig_Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Create");
        }
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files"),fileName);
                
                file.SaveAs(path);
                return fileName;
            }
            return string.Empty;
        }
    }
}
