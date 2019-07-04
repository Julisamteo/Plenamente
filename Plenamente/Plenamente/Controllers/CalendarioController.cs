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
    public class CalendarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        #region Index method
        /// <summary>  
        /// GET: Home/Index method.  
        /// </summary>  
        /// <returns>Returns - index view page</returns>   
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Get Calendar data method
        /// <summary>  
        /// GET: /Home/GetCalendarData  
        /// </summary>  
        /// <returns>Return data</returns>  
        public ActionResult GetCalendarData()
        {
            JsonResult result = new JsonResult();

            try
            {
                List<EventViewModel> data = LoadData();
                result = Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
        #endregion
        #region Helpers  
        #region Load Data  
        /// <summary>  
        /// Load data method.  
        /// </summary>  
        /// <returns>Returns - Data</returns>  
        private List<EventViewModel> LoadData()
        {
            List<EventViewModel> lst = new List<EventViewModel>();
            try
            {
                List<EventViewModel> cumplimientos =
                 db.Tb_ActiCumplimiento
                    .Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null))
                     .Select(a =>
                         new EventViewModel
                         {
                             Id = a.Acum_Id,
                             Description = "Cumplimiento",
                             Title = a.Acum_Desc,
                             Start = a.Acum_IniAct,
                             End = a.Acum_FinAct,
                             BackgroundColor = a.Finalizada ? "#FF1F17" : "#6CB52D",
                             BorderColor = a.Finalizada ? "#FF6963" : "#65ac1e",
                             EventRoute = "../ActividadCumplimiento/Details/" + a.Acum_Id
                         }).ToList();

                if (cumplimientos != null && cumplimientos.Count > 0)
                {
                    lst.AddRange(cumplimientos);
                }

                List<EventViewModel> planes =
                    db.Tb_ProgamacionTareas
                        .Where(a => a.ActiCumplimiento.Empr_Nit == AccountData.NitEmpresa
                                && a.Estado
                                && a.ActiCumplimiento.Usersplandetrabajo.Count > 0)
                        .Select(a =>
                            new EventViewModel
                            {
                                Id = a.Id,
                                Description = "Tarea programada",
                                Title = a.Descripcion,
                                Start = a.FechaHora,
                                BackgroundColor = "#478064",
                                BorderColor = "#218052",
                                EventRoute = ""
                                //EventRoute = "/ActividadCumplimiento/Create/" + a.Id
                            }).ToList();

                if (planes != null && planes.Count > 0)
                {
                    lst.AddRange(planes);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }
        #endregion
        #endregion

        #region Prueba de metodo de creacion de Eventos - Citas o reuniones periodicas 
        public ActionResult generateAppoiment()
        {
            SingleSchedule single1 = new SingleSchedule
            {
                Name = "Programacion de un unico evento",
                TimeOfDay = new TimeSpan(19, 30, 0),
                Date = new DateTime(2019, 6, 13)
            };

            SingleSchedule single2 = new SingleSchedule
            {
                Name = "Otro ejemplo de unico evento",
                TimeOfDay = new TimeSpan(9, 30, 0),
                Date = new DateTime(2019, 6, 13)
            };

            SimpleRepeatingSchedule simple = new SimpleRepeatingSchedule
            {
                Name = "Planificacion de reunion cada 7 dias",
                TimeOfDay = new TimeSpan(10, 0, 0),
                SchedulingRange = new Period(new DateTime(2019, 1, 2), new DateTime(2019, 12, 31)),
                DaysBetween = 7
            };

            WeeklySchedule weekly = new WeeklySchedule
            {
                Name = "Programacion semanal solo lunes,miercoles y viernes ",
                TimeOfDay = new TimeSpan(8, 0, 0),
                SchedulingRange = new Period(new DateTime(2019, 5, 28), new DateTime(2019, 6, 8))
            };
            weekly.SetDays(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });

            MonthlySchedule monthly = new MonthlySchedule
            {
                Name = "Programacion Mensual fin de mes - Puedes usar otro dia",
                TimeOfDay = new TimeSpan(18, 0, 0),
                DayOfMonth = 31,
                SchedulingRange = new Period(new DateTime(2019, 1, 2), new DateTime(2100, 1, 1))
            };

            List<Schedule> schedules = new List<Schedule> { single1, single2, simple, weekly, monthly };

            CalendarGenerator generator = new CalendarGenerator();
            Period period = new Period(new DateTime(2019, 5, 1), new DateTime(2019, 8, 30));
            IEnumerable<Appointment> appointments = generator.GenerateCalendar(period, schedules);

            return View(appointments);
        }
        #endregion
    }
}
