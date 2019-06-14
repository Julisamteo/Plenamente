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
                lst =
                    db.Tb_AutoEvaluacion.Where(a => a.Empr_Nit == AccountData.NitEmpresa)
                    .Select(a =>
                        new EventViewModel
                        {
                            Id = a.Auev_Id,
                            Description = a.Auev_Nom,
                            Title = a.Auev_Nom,
                            Start = a.Auev_Inicio,
                            End = a.Auev_Fin,
                            BackgroundColor = "#7DDAFF",
                            BorderColor = "#9FBDC9",
                            EventRoute = "/Reportes/VerReporte/" + a.Auev_Id
                        }).ToList();
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
			var single1 = new SingleSchedule
			{
				Name = "Meet Bob for Pint",
				TimeOfDay = new TimeSpan(19, 30, 0),
				Date = new DateTime(2012, 5, 8)
			};

			var single2 = new SingleSchedule
			{
				Name = "Confirm Meeting",
				TimeOfDay = new TimeSpan(9, 30, 0),
				Date = new DateTime(2012, 5, 12)
			};

			var simple = new SimpleRepeatingSchedule
			{
				Name = "Sprint Planning Meeting",
				TimeOfDay = new TimeSpan(10, 0, 0),
				SchedulingRange = new Period(new DateTime(2012, 1, 2), new DateTime(2012, 12, 31)),
				DaysBetween = 7
			};

			var weekly = new WeeklySchedule
			{
				Name = "Check Backup Reliability",
				TimeOfDay = new TimeSpan(8, 0, 0),
				SchedulingRange = new Period(new DateTime(2012, 5, 28), new DateTime(2012, 6, 8))
			};
			weekly.SetDays(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });

			var monthly = new MonthlySchedule
			{
				Name = "Check Wages",
				TimeOfDay = new TimeSpan(18, 0, 0),
				DayOfMonth = 31,
				SchedulingRange = new Period(new DateTime(2012, 1, 2), new DateTime(2100, 1, 1))
			};

			var schedules = new List<Schedule> { single1, single2, simple, weekly, monthly };

			var generator = new CalendarGenerator();
			var period = new Period(new DateTime(2012, 5, 1), new DateTime(2012, 6, 30));
			var appointments = generator.GenerateCalendar(period, schedules);

			//foreach (var appointment in appointments)
			//{
			//	Console.WriteLine(
			//		"{0} | {1}", appointment.Time.ToString("yyyy-MM-dd HH:mm"), appointment.Name);
			//}
			//Console.ReadKey();

			return View(appointments);
		}
		#endregion
	}
}
