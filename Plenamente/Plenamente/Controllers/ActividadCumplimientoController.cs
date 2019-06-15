using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class ActividadCumplimientoController : Controller
    {
        // GET: ActividadCumplimiento
        public ActionResult Index()
        {
            return View();
        }

        // GET: ActividadCumplimiento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActividadCumplimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActividadCumplimiento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
