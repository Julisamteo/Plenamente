using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using System.IO;

//namespace Plenamente.Areas.Administrador.Controllers
//{
//    public class AutoEvaluacionController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
//        // GET: Administrador/AutoEvaluacion
//        public ActionResult Index()
//        {
//            var mymodel = new AutoEvaluacion();
//            mymodel.Criterios = db.Tb_Criterio.Where(x => x.Crit_Id == 3).ToList();
//            mymodel.Estandars = db.Tb_Estandar.Where(x => x.Crit_Id == 3 && x.Esta_Id == 2).ToList();
//            mymodel.ItemEstandars = db.Tb_ItemEstandar.Where(x => x.Esta_Id == 2).ToList();

//            mymodel.Estandars1 = db.Tb_Estandar.Where(x => x.Crit_Id == 3 && x.Esta_Id == 3).ToList();
//            mymodel.itemEstandars1 = db.Tb_ItemEstandar.Where(x => x.Esta_Id == 3).ToList();
//           return View(mymodel);
//        }
        
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
//    }
//}