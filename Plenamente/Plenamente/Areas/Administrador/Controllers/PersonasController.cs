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
    public class PersonasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Personas
        public ActionResult Index()
        {
            var applicationUsers = db.Users.Include(a => a.Afp).Include(a => a.AreaEmpresa).Include(a => a.Arl).Include(a => a.CargoEmpresa).Include(a => a.CateLicencia).Include(a => a.Ciudad).Include(a => a.Empresa).Include(a => a.Eps).Include(a => a.EstadoPersona).Include(a => a.Genero).Include(a => a.Jefe).Include(a => a.JornadaEmpresa).Include(a => a.SedeCiudad).Include(a => a.TipoDocumento).Include(a => a.TipoVinculacion);
            return View(applicationUsers.ToList());
        }

        // GET: Administrador/Personas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Administrador/Personas/Create
        public ActionResult Create()
        {
            ViewBag.Afp_Id = new SelectList(db.Tb_Afp, "Afp_Id", "Afp_Nom");
            ViewBag.Aemp_Id = new SelectList(db.Tb_AreaEmpresa, "Aemp_Id", "Aemp_Nom");
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom");
            ViewBag.Cemp_Id = new SelectList(db.Tb_CargoEmpresa, "Cemp_Id", "Cemp_Nom");
            ViewBag.Cate_Id = new SelectList(db.Tb_CateLicencia, "Cate_Id", "Cate_Nom");
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom");
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            ViewBag.Eps_Id = new SelectList(db.Tb_Eps, "Eps_Id", "Eps_Nom");
            ViewBag.Espe_Id = new SelectList(db.Tb_EstadoPersona, "Espe_Id", "Espe_Nom");
            ViewBag.Gene_Id = new SelectList(db.Tb_Genero, "Gene_Id", "Gene_Nom");
            ViewBag.Jefe_Id = new SelectList(db.Users, "Id", "Pers_Nom1");
            ViewBag.Jemp_Id = new SelectList(db.Tb_JornadaEmpresa, "Jemp_Id", "Jemp_Nom");
            ViewBag.Sciu_Id = new SelectList(db.Tb_SedeCiudad, "Sciu_Id", "Sciu_Nom");
            ViewBag.Tdoc_Id = new SelectList(db.Tb_TipoDocumento, "Tdoc_Id", "Tdoc_Nom");
            ViewBag.Tvin_Id = new SelectList(db.Tb_TipoVinculacion, "Tvin_Id", "Tvin_Nom");
            return View();
        }

        // POST: Administrador/Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pers_Doc,Pers_Nom1,Pers_Nom2,Pers_Apel1,Pers_Apel2,Pers_Licencia,Pers_LicVence,Pers_Foto,Pers_Ingreso,Pers_Retiro,Pers_Dir,Pers_Cemeg,Pers_Temeg,Pers_Registro,Tdoc_Id,Sciu_Id,Ciud_Id,Cemp_Id,Aemp_Id,Cate_Id,Gene_Id,Jemp_Id,Tvin_Id,Eps_Id,Afp_Id,Arl_Id,Empr_Nit,Espe_Id,Jefe_Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Afp_Id = new SelectList(db.Tb_Afp, "Afp_Id", "Afp_Nom", applicationUser.Afp_Id);
            ViewBag.Aemp_Id = new SelectList(db.Tb_AreaEmpresa, "Aemp_Id", "Aemp_Nom", applicationUser.Aemp_Id);
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", applicationUser.Arl_Id);
            ViewBag.Cemp_Id = new SelectList(db.Tb_CargoEmpresa, "Cemp_Id", "Cemp_Nom", applicationUser.Cemp_Id);
            ViewBag.Cate_Id = new SelectList(db.Tb_CateLicencia, "Cate_Id", "Cate_Nom", applicationUser.Cate_Id);
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", applicationUser.Ciud_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", applicationUser.Empr_Nit);
            ViewBag.Eps_Id = new SelectList(db.Tb_Eps, "Eps_Id", "Eps_Nom", applicationUser.Eps_Id);
            ViewBag.Espe_Id = new SelectList(db.Tb_EstadoPersona, "Espe_Id", "Espe_Nom", applicationUser.Espe_Id);
            ViewBag.Gene_Id = new SelectList(db.Tb_Genero, "Gene_Id", "Gene_Nom", applicationUser.Gene_Id);
            ViewBag.Jefe_Id = new SelectList(db.Users, "Id", "Pers_Nom1", applicationUser.Jefe_Id);
            ViewBag.Jemp_Id = new SelectList(db.Tb_JornadaEmpresa, "Jemp_Id", "Jemp_Nom", applicationUser.Jemp_Id);
            ViewBag.Sciu_Id = new SelectList(db.Tb_SedeCiudad, "Sciu_Id", "Sciu_Nom", applicationUser.Sciu_Id);
            ViewBag.Tdoc_Id = new SelectList(db.Tb_TipoDocumento, "Tdoc_Id", "Tdoc_Nom", applicationUser.Tdoc_Id);
            ViewBag.Tvin_Id = new SelectList(db.Tb_TipoVinculacion, "Tvin_Id", "Tvin_Nom", applicationUser.Tvin_Id);
            return View(applicationUser);
        }

        // GET: Administrador/Personas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Afp_Id = new SelectList(db.Tb_Afp, "Afp_Id", "Afp_Nom", applicationUser.Afp_Id);
            ViewBag.Aemp_Id = new SelectList(db.Tb_AreaEmpresa, "Aemp_Id", "Aemp_Nom", applicationUser.Aemp_Id);
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", applicationUser.Arl_Id);
            ViewBag.Cemp_Id = new SelectList(db.Tb_CargoEmpresa, "Cemp_Id", "Cemp_Nom", applicationUser.Cemp_Id);
            ViewBag.Cate_Id = new SelectList(db.Tb_CateLicencia, "Cate_Id", "Cate_Nom", applicationUser.Cate_Id);
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", applicationUser.Ciud_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", applicationUser.Empr_Nit);
            ViewBag.Eps_Id = new SelectList(db.Tb_Eps, "Eps_Id", "Eps_Nom", applicationUser.Eps_Id);
            ViewBag.Espe_Id = new SelectList(db.Tb_EstadoPersona, "Espe_Id", "Espe_Nom", applicationUser.Espe_Id);
            ViewBag.Gene_Id = new SelectList(db.Tb_Genero, "Gene_Id", "Gene_Nom", applicationUser.Gene_Id);
            ViewBag.Jefe_Id = new SelectList(db.Users, "Id", "Pers_Nom1", applicationUser.Jefe_Id);
            ViewBag.Jemp_Id = new SelectList(db.Tb_JornadaEmpresa, "Jemp_Id", "Jemp_Nom", applicationUser.Jemp_Id);
            ViewBag.Sciu_Id = new SelectList(db.Tb_SedeCiudad, "Sciu_Id", "Sciu_Nom", applicationUser.Sciu_Id);
            ViewBag.Tdoc_Id = new SelectList(db.Tb_TipoDocumento, "Tdoc_Id", "Tdoc_Nom", applicationUser.Tdoc_Id);
            ViewBag.Tvin_Id = new SelectList(db.Tb_TipoVinculacion, "Tvin_Id", "Tvin_Nom", applicationUser.Tvin_Id);
            return View(applicationUser);
        }

        // POST: Administrador/Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pers_Doc,Pers_Nom1,Pers_Nom2,Pers_Apel1,Pers_Apel2,Pers_Licencia,Pers_LicVence,Pers_Foto,Pers_Ingreso,Pers_Retiro,Pers_Dir,Pers_Cemeg,Pers_Temeg,Pers_Registro,Tdoc_Id,Sciu_Id,Ciud_Id,Cemp_Id,Aemp_Id,Cate_Id,Gene_Id,Jemp_Id,Tvin_Id,Eps_Id,Afp_Id,Arl_Id,Empr_Nit,Espe_Id,Jefe_Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Afp_Id = new SelectList(db.Tb_Afp, "Afp_Id", "Afp_Nom", applicationUser.Afp_Id);
            ViewBag.Aemp_Id = new SelectList(db.Tb_AreaEmpresa, "Aemp_Id", "Aemp_Nom", applicationUser.Aemp_Id);
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", applicationUser.Arl_Id);
            ViewBag.Cemp_Id = new SelectList(db.Tb_CargoEmpresa, "Cemp_Id", "Cemp_Nom", applicationUser.Cemp_Id);
            ViewBag.Cate_Id = new SelectList(db.Tb_CateLicencia, "Cate_Id", "Cate_Nom", applicationUser.Cate_Id);
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", applicationUser.Ciud_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", applicationUser.Empr_Nit);
            ViewBag.Eps_Id = new SelectList(db.Tb_Eps, "Eps_Id", "Eps_Nom", applicationUser.Eps_Id);
            ViewBag.Espe_Id = new SelectList(db.Tb_EstadoPersona, "Espe_Id", "Espe_Nom", applicationUser.Espe_Id);
            ViewBag.Gene_Id = new SelectList(db.Tb_Genero, "Gene_Id", "Gene_Nom", applicationUser.Gene_Id);
            ViewBag.Jefe_Id = new SelectList(db.Users, "Id", "Pers_Nom1", applicationUser.Jefe_Id);
            ViewBag.Jemp_Id = new SelectList(db.Tb_JornadaEmpresa, "Jemp_Id", "Jemp_Nom", applicationUser.Jemp_Id);
            ViewBag.Sciu_Id = new SelectList(db.Tb_SedeCiudad, "Sciu_Id", "Sciu_Nom", applicationUser.Sciu_Id);
            ViewBag.Tdoc_Id = new SelectList(db.Tb_TipoDocumento, "Tdoc_Id", "Tdoc_Nom", applicationUser.Tdoc_Id);
            ViewBag.Tvin_Id = new SelectList(db.Tb_TipoVinculacion, "Tvin_Id", "Tvin_Nom", applicationUser.Tvin_Id);
            return View(applicationUser);
        }

        // GET: Administrador/Personas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Administrador/Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
