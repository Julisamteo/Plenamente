using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                                               Periodo = i.Iest_Peri,
                                               Cumplimientos = i.Cumplimientos.Where(cu => cu.Empr_Nit == AccountData.NitEmpresa).ToList()
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
                if (model.Id == 0)
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
                else
                {
                    model.Registro = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }
            return View(model);
        }
        //Olarte , aca deberia ingeresar el item en id para mantener la referencia de que item se esta relacionando el doc o el cumplimiento , no se aun
        public ActionResult CargaEvidencia(int idItem=1)
        {            
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            var usuario = db.Users.Find(AccountData.UsuarioId);
            var tipoDocumento = db.Tb_TipoDocumento.Find(usuario.Tdoc_Id).Tdoc_Nom;
            //falta listado de usuarios que permanezcan a la misma empresa 
            //temporales manual,fecha,registro
            EvidenciaCumplimientoViewModel evidenciaCumplimientoViewModel = new EvidenciaCumplimientoViewModel
            {                
                IdCumplimiento = idItem

            };
            return View(evidenciaCumplimientoViewModel);
        }
        //faltan varias cosas y dudas sobre el tipo de documento que se sube pues en la vista no esta ese atributo y otros items que no se entiende LUEGO BORRAMOS LOS COMENTARIOS , NO LOS BORRE PUTO
        [HttpPost]
        public ActionResult CargaEvidencia([Bind(Include = "Evidencia,Archivo,NombreDocumento,TipoDocumento,Fecha,Responsable,IdCumplimiento")]EvidenciaCumplimientoViewModel model)
        {
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            Evidencia evidencia = new Evidencia
            {
                Evid_Nombre = model.NombreDocumento,
                Cump_Id = model.IdCumplimiento,
                Evid_Registro = model.Fecha,
                Tdca_id = Convert.ToInt32(model.TipoDocumento),                
                Evid_Archivo = model.Archivo.FileName + "prueba"

            };
            evidencia.Id = AccountData.UsuarioId;
            db.Tb_Evidencia.Add(evidencia);
            db.SaveChanges();
            return View(model);
        }

    }
}