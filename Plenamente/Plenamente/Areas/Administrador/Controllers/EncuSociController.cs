using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class EncuSociController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administrador/EncuSoci
        public ActionResult Index()
        {
           
            var mymodel = new SocioDemografica();
            mymodel.Criterios = db.Tb_Criterio.Where(x=> x.Crit_Id==3).ToList();
            mymodel.Estandars = db.Tb_Estandar.Where(x=> x.Crit_Id==3 && x.Esta_Id==2).ToList();
            mymodel.ItemEstandars = db.Tb_ItemEstandar.Where(x=> x.Esta_Id==2).ToList();
            mymodel.Cumplimientos = db.Tb_Cumplimiento.Where(x => x.Cump_Id == 3).ToList();

            mymodel.Estandars1 = db.Tb_Estandar.Where(x => x.Crit_Id == 3 && x.Esta_Id == 3).ToList();
            mymodel.itemEstandars1 = db.Tb_ItemEstandar.Where(x => x.Esta_Id == 3).ToList();
            return View(mymodel);
        }
   


    }
    //var hola = db.Tb_Criterio.Where(x => x.Crit_Id == '3').ToList();
}