using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using Plenamente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        //Vista inicial de los usuarios en donde se muestran graficas, notificaciones, etc.
        // GET: Administrador/admin
        public ActionResult Index()
        {
            return View();
        }

        // Controllers

        //Vista que permite la administración de los usuarios,
        //Lista Usuarios y genera los botones de crear usuario, crear rol, editar y eliminar usuarios,
        //Incluye el paginado de la lista y la busqueda por la cadena de texto ingresada
        // GET: /Admin/
        //[Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult Index(string searchStringUserNameOrEmail)
        public ActionResult ManageUsers(string searchStringUserNameOrEmail, string currentFilter, int? page)
        {
            try
            {
                int intPage = 1;
                int intPageSize = 10;
                int intTotalPageCount = 0;

                if (searchStringUserNameOrEmail != null)
                {
                    intPage = 1;
                }
                else
                {
                    if (currentFilter != null)
                    {
                        searchStringUserNameOrEmail = currentFilter;
                        intPage = page ?? 1;
                    }
                    else
                    {
                        searchStringUserNameOrEmail = "";
                        intPage = page ?? 1;
                    }
                }

                ViewBag.CurrentFilter = searchStringUserNameOrEmail;

                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();
                int intSkip = (intPage - 1) * intPageSize;

                intTotalPageCount = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .Count();

                var result = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .OrderBy(x => x.UserName)
                    .Skip(intSkip)
                    .Take(intPageSize)
                    .ToList();

                foreach (var item in result)
                {
                    ExpandedUserDTO objUserDTO = new ExpandedUserDTO();

                    objUserDTO.UserName = item.UserName;
                    objUserDTO.Email = item.Email;
                    objUserDTO.LockoutEndDateUtc = item.LockoutEndDateUtc;
                    objUserDTO.Nombres = item.Pers_Nom1;
                    objUserDTO.Apellidos = item.Pers_Apel1;
                    objUserDTO.Documento = item.Pers_Doc;
                    objUserDTO.Pers_Licencia = item.Pers_Licencia;
                    objUserDTO.Pers_LicVence = item.Pers_LicVence;
                    objUserDTO.Pers_Direccion = item.Pers_Dir;
                    objUserDTO.Pers_ContactoEmeg = item.Pers_Cemeg;
                    objUserDTO.Pers_TelefonoEmeg = item.Pers_Temeg;
                    objUserDTO.Tdoc_Id = item.Tdoc_Id;
                    objUserDTO.Sciu_Id = item.Sciu_Id;
                    //objUserDTO.Ciudad = item.Ciudad;
                    objUserDTO.Cemp_Id = item.Cemp_Id;
                    objUserDTO.Aemp_Id = item.Aemp_Id;
                    objUserDTO.Cate_Id = item.Cate_Id;
                    objUserDTO.Gene_Id = item.Gene_Id;
                    objUserDTO.Jemp_Id = item.Jemp_Id;
                    objUserDTO.Tvin_Id = item.Tvin_Id;
                    objUserDTO.Eps_Id = item.Eps_Id;
                    objUserDTO.Afp_Id = item.Afp_Id;
                    objUserDTO.Arl_Id = item.Arl_Id;
                    objUserDTO.Empr_Nit = item.Empr_Nit;
                    objUserDTO.Espe_Id = item.Espe_Id;
                    objUserDTO.Jefe_Id = item.Jefe_Id;

                    col_UserDTO.Add(objUserDTO);
                    ViewBag.Empr_Nit = item.Empr_Nit;
                }

                // Set the number of pages
                var _UserDTOAsIPagedList =
                    new StaticPagedList<ExpandedUserDTO>
                    (
                        col_UserDTO, intPage, intPageSize, intTotalPageCount
                        );

                return View(_UserDTOAsIPagedList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();

                return View(col_UserDTO.ToPagedList(1, 25));
            }
        }
        #endregion

        // Users *****************************

        // GET: /Admin/Edit/Create 
        
        #region public ActionResult Create()
        //Función que permite obtener los campos de creación del usuario generado en el modelo UserRolesDTO en la clase ExpandedUserDTO
        public ActionResult Create(int? Empr_Nit)
        {
            List<TipoDocumento> tipoDocumentoList = db.Tb_TipoDocumento.ToList();
            ViewBag.TdocBag = new SelectList(tipoDocumentoList, "Tdoc_Id", "Tdoc_Nom");
            List<Afp> AfpList = db.Tb_Afp.ToList();
            ViewBag.Afp_Id = new SelectList(AfpList, "Afp_Id", "Afp_Nom");
            List<Eps> EpsList = db.Tb_Eps.ToList();
            ViewBag.Eps_Id = new SelectList(EpsList, "Eps_Id", "Eps_Nom");
            List<Arl> ArlList = db.Tb_Arl.ToList();
            ViewBag.Arl_Id = new SelectList(ArlList, "Arl_Id", "Arl_Nom");
            List<Ciudad> CiudadList = db.Tb_Ciudad.ToList();
            ViewBag.Ciud_Id = new SelectList(CiudadList, "Ciud_Id", "Ciud_Nom");
            List<SedeCiudad> SedeCiudadList = db.Tb_SedeCiudad.ToList();
            ViewBag.Sciu_id = new SelectList(SedeCiudadList, "Sciu_Id", "Sciu_Nom");
            List<CargoEmpresa> CargoEmpresaList = db.Tb_CargoEmpresa.ToList();
            ViewBag.Cemp_Id = new SelectList(CargoEmpresaList, "Cemp_Id", "Cemp_Nom");
            List<AreaEmpresa> AreaEmpresaList = db.Tb_AreaEmpresa.ToList();
            ViewBag.Aemp_Id = new SelectList(AreaEmpresaList, "Aemp_Id", "Aemp_Nom");
            List<CateLicencia> CateLicenciaList = db.Tb_CateLicencia.ToList();
            ViewBag.Cate_Id = new SelectList(CateLicenciaList, "Cate_Id", "Cate_Nom");
            List<Genero> GeneroList = db.Tb_Genero.ToList();
            ViewBag.Gene_Id = new SelectList(GeneroList, "Gene_Id", "Gene_Nom");
            List<JornadaEmpresa> JornadaEmpresasList = db.Tb_JornadaEmpresa.ToList();
            ViewBag.Jemp_Id = new SelectList(JornadaEmpresasList, "Jemp_Id", "Jemp_Nom");
            List<TipoVinculacion> TipoVinculacionList = db.Tb_TipoVinculacion.ToList();
            ViewBag.Tvin_Id = new SelectList(TipoVinculacionList, "Tvin_Id", "Tvin_Nom");
            List<EstadoPersona> EstadoPersonaList = db.Tb_EstadoPersona.ToList();
            ViewBag.Espe_Id = new SelectList(EstadoPersonaList, "Espe_Id", "Espe_Nom");
            List<ApplicationUser> IdentityUsersList = db.Users.ToList();
            ViewBag.Jefe_Id = new SelectList(IdentityUsersList, "Jefe_Id", "Pers_Nom1");

            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();

            ViewBag.Roles = GetAllRolesAsSelectList();

            return View(objExpandedUserDTO);
        }
        #endregion

        //Método POST para enviar los campos llenados del formulario 
        // PUT: /Admin/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Create(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult Create(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var Email = paramExpandedUserDTO.Email.Trim();
                var UserName = paramExpandedUserDTO.Email.Trim();
                var Password = paramExpandedUserDTO.Password.Trim();
                var Nombre = paramExpandedUserDTO.Nombres.Trim();
                var Apellido = paramExpandedUserDTO.Apellidos.Trim();
                var Documento = paramExpandedUserDTO.Documento;
                var Licencia = paramExpandedUserDTO.Pers_Licencia;
                var VigLicencia = paramExpandedUserDTO.Pers_LicVence;
                var Direccion = paramExpandedUserDTO.Pers_Direccion;
                var ContactoEme = paramExpandedUserDTO.Pers_ContactoEmeg;
                var TelefonoEme = paramExpandedUserDTO.Pers_TelefonoEmeg;
                var TipoDocumento = paramExpandedUserDTO.Tdoc_Id;
                var Afp = paramExpandedUserDTO.Afp_Id;
                var Eps = paramExpandedUserDTO.Eps_Id;
                var Arl = paramExpandedUserDTO.Arl_Id;
                var Ciudad = paramExpandedUserDTO.Ciud_Id;
                var SedeCiudad = paramExpandedUserDTO.Sciu_Id;
                var CargoEmpresa = paramExpandedUserDTO.Cemp_Id;
                var AreaEmpresa = paramExpandedUserDTO.Aemp_Id;
                var CategoriaLicencia = paramExpandedUserDTO.Cate_Id;
                var Genero = paramExpandedUserDTO.Gene_Id;
                var Jornada = paramExpandedUserDTO.Jemp_Id;
                var TipoVinculacion = paramExpandedUserDTO.Tvin_Id;
                var EstadoPersona = paramExpandedUserDTO.Espe_Id;
                var Jefe = paramExpandedUserDTO.Jefe_Id;


                if (Email == "")
                {
                    throw new Exception("No Email");
                }

                if (Password == "")
                {
                    throw new Exception("No Password");
                }

                // convierte en minusculas el email ingresado
                UserName = Email.ToLower();

                // Proceso de creación del usuario

                var objNewAdminUser = new ApplicationUser
                {
                    UserName = UserName,
                    Email = Email,
                    Pers_Nom1 = Nombre,
                    Pers_Apel1 = Apellido,
                    Pers_Doc = Documento,
                    Pers_Licencia = Licencia,
                    Pers_LicVence = VigLicencia,
                    Pers_Dir = Direccion,
                    Pers_Cemeg = ContactoEme,
                    Pers_Temeg = TelefonoEme,
                    Tdoc_Id = TipoDocumento,
                    Afp_Id = Afp,
                    Arl_Id = Arl,
                    Eps_Id = Eps,
                    Sciu_Id = SedeCiudad,
                    Cemp_Id = CargoEmpresa,
                    Aemp_Id = AreaEmpresa,
                    Cate_Id = CategoriaLicencia,
                    Gene_Id = Genero,
                    Jemp_Id = Jornada,
                    Tvin_Id = TipoVinculacion,
                    Espe_Id = EstadoPersona,
                    Jefe_Id = Jefe


                };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);

                if (AdminUserCreateResult.Succeeded == true)
                {
                    string strNewRole = Convert.ToString(Request.Form["Roles"]);

                    if (strNewRole != "0")
                    {
                        // Le asigna un rol al usuario
                        UserManager.AddToRole(objNewAdminUser.Id, strNewRole);
                    }
                    db.Users.Add(objNewAdminUser);
                    db.SaveChanges();
                    return RedirectToAction("Manageusers");
                }
                else
                {
                    ViewBag.Roles = GetAllRolesAsSelectList();
                    ModelState.AddModelError(string.Empty,
                        "Error: Failed to create the user. Check password requirements.");
                    return View(paramExpandedUserDTO);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Roles = GetAllRolesAsSelectList();
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("Create");
            }
        }
        #endregion

        public ActionResult TrueUserCreate()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<TipoDocumento> tipoDocumentoList = db.Tb_TipoDocumento.ToList();
            ViewBag.TdocBag = new SelectList(tipoDocumentoList, "Tdoc_Id", "Tdoc_Nom");
            List<Afp> AfpList = db.Tb_Afp.ToList();
            ViewBag.Afp_Id = new SelectList(AfpList, "Afp_Id", "Afp_Nom");
            List<Eps> EpsList = db.Tb_Eps.ToList();
            ViewBag.Eps_Id = new SelectList(EpsList, "Eps_Id", "Eps_Nom");
            List<Arl> ArlList = db.Tb_Arl.ToList();
            ViewBag.Arl_Id = new SelectList(ArlList, "Arl_Id", "Arl_Nom");
            List<Ciudad> CiudadList = db.Tb_Ciudad.ToList();
            ViewBag.Ciud_Id = new SelectList(CiudadList, "Ciud_Id", "Ciud_Nom");
            List<SedeCiudad> SedeCiudadList = db.Tb_SedeCiudad.ToList();
            ViewBag.Sciu_id = new SelectList(SedeCiudadList, "Sciu_Id", "Sciu_Nom");
            List<CargoEmpresa> CargoEmpresaList = db.Tb_CargoEmpresa.ToList();
            ViewBag.Cemp_Id = new SelectList(CargoEmpresaList, "Cemp_Id", "Cemp_Nom");
            List<AreaEmpresa> AreaEmpresaList = db.Tb_AreaEmpresa.ToList();
            ViewBag.Aemp_Id = new SelectList(AreaEmpresaList, "Aemp_Id", "Aemp_Nom");
            List<CateLicencia> CateLicenciaList = db.Tb_CateLicencia.ToList();
            ViewBag.Cate_Id = new SelectList(CateLicenciaList, "Cate_Id", "Cate_Nom");
            List<Genero> GeneroList = db.Tb_Genero.ToList();
            ViewBag.Gene_Id = new SelectList(GeneroList, "Gene_Id", "Gene_Nom");
            List<JornadaEmpresa> JornadaEmpresasList = db.Tb_JornadaEmpresa.ToList();
            ViewBag.Jemp_Id = new SelectList(JornadaEmpresasList, "Jemp_Id", "Jemp_Nom");
            List<TipoVinculacion> TipoVinculacionList = db.Tb_TipoVinculacion.ToList();
            ViewBag.Tvin_Id = new SelectList(TipoVinculacionList, "Tvin_Id", "Tvin_Nom");
            List<EstadoPersona> EstadoPersonaList = db.Tb_EstadoPersona.ToList();
            ViewBag.Espe_Id = new SelectList(EstadoPersonaList, "Espe_Id", "Espe_Nom");
            List<ApplicationUser> IdentityUsersList = db.Users.ToList();
            ViewBag.Jefe_Id = new SelectList(IdentityUsersList, "Jefe_Id", "Pers_Nom1");

            ViewBag.Roles = GetAllRolesAsSelectList();

            return View();
        }

        [HttpPost]
        public ActionResult SaveRecord(ExpandedUserDTO userdto)
        {
            try
            {
                
                {
                    ApplicationUser user = new ApplicationUser();
                    user.Email = userdto.Email.Trim();
                    user.UserName = userdto.UserName.Trim();
                    user.PasswordHash = userdto.Password.Trim();
                    user.Pers_Nom1 = userdto.Nombres.Trim();
                    user.Pers_Apel1 = userdto.Apellidos.Trim();
                    user.Pers_Doc = userdto.Documento;
                    user.Pers_Licencia = userdto.Pers_Licencia;
                    user.Pers_LicVence = userdto.Pers_LicVence;
                    user.Pers_Dir = userdto.Pers_Direccion;
                    user.Pers_Cemeg = userdto.Pers_ContactoEmeg;
                    user.Pers_Temeg = userdto.Pers_TelefonoEmeg;
                    user.Tdoc_Id = userdto.Tdoc_Id;
                    user.Gene_Id = userdto.Gene_Id;
                    user.Espe_Id = userdto.Espe_Id;
                    user.Cate_Id = userdto.Cate_Id;


                    db.Users.Add(user);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("TrueUserCreate");
        }

        // GET: /Admin/Edit/TestUser 
        [Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult EditUser(string UserName)
        public ActionResult EditUser(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // PUT: /Admin/EditUser
        [Authorize(Roles = "SuperAdmin2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ExpandedUserDTO objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                return Redirect("~/Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(paramExpandedUserDTO.UserName));
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUser
        [Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult DeleteUser(string UserName)
        public ActionResult DeleteUser(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (UserName.ToLower() == this.User.Identity.Name.ToLower())
                {
                    ModelState.AddModelError(
                        string.Empty, "Error: Cannot delete the current user");

                    return View("EditUser");
                }

                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                }

                return RedirectToAction("Manageusers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(UserName));
            }
        }
        #endregion

        // GET: /Admin/EditRoles/TestUser 
        [Authorize(Roles = "SuperAdmin2")]
        #region ActionResult EditRoles(string UserName)
        public ActionResult EditRoles(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserName = UserName.ToLower();

            // Check that we have an actual user
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }

            UserAndRolesDTO objUserAndRolesDTO =
                GetUserAndRoles(UserName);

            return View(objUserAndRolesDTO);
        }
        #endregion

        // PUT: /Admin/EditRoles/TestUser 
        [Authorize(Roles = "SuperAdmin2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        {
            try
            {
                if (paramUserAndRolesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                string UserName = paramUserAndRolesDTO.UserName;
                string strNewRole = Convert.ToString(Request.Form["AddRole"]);

                if (strNewRole != "No Roles Found")
                {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByName(UserName);

                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View(objUserAndRolesDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditRoles");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteRole?UserName="TestUser&RoleName=Administrator
        [Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult DeleteRole(string UserName, string RoleName)
        public ActionResult DeleteRole(string UserName, string RoleName)
        {
            try
            {
                if ((UserName == null) || (RoleName == null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                UserName = UserName.ToLower();

                // Check that we have an actual user
                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                if (UserName.ToLower() ==
                    this.User.Identity.Name.ToLower() && RoleName == "Administrator")
                {
                    ModelState.AddModelError(string.Empty,
                        "Error: Cannot delete Administrator Role for the current user");
                }

                // Go get the User
                ApplicationUser user = UserManager.FindByName(UserName);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, RoleName);
                UserManager.Update(user);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                return RedirectToAction("EditRoles", new { UserName = UserName });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View("EditRoles", objUserAndRolesDTO);
            }
        }
        #endregion

        // Roles *****************************

        // GET: /Admin/ViewAllRoles
        [Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult ViewAllRoles()
        public ActionResult ViewAllRoles()
        {
            var roleManager =
                new RoleManager<IdentityRole>
                (
                    new RoleStore<IdentityRole>(new ApplicationDbContext())
                    );

            List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                        select new RoleDTO
                                        {
                                            Id = objRole.Id,
                                            RoleName = objRole.Name
                                        }).ToList();

            return View(colRoleDTO);
        }
        #endregion

        // GET: /Admin/AddRole
        [Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult AddRole()
        public ActionResult AddRole()
        {
            RoleDTO objRoleDTO = new RoleDTO();

            return View(objRoleDTO);
        }
        #endregion

        // PUT: /Admin/AddRole
        [Authorize(Roles = "SuperAdmin2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult AddRole(RoleDTO paramRoleDTO)
        public ActionResult AddRole(RoleDTO paramRoleDTO)
        {
            try
            {
                if (paramRoleDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var RoleName = paramRoleDTO.RoleName.Trim();

                if (RoleName == "")
                {
                    throw new Exception("No RoleName");
                }

                // Create Role
                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext())
                        );

                if (!roleManager.RoleExists(RoleName))
                {
                    roleManager.Create(new IdentityRole(RoleName));
                }

                return Redirect("~/Admin/ViewAllRoles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("AddRole");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUserRole?RoleName=TestRole
        [Authorize(Roles = "SuperAdmin2")]
        #region public ActionResult DeleteUserRole(string RoleName)
        public ActionResult DeleteUserRole(string RoleName)
        {
            try
            {
                if (RoleName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (RoleName.ToLower() == "administrator")
                {
                    throw new Exception(String.Format("Cannot delete {0} Role.", RoleName));
                }

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                var UsersInRole = roleManager.FindByName(RoleName).Users.Count();
                if (UsersInRole > 0)
                {
                    throw new Exception(
                        String.Format(
                            "Canot delete {0} Role because it still has users.",
                            RoleName)
                            );
                }

                var objRoleToDelete = (from objRole in roleManager.Roles
                                       where objRole.Name == RoleName
                                       select objRole).FirstOrDefault();
                if (objRoleToDelete != null)
                {
                    roleManager.Delete(objRoleToDelete);
                }
                else
                {
                    throw new Exception(
                        String.Format(
                            "Canot delete {0} Role does not exist.",
                            RoleName)
                            );
                }

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();

                return View("ViewAllRoles", colRoleDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();

                return View("ViewAllRoles", colRoleDTO);
            }
        }
        #endregion


        // Utility

        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region private List<SelectListItem> GetAllRolesAsSelectList()
        private List<SelectListItem> GetAllRolesAsSelectList()
        {
            List<SelectListItem> SelectRoleListItems =
                new List<SelectListItem>();

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

            SelectRoleListItems.Add(
                new SelectListItem
                {
                    Text = "Select",
                    Value = "0"
                });

            foreach (var item in colRoleSelectList)
            {
                SelectRoleListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name.ToString(),
                        Value = item.Name.ToString()
                    });
            }

            return SelectRoleListItems;
        }
        #endregion

        #region private ExpandedUserDTO GetUser(string paramUserName)
        private ExpandedUserDTO GetUser(string paramUserName)
        {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();

            var result = UserManager.FindByName(paramUserName);

            // If we could not find the user, throw an exception
            if (result == null) throw new Exception("Could not find the User");

            objExpandedUserDTO.UserName = result.UserName;
            objExpandedUserDTO.Email = result.Email;
            objExpandedUserDTO.LockoutEndDateUtc = result.LockoutEndDateUtc;
            objExpandedUserDTO.AccessFailedCount = result.AccessFailedCount;
            objExpandedUserDTO.PhoneNumber = result.PhoneNumber;

            return objExpandedUserDTO;
        }
        #endregion

        #region private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO objExpandedUserDTO)
        private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser result =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (result == null)
            {
                throw new Exception("Could not find the User");
            }

            result.Email = paramExpandedUserDTO.Email;

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            UserManager.Update(result);

            // Was a password sent across?
            if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password))
            {
                // Remove current password
                var removePassword = UserManager.RemovePassword(result.Id);
                if (removePassword.Succeeded)
                {
                    // Add new password
                    var AddPassword =
                        UserManager.AddPassword(
                            result.Id,
                            paramExpandedUserDTO.Password
                            );

                    if (AddPassword.Errors.Count() > 0)
                    {
                        throw new Exception(AddPassword.Errors.FirstOrDefault());
                    }
                }
            }

            return paramExpandedUserDTO;
        }
        #endregion

        #region private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser user =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
            UserManager.Update(user);
            UserManager.Delete(user);
        }
        #endregion

        #region private UserAndRolesDTO GetUserAndRoles(string UserName)
        private UserAndRolesDTO GetUserAndRoles(string UserName)
        {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(UserName);

            List<UserRoleDTO> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UserRoleDTO
                 {
                     RoleName = objRole,
                     UserName = UserName
                 }).ToList();

            if (colUserRoleDTO.Count() == 0)
            {
                colUserRoleDTO.Add(new UserRoleDTO { RoleName = "No Roles Found" });
            }

            ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

            // Create UserRolesAndPermissionsDTO
            UserAndRolesDTO objUserAndRolesDTO =
                new UserAndRolesDTO();
            objUserAndRolesDTO.UserName = UserName;
            objUserAndRolesDTO.colUserRoleDTO = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion

        #region private List<string> RolesUserIsNotIn(string UserName)
        private List<string> RolesUserIsNotIn(string UserName)
        {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByName(UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
            var colRolesUserInNotIn = (from objRole in colAllRoles
                                       where !colRolesForUser.Contains(objRole)
                                       select objRole).ToList();

            if (colRolesUserInNotIn.Count() == 0)
            {
                colRolesUserInNotIn.Add("No Roles Found");
            }

            return colRolesUserInNotIn;
        }
        #endregion
    }
}