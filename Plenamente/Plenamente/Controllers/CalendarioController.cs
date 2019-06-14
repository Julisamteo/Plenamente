using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
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
                    db.Tb_AutoEvaluacion.Where(a => a.Empr_Nit == AccountData.NitEmpresa).Select(a =>
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
    }
}
