using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using Plenamente.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class ActividadCumplimientoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ActividadCumplimiento
        public ActionResult Index()
        {
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            IQueryable<ActiCumplimiento> list = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa);
            //ActiCumplimiento actiEmpresas =  db.Tb_ActiCumplimiento.Find(AccountData.NitEmpresa);



            return View(list.ToList());
        }

        // GET: ActividadCumplimiento/Details/5
        public ActionResult Details(int id)
        {

            ActiCumplimiento list = db.Tb_ActiCumplimiento.Find(id);

            return View(list);

        }

        // GET: ActividadCumplimiento/Create
        public ActionResult Create()
        {
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);

            ViewModelActividadCumplimiento model = new ViewModelActividadCumplimiento();

            return View(model);

        }

        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia,idObjetivo,Frecuencia_desc,period,weekly_0,weekly_1,weekly_2,weekly_3,weekly_4,weekly_5,weekly_6")] ViewModelActividadCumplimiento model)
        {


            /* try
             {*/
            // TODO: Add insert logic here
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            // TODO: Add insert logic here
            ActiCumplimiento actcumplimiento = new ActiCumplimiento
            {
                Acum_Desc = model.NombreActividad,
                Acum_Porcentest = model.Meta,
                Acum_IniAct = model.FechaInicial,
                Acum_FinAct = model.FechaFinal,
                Oemp_Id = model.idObjetivo,
                Acum_Registro = DateTime.Now,
                Id = usuario.Id,
                Frec_Id = 1,
                Peri_Id = 6,
                Empr_Nit = empresa.Empr_Nit
            };

            db.Tb_ActiCumplimiento.Add(actcumplimiento);
            db.SaveChanges();
            //Generamos la programacion de tareas en el tiempo.
            generateAppoiment(model, actcumplimiento.Acum_Id);

            return RedirectToAction("Index");
            /*}
          catch
           {
               return View();
           }*/
        }

        private void generateAppoiment(ViewModelActividadCumplimiento model, int idActcumplimiento)
        {
            List<Schedule> schedules = new List<Schedule> ();

            if (model.Frecuencia_desc == "norepeat")
            {
                SingleSchedule single1 = new SingleSchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora, //new TimeSpan(19, 30, 0),
                    Date = model.FechaInicial.Date
                };
                schedules.Add(single1);
            }
            else if (model.Frecuencia_desc == "daily")
            {
                SimpleRepeatingSchedule simple = new SimpleRepeatingSchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora,//new TimeSpan(10, 0, 0),
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                    DaysBetween = model.period
                };
                schedules.Add(simple);
            }
            else if (model.Frecuencia_desc == "weekly")
            {
                WeeklySchedule weekly = new WeeklySchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora,//TimeSpan(8, 0, 0),
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),                    
                };
                weekly.SetDays(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });

                schedules.Add(weekly);
            }
            else if (model.Frecuencia_desc == "monthly")
            {
                MonthlySchedule monthly = new MonthlySchedule
                {
                    Name = model.NombreActividad,
                    TimeOfDay = model.hora,//TimeSpan(8, 0, 0),
                    DayOfMonth = model.period,
                    SchedulingRange = new Period(model.FechaInicial.Date, model.FechaFinal.Date),
                };
                schedules.Add(monthly);
            }            

            CalendarGenerator generator = new CalendarGenerator();
            Period period = new Period(model.FechaInicial.Date, model.FechaFinal.Date);
            IEnumerable<Appointment> appointments = generator.GenerateCalendar(period, schedules);
            foreach (var app in appointments)
            {
                

                db.Tb_ProgamacionTareas.Add(
                new ProgamacionTareas
                    {
                        ActiCumplimiento_Id = idActcumplimiento,
                        Descripcion = app.Name,
                        //FechaHora = new DateTime(model.FechaInicial.Year, model.FechaInicial.Month, model.FechaInicial.Day, model.hora.Hours, model.hora.Minutes, model.hora.Seconds),
                        FechaHora = app.Time,
                        Estado = true,                    
                    }
                );
                db.SaveChanges();
            }            
        }

        // GET: ActividadCumplimiento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Edit/5
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

        // GET: ActividadCumplimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActividadCumplimiento/Delete/5
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
