using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Routing
{
   public  class LeRoutes:IRegisterRoutes
    {
       public void RegisterRoutes(RouteCollection Routes)
       {
           Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           Routes.MapRoute(
               "Default", // Route name
               "{controller}/{action}/{id}", // URL with parameters
               new { controller = "Mycenter", action = "Index", id = UrlParameter.Optional } // Parameter defaults
           );
       }
    }
}
