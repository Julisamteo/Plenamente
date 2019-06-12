using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult AutoevaluacionSST(string textError = "")
        {
            List<CriteriosViewModel> list = new List<CriteriosViewModel>();
            try
            {
                ViewBag.TextError = textError;
                AutoEvaluacion autoevaluacion = db.Tb_AutoEvaluacion.FirstOrDefault(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada);
                if (autoevaluacion == null)
                {
                    db.Tb_AutoEvaluacion.Add(
                          new AutoEvaluacion
                          {
                              Empr_Nit = AccountData.NitEmpresa,
                              Auev_Inicio = DateTime.Now,
                              Auev_Nom = "Autoevaluación"
                          });
                    db.SaveChanges();
                }
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                int numeroTrabajadores = empresa.Empr_Ttrabaja;
                TipoEmpresa tipoEmpresa = new TipoEmpresa();
                if (numeroTrabajadores > 0)
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= numeroTrabajadores && t.RangoMaximoTrabajadores >= numeroTrabajadores);
                }
                list =
                   db.Tb_Criterio
                       .Select(c =>
                           new CriteriosViewModel
                           {
                               Id = c.Crit_Id,
                               Nombre = c.Crit_Nom,
                               Porcentaje = c.Crit_Porcentaje,
                               Registro = c.Crit_Registro,
                               Estandares =
                               c.Estandars
                                .Select(e =>
                                   new EstandaresViewModel
                                   {
                                       Id = e.Esta_Id,
                                       Nombre = e.Esta_Nom,
                                       Porcentaje = e.Esta_Porcentaje,
                                       Registro = e.Esta_Registro,
                                       Elementos =
                                           e.itemEstandars
                                            .Where(ie => tipoEmpresa.Categoria == 0 || ie.Categoria <= tipoEmpresa.Categoria)
                                            .Select(i =>
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
                                                   MasInformacion = i.Iest_MasInfo,
                                                   Cumplimientos = i.Cumplimientos.Where(cu => cu.Empr_Nit == AccountData.NitEmpresa && !cu.AutoEvaluacion.Finalizada).ToList()
                                               }).ToList()
                                   }).ToList(),
                           }).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }
            return View(list);
        }
        [Authorize]
        public ActionResult Cumplimiento(int idItem)
        {
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.FirstOrDefault(c => c.Empr_Nit == AccountData.NitEmpresa && c.Iest_Id == idItem && !c.AutoEvaluacion.Finalizada);
            ItemEstandar item = db.Tb_ItemEstandar.Find(idItem);

            if (cumplimiento == null)
            {
                return View(
                    new CumplimientoViewModel
                    {
                        ItemEstandarId = idItem,
                        Cumple = true,
                        Justifica = true,
                        Nit = AccountData.NitEmpresa,
                        Registro = DateTime.Now,
                        ItemEstandar =
                            new ElementoViewModel
                            {
                                Id = item.Iest_Id,
                                Descripcion = item.Iest_Desc,
                                Observaciones = item.Iest_Observa,
                                Porcentaje = item.Iest_Porcentaje,
                                Recurso = item.Iest_Recurso,
                                Registro = item.Iest_Registro,
                                Reursob = item.Iest_Rescursob,
                                Verificar = item.Iest_Verificar,
                                Video = item.Iest_Video,
                                Periodo = item.Iest_Peri,
                                MasInformacion = item.Iest_MasInfo
                            }
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
                    ItemEstandarId = cumplimiento.Iest_Id,
                    ItemEstandar =
                            new ElementoViewModel
                            {
                                Id = item.Iest_Id,
                                Descripcion = item.Iest_Desc,
                                Observaciones = item.Iest_Observa,
                                Porcentaje = item.Iest_Porcentaje,
                                Recurso = item.Iest_Recurso,
                                Registro = item.Iest_Registro,
                                Reursob = item.Iest_Rescursob,
                                Verificar = item.Iest_Verificar,
                                Video = item.Iest_Video,
                                Periodo = item.Iest_Peri,
                                MasInformacion = item.Iest_MasInfo
                            },
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
                AutoEvaluacion autoevaluacion = db.Tb_AutoEvaluacion.FirstOrDefault(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada);
                Cumplimiento cumplimiento;
                if (model.Id == 0)
                {
                    cumplimiento =
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
                            Auev_Id = autoevaluacion.Auev_Id,
                        };
                    db.Tb_Cumplimiento.Add(cumplimiento);
                }
                else
                {
                    cumplimiento = db.Tb_Cumplimiento.Find(model.Id);
                    cumplimiento.Cump_Id = model.Id;
                    cumplimiento.Cump_Cumple = model.Cumple;
                    cumplimiento.Cump_Nocumple = model.Nocumple;
                    cumplimiento.Cump_Justifica = model.Justifica;
                    cumplimiento.Cump_Nojustifica = model.Nojustifica;
                    cumplimiento.Cump_Observ = model.Observaciones;
                    cumplimiento.Cump_Registro = DateTime.Now;
                    cumplimiento.Empr_Nit = model.Nit;
                    cumplimiento.Iest_Id = model.ItemEstandarId;
                    cumplimiento.Auev_Id = autoevaluacion.Auev_Id;
                    db.Entry(cumplimiento).State = EntityState.Modified;
                }
                db.SaveChanges();
                model.Id = cumplimiento.Cump_Id;
                ViewBag.TextExitoso = "Se guardaron los datos exitosamente";
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
                ItemEstandar item = db.Tb_ItemEstandar.Find(model.ItemEstandarId);
                model.ItemEstandar =
                    new ElementoViewModel
                    {
                        Id = item.Iest_Id,
                        Descripcion = item.Iest_Desc,
                        Observaciones = item.Iest_Observa,
                        Porcentaje = item.Iest_Porcentaje,
                        Recurso = item.Iest_Recurso,
                        Registro = item.Iest_Registro,
                        Reursob = item.Iest_Rescursob,
                        Verificar = item.Iest_Verificar,
                        Video = item.Iest_Video,
                        Periodo = item.Iest_Peri,
                        MasInformacion = item.Iest_MasInfo
                    };
                return View(model);
            }

            return RedirectToAction("AutoevaluacionSST");
        }
        [Authorize]
        public ActionResult GuardarTerminar()
        {
            List<CriteriosViewModel> list = new List<CriteriosViewModel>();
            try
            {
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                int numeroTrabajadores = empresa.Empr_Ttrabaja;
                TipoEmpresa tipoEmpresa = new TipoEmpresa();
                if (numeroTrabajadores > 0)
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= numeroTrabajadores && t.RangoMaximoTrabajadores >= numeroTrabajadores);
                }                
                AutoEvaluacion autoevaluacion = db.Tb_AutoEvaluacion.FirstOrDefault(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada);
                if (autoevaluacion != null)
                {
                    int q = db.Tb_Cumplimiento.Count(c => c.Auev_Id == autoevaluacion.Auev_Id);
                    int q2 = db.Tb_ItemEstandar.Count(ie => tipoEmpresa.Categoria == 0 || ie.Categoria <= tipoEmpresa.Categoria);
                    if (q2 > q)
                    {
                        return RedirectToAction("AutoevaluacionSST", new { textError = "Esta evaluación aún no ha sido finalizada" });
                    }
                    autoevaluacion.Auev_Fin = DateTime.Now;
                    autoevaluacion.Finalizada = true;
                    db.Entry(autoevaluacion).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
                return RedirectToAction("AutoevaluacionSST");
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult CargaEvidencia(int idItem)
        {
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            ViewBag.users = new SelectList(db.Users.Where(c => c.Empr_Nit == usuario.Empr_Nit), "Id", "Pers_Nom1");

            EvidenciaCumplimientoViewModel evidenciaCumplimientoViewModel = new EvidenciaCumplimientoViewModel
            {
                IdCumplimiento = idItem

            };
            return View(evidenciaCumplimientoViewModel);
        }
        [HttpPost]
        public ActionResult CargaEvidencia([Bind(Include = "Evidencia,Archivo,NombreDocumento,TipoDocumento,Fecha,Responsable,IdCumplimiento")]EvidenciaCumplimientoViewModel model)
        {
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.FirstOrDefault(a => a.Cump_Id == model.IdCumplimiento);
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ViewBag.users = new SelectList(db.Users.Where(b => b.Empr_Nit == usuario.Empr_Nit), "Id", "Pers_Nom1");
            string nombreArchivo = model.NombreDocumento;
            List<Evidencia> evidencias = db.Tb_Evidencia.Where(f => f.Evid_Nombre == nombreArchivo).ToList();
            if (evidencias.Count == 0)
            {
                if (model.Archivo == null)
                {
                    ViewBag.falla = "Seleccion un archivo";
                    return View(model);
                }
                string extensionArchivo = model.Archivo.FileName.Split('.').Last();

                Evidencia evidencia = new Evidencia
                {
                    Evid_Nombre = nombreArchivo,
                    Cump_Id = model.IdCumplimiento,
                    Evid_Registro = model.Fecha,
                    Tdca_id = Convert.ToInt32(model.TipoDocumento),
                    Evid_Archivo = nombreArchivo + "." + extensionArchivo

                };
                evidencia.Responsable = AccountData.UsuarioId;
                db.Tb_Evidencia.Add(evidencia);
                db.SaveChanges();

                if (model.Archivo.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), nombreArchivo + "." + extensionArchivo);
                    model.Archivo.SaveAs(path);
                }
                ViewBag.exitoso = "Guardado con exito en la ruta";
            }
            else
            {
                ViewBag.falla = "Ya existe un documento con ese nombre";
                return View(model);
            }
            return View(new EvidenciaCumplimientoViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult NumeroEmpleados()
        {
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

            //Validacion. Existe alguna autoevaluacion en proceso
            if (db.Tb_AutoEvaluacion.Any(a => a.Empr_Nit == AccountData.NitEmpresa && !a.Finalizada))
            {
                return RedirectToAction("AutoevaluacionSST");
            }

            EmpresaViewModel model = new EmpresaViewModel
            {
                IdEmpresa = empresa.Empr_Nit,
                NombreEmpresa = empresa.Empr_Nom,
                NumeroEmpleados = empresa.Empr_Ttrabaja
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult NumeroEmpleados([Bind(Include = "NumeroEmpleados")]EmpresaViewModel model)
        {
            return RedirectToAction("AutoevaluacionSST");
        }

        public ActionResult ModificarNumeroEmpleados(int numeroEmpleados)
        {
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            EmpresaViewModel model = new EmpresaViewModel
            {
                IdEmpresa = empresa.Empr_Nit,
                NombreEmpresa = empresa.Empr_Nom,
                NumeroEmpleados = numeroEmpleados
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ModificarNumeroEmpleados([Bind(Include = "NumeroEmpleados")]EmpresaViewModel model)
        {
            Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
            if (!ModelState.IsValid)
            {
                model.NombreEmpresa = empresa.Empr_Nom;
                return View(model);
            }

            empresa.Empr_Ttrabaja = model.NumeroEmpleados;
            db.Entry(empresa).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("AutoevaluacionSST");
        }
        public ActionResult VerHistorico()
        {
            int? EmpNit = db.Users.Find(AccountData.UsuarioId).Empr_Nit;

            List<AutoEvaluacion> autoEvaluacions = db.Tb_AutoEvaluacion.Where(c => c.Empr_Nit == EmpNit && c.Finalizada).OrderBy(c => c.Auev_Fin).ToList();
            List<AutoEvaluacionViewModel> autoEvaluacionViewModel = new List<AutoEvaluacionViewModel>();
            int identificadorIncremental = 1;
            foreach (AutoEvaluacion a in autoEvaluacions)
            {
                AutoEvaluacionViewModel autoEvaluacionView = new AutoEvaluacionViewModel
                {
                    Id = a.Auev_Id,
                    IdentificadorIncremental = identificadorIncremental,
                    Auev_Fin = a.Auev_Fin,
                    AutoEvaluacion = a,
                    Auev_Inicio = a.Auev_Inicio,
                    NameAutoEvaluacion = a.Auev_Nom
                };
                autoEvaluacionViewModel.Add(autoEvaluacionView);
                identificadorIncremental++;
            }

            return View(autoEvaluacionViewModel);
        }


    }


}