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
        private HMSRoleManager _roleManager;
        
        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager , HMSRoleManager roleManager)
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

        //ApplicationDbContext db = new ApplicationDbContext();

        public HMSRoleManager RoleManager
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
         public ActionResult GetSingleUserRoles(string id)
         {
           var model = UserManager.GetRoles(id);

             return PartialView("_GetSingleUserRoles",model.ToList());
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
                 users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()) 
                     || a.UserName.ToLower().Contains(searchTerm.ToLower()));
             }

             if (!string.IsNullOrEmpty(roleId))
             {
                 //users = from user in users
                 //    where user.Roles.Any(a => a.RoleId == roleId)
                 //    select user;
                 var role = RoleManager.FindById(roleId);
                 var userIds = role.Users.Select(a=>a.UserId).ToList();
                 users = users.Where( a=> userIds.Contains(a.Id));
                 
                
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

            model.Roles = RoleManager.Roles;

            if (!string.IsNullOrEmpty(ID) && isDelete)
            {
                //delete here
                 ApplicationUser user =  UserManager.FindById(ID);
                
                 model.Id = user.Id;
                 model.Name = user.UserName;
                 model.Email = user.Email;
                 model.FullName = user.FullName;
                 model.Country = user.Country;
                 model.City = user.City;
                 model.Address = user.Address;
                 model.UserRolesNames = UserManager.GetRoles(user.Id).ToList();
               
            }

            else if (!string.IsNullOrEmpty(ID) && isDelete == false)
            {
                //edit here
                ApplicationUser user = await UserManager.FindByIdAsync(ID);
                //model.Id = int.Parse(user.Id);
                model.Id = user.Id;
                model.Name = user.UserName;
                model.Email = user.Email;
                model.FullName = user.FullName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
                model.UserRolesNames = UserManager.GetRoles(user.Id).ToList();
               
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
                user.FullName = user.FullName;
                user.Country = user.Country;
                user.City = user.City;
                user.Address = user.Address;
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
                //Create new User here with Role
                var user = new ApplicationUser();

                user.UserName = model.Name;
                user.Email = model.Email;
                user.FullName = user.FullName;
                user.Country = user.Country;
                user.City = user.City;
                user.Address = user.Address;
                var role = await RoleManager.FindByIdAsync(model.RoleId);

                result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                   result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                }
                
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return json;


        }

       
        //user roles add and remove actions
        public async Task<ActionResult> GetUserRoles(string id)
        {
            UsersActionViewModel model = new UsersActionViewModel();
            
            var user = await UserManager.FindByIdAsync(id);
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRole = RoleManager.Roles.Where(a => userRoleIDs.Contains(a.Id)).ToList();
            //model.UserRole = RoleManager.Roles.Where(a => a.Id.Contains(userRoleIDs)).ToList();
            model.Name = user.UserName;
            model.Id = id;
            model.Roles = RoleManager.Roles.Where(a=> !userRoleIDs.Contains(a.Id)).ToList();

            return PartialView("_GetUserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> GetUserRoles(string userid , string roleid , bool isRemove = false)
        {
           
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            if (isRemove == false)
            {
                // add to role
                if (userid != null && roleid != null)
                {
                    var role = await RoleManager.FindByIdAsync(roleid);
                   
                    result = await UserManager.AddToRoleAsync(userid, role.Name);

                }
            }
            else if (isRemove == true)
            {
                //remove from role
                if (userid != null && roleid != null)
                {
                    var role = await RoleManager.FindByIdAsync(roleid);
                    //edit here
                    result = await UserManager.RemoveFromRoleAsync(userid, role.Name);

                }
            }
           

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return json;

        }


    }
}