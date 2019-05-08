using PagedList;
using Plenamente.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Plenamente.Controllers;
namespace Plenamente.Areas.Administrador.Controllers
{
    public class EncuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Encuestas
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var cargos = from s in db.Tb_Encuesta
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cargos = cargos.Where(s => s.Encu_Vence.ToString().Contains(searchString)
                                       || s.Encu_Vence.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    cargos = cargos.OrderByDescending(s => s.Encu_Vence.ToString());
                    break;
                default:  // Name ascending 
                    cargos = cargos.OrderBy(s => s.Encu_Vence.ToString());
                    break;
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(cargos.ToPagedList(pageNumber, pageSize));
        }
        // GET: Administrador/Encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // GET: Administrador/Encuestas/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/Encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Encu_Id,Encu_Creacion,Encu_Vence,Encu_Estado,Encu_Registro,Empr_Nit")] Encuesta encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Encuesta.Add(encuesta);
                db.SaveChanges();
                GuardarPreguntas();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", encuesta.Empr_Nit);
            return View(encuesta);
        }

        // GET: Administrador/Encuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", encuesta.Empr_Nit);
            return View(encuesta);
        }

        // POST: Administrador/Encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Encu_Id,Encu_Creacion,Encu_Vence,Encu_Estado,Encu_Registro,Empr_Nit")] Encuesta encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", encuesta.Empr_Nit);
            return View(encuesta);
        }

        // GET: Administrador/Encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // POST: Administrador/Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            db.Tb_Encuesta.Remove(encuesta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void GuardarPreguntas()
        {
            var maxEncuesta = db.Tb_Encuesta.Max(x => x.Encu_Id);

            var fechaActual = DateTime.Now;
            var fechaFinal = fechaActual.ToString("yyyy-MM-dd h:m:s");
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Apellidos y Nombres Completos', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta8 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese su Nombre y Apellidos','textBox','" + fechaFinal + "', '" + maxPregunta8 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Diligenciamiento', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta9 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese la Fecha','fechaTiempo','" + fechaFinal + "', '" + maxPregunta9 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Cargo u Ocupación', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta10 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Su Fecha de Nacimiento','textBox','" + fechaFinal + "', '" + maxPregunta10 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Área de Trabajo', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta11 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Su Área de trabajo','textBox','" + fechaFinal + "', '" + maxPregunta11 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione uno de los rangos a los que corresponde su edad', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('18-27','radioButton','" + fechaFinal + "', '" + maxPregunta + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('28-37', 'radioButton','" + fechaFinal + "', '" + maxPregunta + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('38-47', 'radioButton' ,'" + fechaFinal + "', '" + maxPregunta + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('48 o Más', 'radioButton' ,'" + fechaFinal + "', '" + maxPregunta + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione su Estado Civil', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta1 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Soltero (A)', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Casado (A)/ Unión Libre', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Separado (A)/ Divorciado', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Viudo (A)', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Genero', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta2 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Hombre', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta2 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Mujer', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta2 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Nacimiento', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta12 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Su Fecha de Nacimiento','fechaTiempo','" + fechaFinal + "', '" + maxPregunta12 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Numero de Personas a Cargo', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta3 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ninguna', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('1 a 3 Personas', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('4 a 6 Personas', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 6 Personas', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Numero de Hijos', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta4 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No tiene Hijos', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('1 a 3 Hijos', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('4 a 6 Hijos', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 6 Hijos', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione su Nivel de Escolaridad', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta5 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Primaria', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Secundaria', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Técnico Tecnólogo', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Estudiante Universitario', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Profesional', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            ////Fin Opcines
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Profesión', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta13 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese su Prefesion','textBox','" + fechaFinal + "', '" + maxPregunta13 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Tipo de Vivienda', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta6 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Propia', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta6 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Arrendada', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta6 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Familiar', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta6 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿A qué estrato pertenece?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta7 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('1', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('2', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('3', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('4', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('5', 'radioButton' ,'" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('6', 'radioButton' ,'" + fechaFinal + "', '" + maxPregunta7 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Con qué servicios cuenta su vivienda?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta14 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Agua', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Luz', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Gas Natural', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Alcantarillado', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Todas Las Anteriores', 'radioButton' ,'" + fechaFinal + "', '" + maxPregunta14 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En qué utiliza su tiempo libre?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta15 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro trabajo', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Labores Domesticas', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Recreación y Deporte', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Estudio', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otros', 'textBox' ,'" + fechaFinal + "', '" + maxPregunta15 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Promedio de Ingresos (S.M.L)', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta16 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Menos de 1 Mínimo Legal (S.M.L.)', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Entre 1 a 3 S.M.L.', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Entre 3 a 6 S.M.L.', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 6 S.M.L', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Cuántos Años Lleva Laborando en La Empresa ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta17 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Menos de 1 año', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 1 a 5 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 5 a 10 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 11 a 15 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 15 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Ingreso a La Empresa', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta18 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Fecha de Ingreso A La Empresa','fechaTiempo','" + fechaFinal + "', '" + maxPregunta18 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Tipo de Contratación?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta19 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Término Indefinido', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Fijo', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Por Termino de Labor (Obra o Labor)', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Por Prestación de Servicios', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Honorarios / Servicios Profesionales', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Antigüedad en el Cargo Actual', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta20 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Menos de 1 año', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 1 a 5 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 5 a 10 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 11 a 15 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 15 años', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En qué actividades de Salud ha Participado Usted en la Empresa?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta21 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Vacunación' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Exámenes de laboratorio y otros', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Exámenes médicos anuales', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ninguna', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Le han Diagnosticado Alguna Enfermedad?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta22 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cual?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Consume Bebidas Alcohólicas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta23 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Semanal' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Quincenal', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Mensual', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ocasional', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Nunca he consumido bebidas alcohólicas', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Practica Algún Deporte?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta24 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta24 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta24 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cual?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta24 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Fuma? Si su Respuesta es SI, Explique con qué Frecuencia', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta25 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta25 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta25 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cual?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta25 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Cuáles de las Siguientes Molestias ha Sentido con Frecuencia en los Últimos Seis (6) Meses?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta26 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolor de cabeza', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolor de cuello, espalda y cintura' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolores Musculares', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dificultad para realizar algún movimiento', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Tos frecuente', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dificultad Respiratoria', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Gastritis, Ulcera', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otras alteraciones del funcionamiento digestivo', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Alteraciones del sueño (insomnio, somnolencia)', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Mal genio', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cansancio mental', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolor en el pecho', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cambios visuales', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cansancio, fatiga, ardor o disconfort visual', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Pitos o ruidos continuos o intermitentes en los oídos', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dificultad para oír', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otros', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Conoce los riesgos a los que está expuesto en su lugar de trabajo?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta27 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta27 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta27 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta27 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Ha recibido capacitación sobre el manejo de los riesgos a los que está expuesto?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta28 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta28 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta28 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta28 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera que la iluminación de su puesto de trabajo es adecuada?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta29 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta29 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta29 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta29 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La temperatura de su sitio de trabajo le ocasiona molestias?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta30 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta30 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta30 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta30 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera que los pisos, techos, paredes, escaleras, presentan riesgo para su salud?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta31 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta31 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta31 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta31 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Existen cables sin entubar, empalmes defectuosos, tomas eléctricas sobrecargadas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta32 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta32 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta32 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta32 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Los sitios destinados para el almacenamiento son suficientes? (archivo, materiales y herramientas)', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta33 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta33 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta33 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta33 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Las tareas que desarrolla le exigen realizar movimientos repetitivos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta34 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta34 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta34 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta34 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La altura de la superficie de trabajo es la adecuada a su estatura, la silla y la labor que realiza?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta35 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta35 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta35 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta35 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Tiene espacio suficiente para variar la posición de las piernas y rodillas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta36 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta36 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta36 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta36 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su trabajo le exige mantenerse frente a la pantalla del computador más del 50% de la jornada?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta37 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta37 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta37 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta37 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Al finalizar la jornada laboral, el cansancio que se siente podría calificarse normal?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta38 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta38 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta38 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta38 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera adecuada la distribución del horario de trabajo, de los turnos, de las horas de descanso, horas extras y pausas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta39 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta39 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta39 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta39 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿El trabajo que desempeña le permite aplicar sus habilidades y conocimientos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta40 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta40 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta40 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta40 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La empresa cuenta con agua potable?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta41 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta41 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta41 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Existe buen manejo de basuras y desechos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta42 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta42 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta42 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Las máquinas y herramientas que utiliza en el desempeño de su labor producen vibración?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta43 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta43 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta43 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No Aplica', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta43 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su trabajo lo realiza al aire libre o a la intemperie?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta44 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Siempre' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Nunca', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('A Veces', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro:', 'textBox' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En el sitio de trabajo manipula o está en contacto con productos químicos? ­ En caso de responder SI indicar ¿cuáles?','" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta45 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta45 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta45 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cuales', 'textBox' , '" + fechaFinal + "', '" + maxPregunta45 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Permanece en una misma posición (sentado o de pie) durante más del 60% de la jornada de trabajo?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta46 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta46 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta46 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', 'textBox' , '" + fechaFinal + "', '" + maxPregunta46 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su labor le exige levantar y transportar cargas? ¿cuáles? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta47 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta47 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta47 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta47 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En su puesto de trabajo necesita utilizar elementos de protección personal? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta48 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta48 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta48 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta48 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su labor le exige levantar y transportar cargas? ¿cuáles? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta49 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta49 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta49 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta49 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En su puesto de trabajo necesita utilizar elementos de protección personal? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta50 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta50 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta50 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', 'textBox' , '" + fechaFinal + "', '" + maxPregunta50 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Consentimiento Informado/ Ley 1581 de 2012: de protección de datos personales ­ ¿Acepta poner a disposición de la Empresa la presente información suministrada? * ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta51 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , 'radioButton' , '" + fechaFinal + "', '" + maxPregunta51 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', 'radioButton' , '" + fechaFinal + "', '" + maxPregunta51 + "')");            
            //Fin Opciones


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
