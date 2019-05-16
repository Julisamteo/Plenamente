using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class FileController : Controller
    {
        // GET: Administrador/File
        public ActionResult Index()
        {
            return View();
        }

        // GET: Administrador/File/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administrador/File/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/File/Create
        [HttpPost]
        public ActionResult Create (FormCollection collection)
        {
            try
            {
                string directory = @"C:\Users\Carlos\source\repos\ErickP24\Plenamente\Plenamente\Plenamente\Content";

                HttpPostedFileBase photo = Request.Files["photo"];

                if (photo != null && photo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    photo.SaveAs(Path.Combine(directory, fileName));
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrador/File/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administrador/File/Edit/5
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

        // GET: Administrador/File/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administrador/File/Delete/5
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
