using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Plenamente.Models;
using PagedList;
using Microsoft.AspNet.Identity;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class CargoEmpresasController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/CargoEmpresas
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

            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr_Nit = UserCurrent.Empr_Nit;


            var cargos = from s in db.Tb_CargoEmpresa
                         where s.Empr_Nit == Empr_Nit
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cargos = cargos.Where(s => s.Cemp_Nom.Contains(searchString)
                                       || s.Cemp_Nom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    cargos = cargos.OrderByDescending(s => s.Cemp_Nom);
                    break;
                default:  // Name ascending 
                    cargos = cargos.OrderBy(s => s.Cemp_Nom);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(cargos.ToPagedList(pageNumber, pageSize));
        }
       

        // GET: Administrador/CargoEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/CargoEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cemp_Id,Cemp_Nom,Empr_Nit,Cemp_Registro")] CargoEmpresa cargoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_CargoEmpresa.Add(cargoEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // POST: Administrador/CargoEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cemp_Id,Cemp_Nom,Empr_Nit,Cemp_Registro")] CargoEmpresa cargoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(cargoEmpresa);
        }

        // POST: Administrador/CargoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            db.Tb_CargoEmpresa.Remove(cargoEmpresa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        //OleDbConnection Econ;
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase file)
        //{
        //    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //    string filepath = "/excelfolder/" + filename;
        //    file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
        //    InsertExceldata(filepath, filename);
        //    return View();
        //}
        //private void ExcelConn(string filepath)

        //{
        //    string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
        //    Econ = new OleDbConnection(constr);
        //}
        //private void InsertExceldata(string fileepath, string filename)
        //{
        //    string fullpath = Server.MapPath("/excelfolder/") + filename;
        //    ExcelConn(fullpath);
        //    string query = string.Format("Select * from [{0}]", "Sheet1$");
        //    OleDbCommand Ecom = new OleDbCommand(query, Econ);
        //    Econ.Open();
        //    DataSet ds = new DataSet();
        //    OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
        //    Econ.Close();
        //    oda.Fill(ds);
        //    DataTable dt = ds.Tables[0];
        //    SqlBulkCopy objbulk = new SqlBulkCopy(con);
        //    objbulk.DestinationTableName = "Tb_CargoEmpresa";
        //    objbulk.ColumnMappings.Add("Nombre", "Cemp_Nom");
        //    objbulk.ColumnMappings.Add("Nit", "Empr_Nit");
        //    objbulk.ColumnMappings.Add("fecha", "Cemp_Registro");
        //    con.Open();
        //    objbulk.WriteToServer(dt);
        //    con.Close();
        //}
    }
}
