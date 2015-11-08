using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Helper
{
   public  class GlobalApplication:HttpApplication
    {
        public void Application_Start()
        {
           
            Core.Routing.LeRoutes le = new Core.Routing.LeRoutes();
            AreaRegistration.RegisterAllAreas();
            le.RegisterRoutes(RouteTable.Routes);
        }

    }
}
