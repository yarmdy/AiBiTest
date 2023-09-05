using System;
using System.Web;
using System.Web.Mvc;
using AiBi.Test.Web;
using AiBi.Test.Common;
using AiBi.Test.Bll;

namespace AiBi.Test.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorAttribute());
        }
    }

    public class CustomErrorAttribute : HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            Exception exception = filterContext.Exception;
            var code = new HttpException(null,exception).GetHttpCode();
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var json = new JsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                json.Data = new Response { code  =  EnumResStatus.Fail,data=null,msg=exception.GetInnerMessage()};
                filterContext.Result = json;
                return;
            }
            var view = new ViewResult();
            view.ViewName = "Error";
            view.ViewData=new ViewDataDictionary() { { "ErrorTitle",code+""},{ "ErrorMessage", exception.GetInnerMessage() } };
            filterContext.Result = view;
        }
    }
}
