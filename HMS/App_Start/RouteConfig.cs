using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new [] { "HMS.Controllers"  }

          );

            ////customized route for Accomodation controler with conflity Second accomdation controller in area dashboard
            //routes.MapRoute(
            //    name: "FEAccomodations",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Accomodation", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new [] { "HMS.Controllers"  }
                
            //);


          
        }
    }
}
