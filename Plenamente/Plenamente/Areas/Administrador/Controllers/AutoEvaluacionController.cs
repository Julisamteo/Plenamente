using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using System.IO;

namespace Plenamente.Areas.Administrador.Controllers
{
   public class AutoEvaluacionController : Controller
   {
        // Se declara una variable privada para poder acceder al contexto de la DB.
        private ApplicationDbContext db = new ApplicationDbContext();
         //GET: Administrador/AutoEvaluacion
        public ActionResult Index()
       {

           // Creo una variable 'mymodel' para asociarla con la clase 'AutoEvaluacion' donde estan multiplez i Collections nombrados
           var mymodel = new AutoEvaluacion();
           // Se hace el query por variable  para trearla con el ID correspondiente y listarlo.
           mymodel.Criterios1=  db.Tb_Criterio.Where(x => x.Crit_Id == 3).ToList();
           mymodel.Estandars11= db.Tb_Estandar.Where(x => x.Crit_Id == 3 && x.Esta_Id == 2).ToList();
           mymodel.ItemEstandars111= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 1).ToList();
           mymodel.ItemEstandars112= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 5).ToList();
           mymodel.ItemEstandars113= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 6).ToList();
           mymodel.ItemEstandars114= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 1002).ToList();
           mymodel.ItemEstandars115= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 1003).ToList();
           mymodel.ItemEstandars116= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 1004).ToList();
           mymodel.ItemEstandars117= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 1005).ToList();
           mymodel.ItemEstandars118= db.Tb_ItemEstandar.Where(x => x.Iest_Id == 1006).ToList();

            mymodel.Criterios2 = db.Tb_Criterio.Where(x => x.Crit_Id == 4).ToList();

           mymodel.Criterios3 = db.Tb_Criterio.Where(x => x.Crit_Id == 5).ToList();
           mymodel.Criterios4 = db.Tb_Criterio.Where(x => x.Crit_Id == 6).ToList();
           mymodel.Criterios5 = db.Tb_Criterio.Where(x => x.Crit_Id == 7).ToList();
           mymodel.criterios6 = db.Tb_Criterio.Where(x => x.Crit_Id == 8).ToList();
           mymodel.Criterios7 = db.Tb_Criterio.Where(x => x.Crit_Id == 8).ToList();
          
           return View(mymodel);
        }
         // Intento De crear subir el  con errores. 
        
//        // Create File
//        public ActionResult Create()
//        {
//            return View();
//        }

//            [HttpPost]
//            [ValidateAntiForgeryToken]
//            public ActionResult Create(AutoEvaluacion autoEvaluacion)
//            {
//                if (ModelState.IsValid)
//                {
//                List<Cumplimiento> cumplimientos = new List<Cumplimiento>();
//                    for (int i = 0; i<Request.Files.Count; i++)
//                    {
//                    var file = Request.Files[i];
//                        if (file != null && file.ContentLength>0)
//                        {
//                        string cump_Nombre = Path.GetFileName(file.FileName);
//                            Cumplimiento cumplimiento = new Cumplimiento()
//                            {
//                                Cump_Nombre = cump_Nombre,
//                                Cump_Aevidencia=Path.GetExtension(cump_Nombre),
//                                Cump_Guid = Guid.NewGuid()
//                            };
//                        cumplimientos.Add(cumplimiento);

//                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), cumplimiento.Cump_Guid + cumplimiento.Cump_Aevidencia);
//                        file.SaveAs(path);
//                        }
//                    }
//                autoEvaluacion.Cumplimientos = cumplimientos;
//                db.Tb_Cumplimiento.Add(autoEvaluacion);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//                }

//            return View(autoEvaluacion);
//            }
    }
   }