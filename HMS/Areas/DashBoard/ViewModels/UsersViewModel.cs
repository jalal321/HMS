using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using HMS.Entities;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Areas.DashBoard.ViewModels
{
    public class UsersListingViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        public List<IdentityRole> UserRoles { get; set; }

        public string RoleID { get; set; }

        public Pager pager { get; set; }

        public int totalRecord { get; set; }
    }

    public class UsersActionViewModel
    {
        public string Id { get; set; }

        public  IEnumerable<IdentityRole> Roles { get; set; }
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
        public IList<string> UserRolesNames { get; set; }
        public List<IdentityRole> UserRole { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
       
    }
}