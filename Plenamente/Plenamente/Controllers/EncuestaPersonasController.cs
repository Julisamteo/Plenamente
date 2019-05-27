using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using System.Net;
namespace Plenamente.Controllers
{
    public class EncuestaPersonasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: EntrevistPersonas
        public ActionResult Index()
        {

            List<EncuestaPersonas> encuPerso = new List<EncuestaPersonas>();

            var Usuarios = from e in db.Users select e;
            /*var tb_AutoEvaluacion = db.Tb_AutoEvaluacion.Include(a => a.Empresa);
            return View(tb_AutoEvaluacion.ToList());*/

            ViewBag.Prueba = Usuarios.FirstOrDefault().Pers_Nom1;

            var Usuario = Usuarios
            .OrderBy(x => x.UserName).ToList();

            foreach (var item in Usuarios)
            {
                EncuestaPersonas model = new EncuestaPersonas();

                model.Nombres = item.Pers_Nom1;
                model.Email = item.Email;
                model.Documento = item.Pers_Doc;
                model.Apellidos = item.Pers_Apel1;
                model.idPersona = item.Id;
                

                encuPerso.Add(model);
            }

            return View(encuPerso);
        }

    }
}
