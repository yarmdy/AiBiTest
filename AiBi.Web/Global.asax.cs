using AiBi.Test;
using AiBi.Test.Bll;
using AiBi.Test.Common;
using AiBi.Test.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AiBi.Test.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string Version { get; set; }
        public static string Title { get; set; }
        public static string CopyWrite { get; set; }
        protected void Application_Start()
        {
            Version = System.Configuration.ConfigurationManager.AppSettings["version"] + "";
            Title = System.Configuration.ConfigurationManager.AppSettings["title"] + "";
            CopyWrite = System.Configuration.ConfigurationManager.AppSettings["copywrite"] + "";
            AutofacExt.InitAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e) { 
            var ex = Server.GetLastError();
            var code = ex is HttpException ? (ex as HttpException).GetHttpCode(): 500;
            var title = code!=0 ? code+"" : "发生服务器错误";
            var message = ex.GetInnerMessage();

            Server.ClearError();

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(Request.RequestContext,controller);
            var result = new ViewResult { ViewName="Error"};
            result.ViewBag.ErrorTitle = title;
            result.ViewBag.ErrorMessage = message;
            Response.StatusCode = code;
            result.ExecuteResult(controller.ControllerContext);
        }
    }
}
