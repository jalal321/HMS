using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.DashBoard.ViewModels;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace HMS.Areas.DashBoard.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
        public RoleController()
        {
        }

        public RoleController(ApplicationUserManager userManager, ApplicationSignInManager signInManager , HMSRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

       
        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                //return _roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        
      


      

        // GET: DashBoard/User
        public ActionResult Index(string searchTerm, string roleID , int? page )
         {
            RolesListingViewModel model = new RolesListingViewModel();

            //model.Roles = Roleservice.GetAllRoles();
            model.Roles = SearchRoles(searchTerm);
            model.totalRecord = model.Roles.Count();

            //pagination logic start from here
            var pager = new Pager(model.totalRecord, page );

            model.Roles =
                model.Roles.Skip((pager.CurrentPage - 1)*pager.PageSize).Take(pager.PageSize).ToList();

            model.SearchTerm = searchTerm;
            //model.Roles = RoleManager.Roles;
            model.pager = pager;
            return View(model);
        }

        /// <summary>
        /// service for usres
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public string GetRole(string id)
         {
           var model = UserManager.GetRoles(id).FirstOrDefault();

             return model;
         }

         public List<IdentityRole> SearchRoles(string searchTerm)
         {

             //var role = new IdentityRole();
             //role.Name = "Manager";

             //RoleManager.Create(role);

             //var role1 = new IdentityRole();
             //role1.Name = "service Boy";

             //RoleManager.Create(role1);

             var Roles = RoleManager.Roles.AsQueryable();

             if (!string.IsNullOrEmpty(searchTerm))
             {
                 Roles = Roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
             }
 
             return Roles.OrderBy(a=>a.Name).ToList();
         }

        public ActionResult Listing()
        {
            RolesListingViewModel model = new RolesListingViewModel();
            model.Roles = RoleManager.Roles.ToList();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID, bool isDelete = false)
        {
            RolesActionViewModel model = new RolesActionViewModel();
            ViewBag.isDelete = isDelete;


            if (!string.IsNullOrEmpty(ID) && isDelete)
            {
                //delete here
                var role = await RoleManager.FindByIdAsync(ID);

                model.Id = role.Id;
                model.Name = role.Name;
               
            }

            else if (!string.IsNullOrEmpty(ID) && isDelete == false)
            {
                //edit here
                var role = await RoleManager.FindByIdAsync(ID);
  
                model.Id = role.Id;
                model.Name = role.Name;
               
            }
            else
            {
                //new entry
               
            }
            //model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            return PartialView("_Action", model);
        }

        [HttpPost]

        public async Task<JsonResult> Action(RolesActionViewModel model, bool isDeleted = false)
        {

            JsonResult json = new JsonResult();
            IdentityResult result = null;
            
            if (model.Id != null  && isDeleted == false)
            {
                //edit here
                var role = await RoleManager.FindByIdAsync(model.Id);

                role.Name = model.Name;
                

                result = await RoleManager.UpdateAsync(role);
             
            }
            else if (model.Id != null && isDeleted == true)
            {
                //delete here
                 var role = await RoleManager.FindByIdAsync(model.Id);
                 result = await RoleManager.DeleteAsync(role);
            }

            else
            {
              //create new role
                var role = new IdentityRole();
                role.Name = model.Name;

                result =await RoleManager.CreateAsync(role);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return json;


        }
    }
}