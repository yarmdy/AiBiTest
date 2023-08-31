using AiBi.Test;
using AiBi.Test.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AiBi.Test.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string Version { get; set; }
        public static string Title { get; set; }
        protected void Application_Start()
        {
            Version = System.Configuration.ConfigurationManager.AppSettings["version"] + "";
            Title = System.Configuration.ConfigurationManager.AppSettings["title"] + "";
            AutofacExt.InitAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
