using PagedList;
using Plenamente.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese su Nombre y Apellidos','radioButton','" + fechaFinal + "', '" + maxPregunta8 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Diligenciamiento', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Cargo u Ocupación', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Área de Trabajo', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione uno de los rangos a los que corresponde su edad', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('18-27', '" + fechaFinal + "', '" + maxPregunta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('28-37', '" + fechaFinal + "', '" + maxPregunta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('38-47', '" + fechaFinal + "', '" + maxPregunta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('48 o Más', '" + fechaFinal + "', '" + maxPregunta + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione su Estado Civil', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta1 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Soltero (A)', '" + fechaFinal + "', '" + maxPregunta1 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Casado (A)/ Unión Libre', '" + fechaFinal + "', '" + maxPregunta1 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Separado (A)/ Divorciado', '" + fechaFinal + "', '" + maxPregunta1 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Viudo (A)', '" + fechaFinal + "', '" + maxPregunta1 + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Genero', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta2 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Hombre', '" + fechaFinal + "', '" + maxPregunta2 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Mujer', '" + fechaFinal + "', '" + maxPregunta2 + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Nacimiento', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Numero de Personas a Cargo', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta3 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Ninguna', '" + fechaFinal + "', '" + maxPregunta3 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('1 a 3 Personas', '" + fechaFinal + "', '" + maxPregunta3 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('4 a 6 Personas', '" + fechaFinal + "', '" + maxPregunta3 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Más de 6 Personas', '" + fechaFinal + "', '" + maxPregunta3 + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Numero de Hijos', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta4 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('No tiene Hijos', '" + fechaFinal + "', '" + maxPregunta4 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('1 a 3 Hijos', '" + fechaFinal + "', '" + maxPregunta4 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('4 a 6 Hijos', '" + fechaFinal + "', '" + maxPregunta4 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Más de 6 Hijos', '" + fechaFinal + "', '" + maxPregunta4 + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione su Nivel de Escolaridad', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta5 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Primaria', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Secundaria', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Técnico Tecnólogo', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Estudiante Universitario', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Profesional', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            ////Fin Opcines
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Profesión', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Tipo de Vivienda', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta6 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Propia', '" + fechaFinal + "', '" + maxPregunta6 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Arrendada', '" + fechaFinal + "', '" + maxPregunta6 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('Familiar', '" + fechaFinal + "', '" + maxPregunta6 + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿A qué estrato pertenece?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //var maxPregunta7 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('1', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('2', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('3 Tecnólogo', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('4 Universitario', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('5', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Registro, Preg_Id) VALUES ('6', '" + fechaFinal + "', '" + maxPregunta5 + "')");
            ////Fin Opciones
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Con qué servicios cuenta su vivienda?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En qué utiliza su tiempo libre?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Promedio de Ingresos (S.M.L)', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Cuántos Años Lleva Laborando en La Empresa ', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Ingreso a La Empresa', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Tipo de Contratación?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Antigüedad en el Cargo Actual', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En qué actividades de Salud ha Participado Usted en la Empresa?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Le han Diagnosticado Alguna Enfermedad?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Consume Bebidas Alcohólicas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Practica Algún Deporte?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Fuma? Si su Respuesta es SI, Explique con qué Frecuencia', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Cuáles de las Siguientes Molestias ha Sentido con Frecuencia en los Últimos Seis (6) Meses?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Conoce los riesgos a los que está expuesto en su lugar de trabajo?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Ha recibido capacitación sobre el manejo de los riesgos a los que está expuesto?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera que la iluminación de su puesto de trabajo es adecuada?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La temperatura de su sitio de trabajo le ocasiona molestias?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera que los pisos, techos, paredes, escaleras, presentan riesgo para su salud?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Existen cables sin entubar, empalmes defectuosos, tomas eléctricas sobrecargadas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Los sitios destinados para el almacenamiento son suficientes? (archivo, materiales y herramientas)', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Las tareas que desarrolla le exigen realizar movimientos repetitivos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La altura de la superficie de trabajo es la adecuada a su estatura, la silla y la labor que realiza?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Tiene espacio suficiente para variar la posición de las piernas y rodillas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su trabajo le exige mantenerse frente a la pantalla del computador más del 50% de la jornada?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Al finalizar la jornada laboral, el cansancio que se siente podría calificarse normal?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera adecuada la distribución del horario de trabajo, de los turnos, de las horas de descanso, horas extras y pausas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿El trabajo que desempeña le permite aplicar sus habilidades y conocimientos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La empresa cuenta con agua potable?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Existe buen manejo de basuras y desechos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Las máquinas y herramientas que utiliza en el desempeño de su labor producen vibración?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su trabajo lo realiza al aire libre o a la intemperie?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En el sitio de trabajo manipula o está en contacto con productos químicos? ­ En caso de responder SI indicar ¿cuáles?','" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Permanece en una misma posición (sentado o de pie) durante más del 60% de la jornada de trabajo?', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su labor le exige levantar y transportar cargas? ¿cuáles? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En su puesto de trabajo necesita utilizar elementos de protección personal? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su labor le exige levantar y transportar cargas? ¿cuáles? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En su puesto de trabajo necesita utilizar elementos de protección personal? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            //db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Consentimiento Informado/ Ley 1581 de 2012: de protección de datos personales ­ ¿Acepta poner a disposición de la Empresa la presente información suministrada? * ', '" + fechaFinal + "','" + maxEncuesta + "')");


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
