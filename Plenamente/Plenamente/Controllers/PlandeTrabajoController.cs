using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;

namespace Plenamente.Controllers
{
    public class PlandeTrabajoController : Controller
    {
        private readonly int _RegistrosPorPagina = 10;
        private PaginadorGenerico<PlandeTrabajo> _PaginadorCustomers;
		private readonly int _RegistrosPorPaginaActividades = 5;
		private PaginadorGenerico<ActividadesAsignadasPlanDeTrabajoViewModel> _PaginadorCustomersActividades;
		private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlandeTrabajo
        public ActionResult Index(int pagina = 1)
        {
            int _TotalRegistros = 0;
            var tb_PlandeTrabajo = db.Tb_PlandeTrabajo.Where(p => p.Emp_Id == AccountData.NitEmpresa).ToList();
            _TotalRegistros = tb_PlandeTrabajo.Count();
            tb_PlandeTrabajo = tb_PlandeTrabajo.Skip((pagina - 1) * _RegistrosPorPagina)
                                               .Take(_RegistrosPorPagina)
                                               .ToList();
            int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _PaginadorCustomers = new PaginadorGenerico<PlandeTrabajo>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = tb_PlandeTrabajo
            };
            return View(_PaginadorCustomers);
        }

        // GET: PlandeTrabajo/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var plantrabajo = db.Tb_PlandeTrabajo.Find(id);			
			var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
			List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
			List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
			foreach (var item in actividadesEmpresa)
			{
				var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
				if (useractividadpt.Count <= 0)
				{
					actiCumplimientoSinAsignar.Add(item);
				}
				else
				{
					var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == id && c.Acum_Id == item.Acum_Id).ToList();
					if (cumplimientoPlanDetrabajo.Count > 0)
					{

						var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
						var nombre = db.Users.Find(user.Id);

						ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
						{
							IdUserPlanDeTrabajoActividad = user.Uspl_Id,
							NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
							IdPlantTrabajo = plantrabajo.Plat_Id,
							IdActiCumplimiento = item.Acum_Id,
							DescripcionCumplimiento = item.Acum_Desc,
							NombrePlanTrabajo = plantrabajo.Plat_Nom

						};
						actiCumplimientoAsignados.Add(temp);
					}

				}
			}			
			PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
			{
				NombrePlanTrabajo = plantrabajo.Plat_Nom,
				IdPlantTrabajo = plantrabajo.Plat_Id
			};
			ViewBag.actividadesAsignadas = actiCumplimientoAsignados;
			return View(plandetrabajoActividades);			
        }

        // GET: PlandeTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.Emp_Id = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: PlandeTrabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Plat_Id,Plat_Nom,Emp_Id")] PlandeTrabajo plandeTrabajo)
        {
            if (ModelState.IsValid)
            {
                plandeTrabajo.FechaCreacion = DateTime.Now;
                plandeTrabajo.FechaActualizacion = DateTime.Now;
                var nombreplan = db.Tb_PlandeTrabajo.Where(c => c.Emp_Id == plandeTrabajo.Emp_Id && c.Plat_Nom == plandeTrabajo.Plat_Nom).ToList();
                if (nombreplan.Count > 0)
                {
                    ViewBag.TextError = "Nombre del plan de trabajo repetido";
                    return View(plandeTrabajo);
                }
                db.Tb_PlandeTrabajo.Add(plandeTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plandeTrabajo);
        }

        // GET: PlandeTrabajo/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
            if (plandeTrabajo == null)
            {
                return HttpNotFound();
            }            
            return View(plandeTrabajo);
        }

        // POST: PlandeTrabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Plat_Id,Plat_Nom,Emp_Id,FechaCreacion")] PlandeTrabajo plandeTrabajo)
        {
            if (ModelState.IsValid)
            {
                plandeTrabajo.FechaActualizacion = DateTime.Now;
                var Planesdetrabajo=db.Tb_PlandeTrabajo.Where(c=> c.Plat_Nom==plandeTrabajo.Plat_Nom && c.Emp_Id==AccountData.NitEmpresa).ToList();
				if (Planesdetrabajo.Count<=0)
				{
					db.Entry(plandeTrabajo).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					return View(plandeTrabajo);
				}
                
                
            }            
            return View(plandeTrabajo);
        }

        // GET: PlandeTrabajo/Delete/5
        public ActionResult Eliminar(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var plantrabajo = db.Tb_PlandeTrabajo.Find(id);
			var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
			List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
			List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
			foreach (var item in actividadesEmpresa)
			{
				var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
				if (useractividadpt.Count <= 0)
				{
					actiCumplimientoSinAsignar.Add(item);
				}
				else
				{
					var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == id && c.Acum_Id == item.Acum_Id).ToList();
					if (cumplimientoPlanDetrabajo.Count > 0)
					{

						var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
						var nombre = db.Users.Find(user.Id);

						ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
						{
							IdUserPlanDeTrabajoActividad = user.Uspl_Id,
							NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
							IdPlantTrabajo = plantrabajo.Plat_Id,
							IdActiCumplimiento = item.Acum_Id,
							DescripcionCumplimiento = item.Acum_Desc,
							NombrePlanTrabajo = plantrabajo.Plat_Nom

						};
						actiCumplimientoAsignados.Add(temp);
					}

				}
			}
			PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
			{
				NombrePlanTrabajo = plantrabajo.Plat_Nom,
				IdPlantTrabajo = plantrabajo.Plat_Id
			};
			ViewBag.actividadesAsignadas = actiCumplimientoAsignados;
			return View(plandetrabajoActividades);
		}

        // POST: PlandeTrabajo/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ElimitarConfirmado(int id)
        {
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(id);
			var usuariosPlandetrabajo = db.Tb_UsersPlandeTrabajo.Where(c=> c.Plat_Id==id && c.Emp_Id==plandeTrabajo.Emp_Id).ToList();
			foreach(var item in usuariosPlandetrabajo)
			{
				db.Tb_UsersPlandeTrabajo.Remove(item);
			}
            db.Tb_PlandeTrabajo.Remove(plandeTrabajo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ActividadesPlanTrabajo(int IdPlantTrabajo,int pagina =1)
        {
            var plantrabajo = db.Tb_PlandeTrabajo.Find(IdPlantTrabajo);
            ViewBag.users = new SelectList(db.Users.Where(c => c.Empr_Nit == AccountData.NitEmpresa), "Id", "Pers_Nom1");
            var actividadesEmpresa = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
            List<ActiCumplimiento> actiCumplimientoSinAsignar = new List<ActiCumplimiento>();
            List<ActividadesAsignadasPlanDeTrabajoViewModel> actiCumplimientoAsignados = new List<ActividadesAsignadasPlanDeTrabajoViewModel>();
            foreach (var item in actividadesEmpresa)
            {
                var useractividadpt = db.Tb_UsersPlandeTrabajo.Where(c => c.Acum_Id == item.Acum_Id).ToList();
                if (useractividadpt.Count <= 0)
                {
                    actiCumplimientoSinAsignar.Add(item);
                }
                else
                {
                    var cumplimientoPlanDetrabajo = db.Tb_UsersPlandeTrabajo.Where(c => c.Plat_Id == IdPlantTrabajo && c.Acum_Id==item.Acum_Id).ToList();
                    if (cumplimientoPlanDetrabajo.Count>0)
                    {
                        
                        var user = db.Tb_UsersPlandeTrabajo.First(c => c.Acum_Id == item.Acum_Id);
                        var nombre = db.Users.Find(user.Id);

                        ActividadesAsignadasPlanDeTrabajoViewModel temp = new ActividadesAsignadasPlanDeTrabajoViewModel
                        {
                            IdUserPlanDeTrabajoActividad= user.Uspl_Id,
                            NombreUser = nombre.Pers_Nom1 + " " + nombre.Pers_Apel1,
                            IdPlantTrabajo = plantrabajo.Plat_Id,
                            IdActiCumplimiento = item.Acum_Id,
                            DescripcionCumplimiento = item.Acum_Desc,
                            NombrePlanTrabajo = plantrabajo.Plat_Nom

                        };
                        actiCumplimientoAsignados.Add(temp);
                    }
                   
                }
            }
            ViewBag.actividades = new SelectList(actiCumplimientoSinAsignar, "Acum_Id", "Acum_Desc");
            PlandetrabajoActividadesViewModel plandetrabajoActividades = new PlandetrabajoActividadesViewModel
            {
                NombrePlanTrabajo = plantrabajo.Plat_Nom,
                IdPlantTrabajo = plantrabajo.Plat_Id
            };
			int _TotalRegistros = 0;
			_TotalRegistros = actiCumplimientoAsignados.Count();
			actiCumplimientoAsignados = actiCumplimientoAsignados.Skip((pagina - 1) * _RegistrosPorPaginaActividades)
											   .Take(_RegistrosPorPaginaActividades)
											   .ToList();
			int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPaginaActividades);
			_PaginadorCustomersActividades = new PaginadorGenerico<ActividadesAsignadasPlanDeTrabajoViewModel>()
			{
				RegistrosPorPagina = _RegistrosPorPaginaActividades,
				TotalRegistros = _TotalRegistros,
				TotalPaginas = _TotalPaginas,
				PaginaActual = pagina,
				Resultado = actiCumplimientoAsignados
			};

			ViewBag.actividadesAsignadas = _PaginadorCustomersActividades;
            return View(plandetrabajoActividades);
        }

        [HttpPost]
        public ActionResult ActividadesPlanTrabajo([Bind(Include = "IdPlantTrabajo,IdActiCumplimiento,IdUser")]PlandetrabajoActividadesViewModel model)
        {
            if (ModelState.IsValid)
            {
                
               UsuariosPlandetrabajo user = new UsuariosPlandetrabajo
                {
                    Acum_Id = model.IdActiCumplimiento,
                    Plat_Id = model.IdPlantTrabajo,
                    Emp_Id=AccountData.NitEmpresa,
                    Id = model.IdUser
                };
                PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(model.IdPlantTrabajo);
                plandeTrabajo.FechaActualizacion = DateTime.Now;
                db.Entry(plandeTrabajo).State = EntityState.Modified;                
                db.Tb_UsersPlandeTrabajo.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("ActividadesPlanTrabajo", new { model.IdPlantTrabajo });
        }     

        public ActionResult EliminarActividadPlanTrabajo(int IdUserPlanTrabajo)
        {

            UsuariosPlandetrabajo usuariosPlandetrabajo = db.Tb_UsersPlandeTrabajo.Find(IdUserPlanTrabajo);
            db.Tb_UsersPlandeTrabajo.Remove(usuariosPlandetrabajo);
            PlandeTrabajo plandeTrabajo = db.Tb_PlandeTrabajo.Find(usuariosPlandetrabajo.Plat_Id);
            plandeTrabajo.FechaActualizacion = DateTime.Now;
            db.Entry(plandeTrabajo).State = EntityState.Modified;
            db.SaveChanges();            
            return RedirectToAction("ActividadesPlanTrabajo", new { IdPlantTrabajo=usuariosPlandetrabajo.Plat_Id });
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
