using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using HMS;
using HMS.Data;

namespace HMS.Services
{
    class HMSRoleManager : RoleManager<IdentityRole>
    {
        public HMSRoleManager(IRoleStore<IdentityRole , string> roleStore) : base(roleStore)
        {
            
        }

        public static HMSRoleManager Create(IdentityFactoryOptions<HMSRoleManager> options , IOwinContext Context)
        {
         
            return new HMSRoleManager(new RoleStore<IdentityRole>(Context.Get<HMSContext>()));
        }
    }
}
