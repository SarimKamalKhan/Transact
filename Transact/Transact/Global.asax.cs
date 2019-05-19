using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Transact
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
                //added by sarim
             ViewEngines.Engines.Clear(); //clear all engines
            ViewEngines.Engines.Add(new RazorViewEngine());
            //added by sarim
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
