using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using HMS.Areas.DashBoard.ViewModels;
using HMS.Data;
using HMS.Entities;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace HMS.Areas.DashBoard.Controllers
{
    public class UserController : Controller
    {

        
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager , RoleManager<IdentityRole> roleManager)
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

        ApplicationDbContext db = new ApplicationDbContext();
        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            }
            private set
            {
                _roleManager = value;
            }
        }
        
      


      

        // GET: DashBoard/User
        public ActionResult Index(string searchTerm, string roleID , int? page , int pageSize = 2)
         {
            UsersListingViewModel model = new UsersListingViewModel();

            //model.Users = Userservice.GetAllUsers();
            model.Users = SearchUsers(searchTerm , roleID);
            model.totalRecord = model.Users.Count();

            //pagination logic start from here
            var pager = new Pager(model.totalRecord, page , pageSize);

            model.Users =
                model.Users.Skip((pager.CurrentPage - 1)*pager.PageSize).Take(pager.PageSize).ToList();

            model.SearchTerm = searchTerm;
            //model.Roles = accomodationTypesService.GetAllAccomodationTypes();
            model.Roles = RoleManager.Roles;
            model.RoleID = roleID;
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

         public List<ApplicationUser> SearchUsers(string searchTerm, string roleId)
         {

             //var role = new IdentityRole();
             //role.Name = "Manager";

             //RoleManager.Create(role);

             //var role1 = new IdentityRole();
             //role1.Name = "service Boy";

             //RoleManager.Create(role1);

             var users = UserManager.Users.AsQueryable();

             if (!string.IsNullOrEmpty(searchTerm))
             {
                 users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
             }

             if (!string.IsNullOrEmpty(roleId))
             {
                 //users = users.Where(a => a.Roles == roleID.Value);
             }
 
             return users.OrderBy(a=>a.Email).ToList();
         }

        public ActionResult Listing()
        {
            UsersListingViewModel model = new UsersListingViewModel();
            model.Users = UserManager.Users.ToList();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID, bool isDelete = false)
        {
            UsersActionViewModel model = new UsersActionViewModel();
            ViewBag.isDelete = isDelete;


            if (!string.IsNullOrEmpty(ID) && isDelete)
            {
                //delete here
                 ApplicationUser user =  UserManager.FindById("ID");
                
                 model.Id = user.Id;
                 model.Name = user.UserName;
                //model.NoOfRoom = accomodationPackage.NoOfRoom;
                //model.FeePerNight = accomodationPackage.FeePerNight;
            }

            else if (!string.IsNullOrEmpty(ID) && isDelete == false)
            {
                //edit here
                ApplicationUser user = await UserManager.FindByIdAsync(ID);
                //model.Id = int.Parse(user.Id);
                model.Id = user.Id;
                model.Name = user.UserName;
                model.Email = user.Email;
                model.UserRoles = UserManager.GetRoles(model.Id).ToList();
               
            }
            else
            {
                //new entry
                
            }
            //model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            return PartialView("_Action", model);
        }

        [HttpPost]

        public async Task<JsonResult> Action(UsersActionViewModel model, bool isDeleted = false)
        {

            JsonResult json = new JsonResult();
            IdentityResult result = null;
            
            if (model.Id != null  && isDeleted == false)
            {
                //edit here
                var user = await UserManager.FindByIdAsync(model.Id);

                user.UserName = model.Name;
                user.Email = model.Email;

                result = await UserManager.UpdateAsync(user);
             
            }
            else if (model.Id != null && isDeleted == true)
            {
                //delete here
                 var user = await UserManager.FindByIdAsync(model.Id);
                 result = await UserManager.DeleteAsync(user);
            }

            else
            {
                var user = new ApplicationUser();

                user.UserName = model.Name;
                user.Email = model.Email;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { result.Succeeded, Messag = string.Join(",", result.Errors) };

            return json;


        }

       

    }
}