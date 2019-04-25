using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class PreguntasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int idEncuesta, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.idEncuesta1 = idEncuesta;


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var preguntas = from s in db.Tb_Pregunta
                            where s.Encu_Id.Equals(idEncuesta)
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                preguntas = preguntas.Where(s => s.Preg_Titulo.Contains(searchString)
                                       || s.Preg_Titulo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    preguntas = preguntas.OrderByDescending(s => s.Preg_Titulo);
                    break;
                default:  // Name ascending 
                    preguntas = preguntas.OrderBy(s => s.Preg_Titulo);
                    break;
            }
           
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(preguntas.ToPagedList(pageNumber, pageSize));

           
        }




        // GET: Administrador/Preguntas/Details/5
        public ActionResult Details(int? id, int? idEncuesta)
        {
            ViewBag.Enci_Id1 = idEncuesta;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }

            return View(pregunta);
        }

        // GET: Administrador/Preguntas/Create
        public ActionResult Create(int ?id, int ?idEncuesta1)
        {
            ViewBag.Enci_Id1 = idEncuesta1;
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id");
            return View();
        }

        // POST: Administrador/Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Preg_Id,Preg_Titulo,Preg_Registro,Encu_Id")] Pregunta pregunta, int ?id, int ?idEncuesta)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Pregunta.Add(pregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id", pregunta.Encu_Id);
            ViewBag.pregId = idEncuesta;
            return View(pregunta);
        }

        // GET: Administrador/Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id", pregunta.Encu_Id);
            return View(pregunta);
        }

        // POST: Administrador/Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Preg_Id,Preg_Titulo,Preg_Registro,Encu_Id")] Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id", pregunta.Encu_Id);
            return View(pregunta);
        }

        // GET: Administrador/Preguntas/Delete/5
        public ActionResult Delete(int? id, int idEncuesta)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // POST: Administrador/Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ?id, int idEncuesta)
        {
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            db.Tb_Pregunta.Remove(pregunta);
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
