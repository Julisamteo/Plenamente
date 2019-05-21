using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using System.IO;
using System.Web.Hosting;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class CargaArchivoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administrador/CargaArchivo
        [HttpGet]
        public ActionResult Upload()
        {
            //ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }
       [HttpPost]
        public ActionResult Upload(Archivo archivo)
        {
            using (ApplicationDbContext entity = new ApplicationDbContext())
            {
                var cumplimiento = new Cumplimiento()
               {
                 //Cump_Nombre = archivo.Cump_Nombre,
                 //Cump_Aevidencia = SaveToPhysicalLocation(archivo.Cump_Aevidencia),
                 Cump_Registro =archivo.Cump_Registro,

          };
                entity.Tb_Cumplimiento.Add(cumplimiento);
                entity.SaveChanges();
           }
           return View(archivo);
        }
       

        /// <summary>  
        /// Save Posted File in Physical path and return saved path to store in a database  
        /// </summary>  
        /// <param name="file"></param>  
        /// <returns></returns>  
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }
        //public ActionResult DownloadFile(string file = "")
        //{

        //    file = HostingEnvironment.MapPath("~" + file);

        //    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    var fileName = Path.GetFileName(file);
        //    return File(file, contentType, fileName);

        //}
       
    }
}