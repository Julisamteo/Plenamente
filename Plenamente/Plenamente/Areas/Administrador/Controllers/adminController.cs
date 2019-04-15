using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class adminController : Controller
    {
        // GET: Administrador/admin
        public ActionResult Index()
        {
            return View();
        }
    }
}