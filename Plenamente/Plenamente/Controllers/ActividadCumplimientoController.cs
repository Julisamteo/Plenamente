using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.App_Tool;
using Plenamente.Scheduler;
using Plenamente.Models;
using Plenamente.Models.ViewModel;


namespace Plenamente.Controllers
{
    public class ActividadCumplimientoController : Controller
    {
        private readonly int _RegistrosPorPagina = 10;
        private PaginadorGenerico<ActiCumplimiento> _PaginadorCustomers;
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ActividadCumplimiento
        public ActionResult Index(int pagina = 1)
        {
            int _TotalRegistros = 0;
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();            
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var list = db.Tb_ActiCumplimiento.Where(c => c.Empr_Nit == AccountData.NitEmpresa).ToList();
            _TotalRegistros = list.Count();
            list = list.Skip((pagina - 1) * _RegistrosPorPagina)
                                               .Take(_RegistrosPorPagina)
                                               .ToList();
            int _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _PaginadorCustomers = new PaginadorGenerico<ActiCumplimiento>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = list
            };
            //ActiCumplimiento actiEmpresas =  db.Tb_ActiCumplimiento.Find(AccountData.NitEmpresa);
            ViewBag.ReturnUrl = Request.UrlReferrer;
            return View(_PaginadorCustomers);
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
            ViewBag.ReturnUrl = Request.UrlReferrer;
            return View(model);

        }

        // POST: ActividadCumplimiento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NombreActividad,Meta,FechaInicial,FechaFinal,hora,Frecuencia,idObjetivo,Frecuencia_desc,period,weekly_0,weekly_1,weekly_2,weekly_3,weekly_4,weekly_5,weekly_6,retornar")] ViewModelActividadCumplimiento model)
        {


            /* try
             {*/
            // TODO: Add insert logic here
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();

            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            string dias = "";
            if(model.weekly_0 != null)
            {
                dias += "lunes,";
            }
            if (model.weekly_1 != null)
            {
                dias += "martes,";
            }
            if (model.weekly_2 != null)
            {
                dias += "miercoles,";
            }
            if (model.weekly_3 != null)
            {
                dias += "jueves,";
            }
            if (model.weekly_4 != null)
            {
                dias += "viernes,";
            }
            if (model.weekly_5 != null)
            {
                dias += "sabado,";
            }
            if (model.weekly_5 != null)
            {
                dias += "domingo,";
            }

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
                Frec_Id = Convert.ToInt32(model.Frecuencia),
                Peri_Id = 6,
                Empr_Nit = empresa.Empr_Nit,
                Repeticiones=model.period,
                DiasSemana=dias,
                HoraAct=model.hora
                

            };

            db.Tb_ActiCumplimiento.Add(actcumplimiento);
            db.SaveChanges();
            //Generamos la programacion de tareas en el tiempo.
            generateAppoiment(model, actcumplimiento.Acum_Id);

            var link = model.retornar;
            return Redirect(link);
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

                //Seteamos loas dias de la semana seleccionados.
                int i = 0;
                List<DayOfWeek> dayOfWeeks = new List<DayOfWeek> ();
                if (model.weekly_0 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Monday);
                    i++;
                }

                if (model.weekly_1 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Tuesday);                    
                    i++;
                }

                if (model.weekly_2 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Wednesday);                    
                    i++;
                }

                if (model.weekly_3 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Thursday);
                    i++;
                }

                if (model.weekly_4 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Friday);
                    i++;
                }

                if (model.weekly_5 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Saturday);
                    i++;
                }

                if (model.weekly_6 != null)
                {
                    dayOfWeeks.Add(DayOfWeek.Sunday);
                    i++;
                }

                weekly.SetDays(dayOfWeeks);

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
            }
            db.SaveChanges();
        }

        // GET: ActividadCumplimiento/Edit/5
        public ActionResult Edit(int id)
        {
            var listfrec = db.Tb_Frecuencia.Select(o => new { Id = o.Frec_Id , Value = o.Frec_Descripcion }).ToList();
            ViewBag.frecuenciaEmpresa = new SelectList(listfrec, "Id", "Value");
            var list = db.Tb_ObjEmpresa.Where(c => c.Empr_Nit == AccountData.NitEmpresa).Select(o => new { Id = o.Oemp_Id, Value = o.Oemp_Nombre }).ToList();
            ViewBag.objetivosEmpresa = new SelectList(list, "Id", "Value");
            Empresa empresa = db.Tb_Empresa.Where(e => e.Empr_Nit == AccountData.NitEmpresa).FirstOrDefault();
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            var model = db.Tb_ActiCumplimiento.Find(id);
            if (model.DiasSemana != null)
            {
                var lunes = model.DiasSemana.Contains("lunes");
                var martes = model.DiasSemana.Contains("martes");
                var miercoles = model.DiasSemana.Contains("miercoles");
                var jueves = model.DiasSemana.Contains("jueves");
                var viernes = model.DiasSemana.Contains("viernes");
                var sabado = model.DiasSemana.Contains("sabado");
                var domingo = model.DiasSemana.Contains("domingo");
                if (lunes)
                {
                    ViewData["lunes"] = "checked";
                }

                if (martes)
                {
                    ViewData["martes"] = "checked";
                }

                if (miercoles)
                {
                    ViewData["miercoles"] = "checked";
                }

                if (jueves)
                {
                    ViewData["jueves"] = "checked";
                }

                if (viernes)
                {
                    ViewData["viernes"] = "checked";
                }

                if (sabado)
                {
                    ViewData["sabado"] = "checked";
                }

                if (domingo)
                {
                    ViewData["domingo"] = "checked";
                }
            }
            ViewData["userid"] = model.Id;
            return View(model);
        }

        // POST: ActividadCumplimiento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Acum_Id,Acum_Desc,Acum_Porcentest,Acum_IniAct,Acum_FinAct,Oemp_Id,Id,Peri_Id,Empr_Nit,Frec_Id")] ActiCumplimiento actiCumplimiento)
        {
            if (ModelState.IsValid)
            {

                db.Entry(actiCumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actiCumplimiento);
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
