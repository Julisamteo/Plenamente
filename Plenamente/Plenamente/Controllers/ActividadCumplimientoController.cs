using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;

namespace Plenamente.Controllers
{
    public class ActividadCumplimientoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ActividadCumplimiento
        public ActionResult Index()
        {
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var list = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa);
            //ActiCumplimiento actiEmpresas =  db.Tb_ActiCumplimiento.Find(AccountData.NitEmpresa);


            ViewBag.ReturnUrl = Request.UrlReferrer;
            return View(list.ToList());
        }

        // GET: ActividadCumplimiento/Details/5
        public ActionResult Details(int id)
        {

            var list  = db.Tb_ActiCumplimiento.Find(id);
            
            return View(list);
            
        }

        // GET: ActividadCumplimiento/Create
        public ActionResult Create()
        {
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);

            var model = new ViewModelActividadCumplimiento();
            ViewBag.ReturnUrl = Request.UrlReferrer;
            return View(model);
            
        }

        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia,idObjetivo,Frecuencia_desc,period,weekly_0,weekly_1,weekly_2,weekly_3,weekly_4,weekly_5,weekly_6,retornar")] ViewModelActividadCumplimiento model)
        {
            
            
           /* try
            {*/
                // TODO: Add insert logic here
                Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

                ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
                // TODO: Add insert logic here
                ActiCumplimiento actcumplimiento = new ActiCumplimiento
                {
                    Acum_Desc = model.NombreActividad,
                    Acum_Porcentest = model.Meta,
                    Acum_IniAct = model.FechaInicial,
                    Acum_FinAct = model.FechaFinal,
                    Oemp_Id = model.idObjetivo,
                    Acum_Registro = DateTime.Now,
                    Id=usuario.Id,
                    Frec_Id = 1,
                    Peri_Id = 6,
                    Empr_Nit=empresa.Empr_Nit
                };
                
                db.Tb_ActiCumplimiento.Add(actcumplimiento);
                db.SaveChanges();
                var link = model.retornar;
                return Redirect(link);
                
            /*}
          catch
           {
               return View();
           }*/
        }

        // GET: ActividadCumplimiento/Edit/5
        public ActionResult Edit(int id)
        {
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);

            var model = db.Tb_ActiCumplimiento.Find(id); ;
            ViewData["userid"] = model.Id;
            return View(model);
        }

        // POST: ActividadCumplimiento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Acum_Id,Acum_Desc,Acum_Porcentest,Acum_IniAct,Acum_FinAct,Oemp_Id,Id,Peri_Id,Empr_Nit,Frec_Id")] ActiCumplimiento actiCumplimiento)
        {


            if (ModelState.IsValid)
            {
               
                    db.Entry(actiCumplimiento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                

            }
            return View(actiCumplimiento);


        }

        // GET: ActividadCumplimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
