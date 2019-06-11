using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using Plenamente.Models;

namespace Plenamente.Controllers
{
    public class EmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empresas
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var empresas = from s in db.Tb_Empresa
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                 empresas = empresas.Where(s => s.Empr_Nom.Contains(searchString)
                                       || s.Empr_Nom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    empresas = empresas.OrderByDescending(s => s.Empr_Nom);
                    break;
                default:  // Name ascending 
                    empresas = empresas.OrderBy(s => s.Empr_Nom);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(empresas.ToPagedList(pageNumber, pageSize));
        }
        // GET: Empresas/Details/5
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tb_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }
        // GET: Empresas/Create
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Create()
        {
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom");
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom");
            return View();
        }
        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Create([Bind(Include = "Empr_Nit,Empr_Nom,Empr_Dir,Arl_Id,Carl_Id,Empr_Afiarl,Empr_Ttrabaja,Empr_Itrabaja,Empr_Registro")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Empresa.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", empresa.Arl_Id);
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom", empresa.Carl_Id);
            return View(empresa);
        }
        // GET: Empresas/Edit/5
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tb_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", empresa.Arl_Id);
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom", empresa.Carl_Id);
            return View(empresa);
        }
        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Edit([Bind(Include = "Empr_Nit,Empr_Nom,Empr_Dir,Arl_Id,Carl_Id,Empr_Afiarl,Empr_Ttrabaja,Empr_Itrabaja,Empr_Registro")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", empresa.Arl_Id);
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom", empresa.Carl_Id);
            return View(empresa);
        }
        // GET: Empresas/Delete/5
        [Authorize(Roles = "SuperAdmin2")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tb_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }
        // POST: Empresas/Delete/5
        [Authorize(Roles = "SuperAdmin2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Tb_Empresa.Find(id);
            db.Tb_Empresa.Remove(empresa);
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
