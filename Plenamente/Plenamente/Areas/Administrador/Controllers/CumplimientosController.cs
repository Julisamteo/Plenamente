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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class CumplimientosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Cumplimientos
        public ActionResult Index(/*HttpPostedFileBase postedFileBase*/)
        {
            //byte[] bytes;
            //using (BinaryReader br=new BinaryReader(postedFileBase.InputStream))
            //{
            //    bytes = br.ReadBytes(postedFileBase.ContentLength);
            //} 


            var tb_Cumplimiento = db.Tb_Cumplimiento.Include(c => c.Empresa).Include(c => c.ItemEstandar).Include(c => c.TipoDocCarga);
            return View(tb_Cumplimiento.ToList());
        }


        // GET: Administrador/Cumplimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimiento);
        }

        // GET: Administrador/Cumplimientos/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc");
            ViewBag.Tdca_Id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            return View();
        }

        // POST: Administrador/Cumplimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cump_Id,Cump_Evidencia,Iest_Id,Empr_Id,Tdca_Id,Cump_Registro")] Cumplimiento cumplimiento)
        {
           
                if (ModelState.IsValid)
                {
                //var fileData = new MemoryStream();
                //cumplimiento.Cump_Evidencia.InputStream.CopyTo(fileData);

                //var evidencia = new Cumplimiento { Cump_Evidencia = fileData.ToArray() };
                    db.Tb_Cumplimiento.Add(cumplimiento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            ViewBag.Empr_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cumplimiento.Empr_Id);
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc", cumplimiento.Iest_Id);
            ViewBag.Tdca_Id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", cumplimiento.Tdca_Id);
            return View(cumplimiento);
        }

        // GET: Administrador/Cumplimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cumplimiento.Empr_Id);
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc", cumplimiento.Iest_Id);
            ViewBag.Tdca_Id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", cumplimiento.Tdca_Id);
            return View(cumplimiento);
        }

        // POST: Administrador/Cumplimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cump_Id,Cump_Evidencia,Iest_Id,Empr_Id,Tdca_Id,Cump_Registro")] Cumplimiento cumplimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cumplimiento.Empr_Id);
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc", cumplimiento.Iest_Id);
            ViewBag.Tdca_Id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", cumplimiento.Tdca_Id);
            return View(cumplimiento);
        }

        // GET: Administrador/Cumplimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimiento);
        }

        // POST: Administrador/Cumplimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            db.Tb_Cumplimiento.Remove(cumplimiento);
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
