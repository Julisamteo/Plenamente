using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Controllers
{
    public class ExpandedUserDTOesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExpandedUserDTOes
        public ActionResult Index()
        {
            var expandedUserDTOes = db.ExpandedUserDTOes.Include(e => e.Empresa);
            return View(expandedUserDTOes.ToList());
        }

        // GET: ExpandedUserDTOes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO expandedUserDTO = db.ExpandedUserDTOes.Find(id);
            if (expandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(expandedUserDTO);
        }

        // GET: ExpandedUserDTOes/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Empresas, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: ExpandedUserDTOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Email,Password,Documento,Nombres,Apellidos,Pers_Licencia,Pers_LicVence,Pers_Direccion,Pers_ContactoEmeg,Pers_TelefonoEmeg,LockoutEndDateUtc,AccessFailedCount,PhoneNumber,Tdoc_Id,Sciu_Id,Ciud_Id,Cemp_Id,Aemp_Id,Cate_Id,Gene_Id,Jemp_Id,Tvin_Id,Eps_Id,Afp_Id,Arl_Id,Empr_Nit,Espe_Id,Jefe_Id")] ExpandedUserDTO expandedUserDTO)
        {
            if (ModelState.IsValid)
            {
                db.ExpandedUserDTOes.Add(expandedUserDTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Empresas, "Empr_Nit", "Empr_Nom", expandedUserDTO.Empr_Nit);
            return View(expandedUserDTO);
        }

        // GET: ExpandedUserDTOes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO expandedUserDTO = db.ExpandedUserDTOes.Find(id);
            if (expandedUserDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Empresas, "Empr_Nit", "Empr_Nom", expandedUserDTO.Empr_Nit);
            return View(expandedUserDTO);
        }

        // POST: ExpandedUserDTOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Email,Password,Documento,Nombres,Apellidos,Pers_Licencia,Pers_LicVence,Pers_Direccion,Pers_ContactoEmeg,Pers_TelefonoEmeg,LockoutEndDateUtc,AccessFailedCount,PhoneNumber,Tdoc_Id,Sciu_Id,Ciud_Id,Cemp_Id,Aemp_Id,Cate_Id,Gene_Id,Jemp_Id,Tvin_Id,Eps_Id,Afp_Id,Arl_Id,Empr_Nit,Espe_Id,Jefe_Id")] ExpandedUserDTO expandedUserDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expandedUserDTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Empresas, "Empr_Nit", "Empr_Nom", expandedUserDTO.Empr_Nit);
            return View(expandedUserDTO);
        }

        // GET: ExpandedUserDTOes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO expandedUserDTO = db.ExpandedUserDTOes.Find(id);
            if (expandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(expandedUserDTO);
        }

        // POST: ExpandedUserDTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ExpandedUserDTO expandedUserDTO = db.ExpandedUserDTOes.Find(id);
            db.ExpandedUserDTOes.Remove(expandedUserDTO);
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
