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
    public class RespuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Respuestas
        public ActionResult Index(int idPregunta)
        {
            ViewBag.idPregunta = idPregunta;
            var tb_Respuesta = db.Tb_Respuesta.Include(r => r.Pregunta);
            return View(tb_Respuesta.ToList());
        }

        // GET: Administrador/Respuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            return View(respuesta);
        }

        // GET: Administrador/Respuestas/Create
        public ActionResult Create(int? id, int idPregunta)
        {
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo");
            ViewBag.idPregunta = idPregunta;
            return View();
        }

        // POST: Administrador/Respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Resp_Id,Resp_Nom,Resp_Registro,Preg_Id")] Respuesta respuesta, int? id, int idPregunta)
        {
            if (ModelState.IsValid)
            {
                ViewBag.idPregunta = idPregunta;
                db.Tb_Respuesta.Add(respuesta);
                db.SaveChanges();
                return RedirectToAction("Index", "Respuestas", routeValues: new { ViewBag.idPregunta });

            }

            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo", respuesta.Preg_Id);
            return View(respuesta);
        }

        // GET: Administrador/Respuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo", respuesta.Preg_Id);
            return View(respuesta);
        }

        // POST: Administrador/Respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Resp_Id,Resp_Nom,Resp_Registro,Preg_Id")] Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo", respuesta.Preg_Id);
            return View(respuesta);
        }

        // GET: Administrador/Respuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            return View(respuesta);
        }

        // POST: Administrador/Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            db.Tb_Respuesta.Remove(respuesta);
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
