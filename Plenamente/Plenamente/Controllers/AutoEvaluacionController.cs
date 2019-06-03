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
        //Olarte , aca deberia ingeresar el item en id para mantener la referencia de que item se esta relacionando el doc o el cumplimiento , no se aun
        public ActionResult CargaEvidencia()
        {
            var usuario = db.Users.Find(AccountData.UsuarioId);
            var tipoDocumento = db.Tb_TipoDocumento.Find(usuario.Tdoc_Id).Tdoc_Nom;
            //temporales manual,fecha,registro
            EvidenciaCumplimientoViewModel evidenciaCumplimientoViewModel = new EvidenciaCumplimientoViewModel
            {
                IdDocumento = usuario.Pers_Doc,
                TipoDocumento = tipoDocumento,
                Manual = 1,
                Fecha = 1,
                Registro = 1,
                Responsable = usuario.Pers_Nom1 + " " + usuario.Pers_Apel1,
                IdCumplimiento = 1

            };
            return View(evidenciaCumplimientoViewModel);
        }
        //faltan varias cosas y dudas sobre el tipo de documento que se sube pues en la vista no esta ese atributo y otros items que no se entiende LUEGO BORRAMOS LOS COMENTARIOS , NO LOS BORRE PUTO
        [HttpPost]
        public ActionResult CargaEvidencia([Bind(Include = "Evidencia,Archivo,IdDocumento,TipoDocumento,Manual,Fecha,Registro,Responsable,IdCumplimiento")]EvidenciaCumplimientoViewModel model)
        {            
           
            Evidencia evidencia = new Evidencia
            {
                Evid_Nombre = model.Archivo.FileName,
                Cump_Id = model.IdCumplimiento,
                Evid_Registro = DateTime.Now,
                Tdca_id = 2,
                Evid_Archivo = model.Archivo.FileName + "prueba"

            };
            evidencia.Id = AccountData.UsuarioId;
            db.Tb_Evidencia.Add(evidencia);
            db.SaveChanges();
            return View(model);
        }

    }
}