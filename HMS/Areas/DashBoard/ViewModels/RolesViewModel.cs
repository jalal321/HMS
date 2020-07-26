using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Areas.DashBoard.ViewModels
{
    
        public class RolesListingViewModel
        {
            public IEnumerable<IdentityRole> Roles { get; set; }
            public IEnumerable<ApplicationUser> Users { get; set; }

            public string SearchTerm { get; set; }

           

            //public string RoleID { get; set; }

            public Pager pager { get; set; }

            public int totalRecord { get; set; }
        }

        public class RolesActionViewModel
        {
            public string Id { get; set; }

            public string Name { get; set; }
            
        }

    }
