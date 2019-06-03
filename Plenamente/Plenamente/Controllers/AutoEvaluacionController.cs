using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class AutoevaluacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult AutoevaluacionSST()
        {
            System.Data.Entity.DbSet<Criterio> criterios = db.Tb_Criterio;
            List<CriteriosViewModel> list =
               db.Tb_Criterio
                   .Select(c =>
                       new CriteriosViewModel
                       {
                           Id = c.Crit_Id,
                           Nombre = c.Crit_Nom,
                           Porcentaje = c.Crit_Porcentaje,
                           Registro = c.Crit_Registro,
                           Estandares =
                           c.Estandars.Select(e =>
                               new EstandaresViewModel
                               {
                                   Id = e.Esta_Id,
                                   Nombre = e.Esta_Nom,
                                   Porcentaje = e.Esta_Porcentaje,
                                   Registro = e.Esta_Registro,
                                   Elementos =
                                       e.itemEstandars.Select(i =>
                                           new ElementoViewModel
                                           {
                                               Id = i.Iest_Id,
                                               Descripcion = i.Iest_Desc,
                                               Observaciones = i.Iest_Observa,
                                               Porcentaje = i.Iest_Porcentaje,
                                               Recurso = i.Iest_Recurso,
                                               Registro = i.Iest_Registro,
                                               Reursob = i.Iest_Rescursob,
                                               Verificar = i.Iest_Verificar,
                                               Video = i.Iest_Video,
                                               Periodo = i.Iest_Peri
                                           }).ToList()
                               }).ToList(),
                       }).ToList();

            return View(list);
        }
        [Authorize]
        public ActionResult Cumplimiento(int id, int item)
        {
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return View(
                    new CumplimientoViewModel
                    {
                        ItemEstandarId = item,
                        Cumple = true,
                        Nit = AccountData.NitEmpresa,
                        Registro = DateTime.Now
                    });
            }
            return View(
                new CumplimientoViewModel
                {
                    AcumMes = cumplimiento.AcumMes?.ToList(),
                    AutoEvaluacionId = cumplimiento.Auev_Id,
                    Cumple = cumplimiento.Cump_Cumple,
                    Evidencias = cumplimiento.Evidencias?.ToList(),
                    Id = cumplimiento.Cump_Id,
                    ItemEstandarId = item,
                    Justifica = cumplimiento.Cump_Justifica,
                    Nit = AccountData.NitEmpresa,
                    Nocumple = cumplimiento.Cump_Nocumple,
                    Nojustifica = cumplimiento.Cump_Nojustifica,
                    Observaciones = cumplimiento.Cump_Observ,
                    Registro = cumplimiento.Cump_Registro
                });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Cumplimiento([Bind(Include = "AutoEvaluacionId,Cumple,Nocumple,Justifica,Nojustifica,Id,Registro,Observaciones,ItemEstandarId,Nit")] CumplimientoViewModel model)
        {
            try
            {
                db.Tb_Cumplimiento.Add(
                    new Cumplimiento
                    {
                        Cump_Id = model.Id,
                        Cump_Cumple = model.Cumple,
                        Cump_Nocumple = model.Nocumple,
                        Cump_Justifica = model.Justifica,
                        Cump_Nojustifica = model.Nojustifica,
                        Cump_Observ = model.Observaciones,
                        Cump_Registro = DateTime.Now,
                        Empr_Nit = model.Nit,
                        Iest_Id = model.ItemEstandarId,
                        Auev_Id = model.AutoEvaluacionId
                    });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }
            return View(model);
        }
    }
}