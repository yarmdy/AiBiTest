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

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var code = ex is HttpException ? (ex as HttpException).GetHttpCode() : 500;
            var title = code != 0 ? code + "" : "��������������";
            var message = ex.GetInnerMessage();

            Server.ClearError();

            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(Request.RequestContext, controller);
            ActionResult result;
            if (Request.IsAjaxRequest())
            {
                var result2 = new JsonResult();
                var res = new Response();
                res.code = EnumResStatus.Fail;
                res.msg = message;
                result2.Data = res;
                result2.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                result = result2;
            }
            else
            {
                var result2 = new ViewResult { ViewName = "Error" };
                result2.ViewBag.ErrorTitle = title;
                result2.ViewBag.ErrorMessage = message;
                result = result2;
            }
            Response.StatusCode = code;
            result.ExecuteResult(controller.ControllerContext);
        }
    }

    public static class AjaxRequestExtensionsHttpRequest
    {
        //
        // ժҪ:
        //     Determines whether the specified HTTP request is an AJAX request.
        //
        // ����:
        //   request:
        //     The HTTP request.
        //
        // ���ؽ��:
        //     true if the specified HTTP request is an AJAX request; otherwise, false.
        //
        // �쳣:
        //   T:System.ArgumentNullException:
        //     The request parameter is null (Nothing in Visual Basic).
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (!(request["X-Requested-With"] == "XMLHttpRequest"))
            {
                if (request.Headers != null)
                {
                    return request.Headers["X-Requested-With"] == "XMLHttpRequest";
                }

                return false;
            }

            return true;
        }
    }
}
