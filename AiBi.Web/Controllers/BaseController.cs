using AiBi.Test.Bll;
using AiBi.Test.Common;
using AiBi.Test.Dal.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AiBi.Test.Web.Controllers
{
    [Authorize]
    public abstract class BaseController<T,PageReqT> : Controller where T:BaseEntity where PageReqT : PageReq
    {
        public abstract BaseBll<T,PageReqT> Bll { get; }
        public SysUserBll SysUserBll { get; set; }

        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult List()
        {
            return View();
        }
        public virtual ActionResult GetPageList(PageReqT req)
        {
            var res = Bll.GetPageList(req);
            return Json(res);
        }
        public virtual ActionResult Edit(int? id=null)
        {
            return View();
        }
        public virtual ActionResult Detail(int id)
        {
            return View();
        }
        public virtual ActionResult GetByKeys(params object[] keys) {
            return Json(Bll.GetByKeys(keys));
        }

        public ActionResult Error(string title,string msg)
        {
            ViewBag.ErrorTitle = title??"";
            ViewBag.ErrorMessage = msg??"";
            return View("Error");
        }
        public ActionResult Error(string msg)
        {
            ViewBag.ErrorTitle = "";
            ViewBag.ErrorMessage = msg;
            return View("Error");
        }
        #region 底层忽略
        /// <summary>
        /// 重写json方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            Bll.Context.Configuration.LazyLoadingEnabled = false;
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Bll.Context.Configuration.LazyLoadingEnabled = false;
            base.OnActionExecuted(filterContext);
        }
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            if (filterContext.Exception != null)
            {
                return;
            }
            // gzip压缩一下，提升性能和速度，节省下网络带宽
            var acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(acceptEncoding))
            {
                acceptEncoding = acceptEncoding.ToLower();
                var response = filterContext.HttpContext.Response;
                if (acceptEncoding.Contains("br"))
                {
                    response.AppendHeader("Content-encoding", "br");
                    var stream = new Brotli.BrotliStream(response.Filter, CompressionMode.Compress);
                    stream.SetQuality(5);
                    response.Filter = stream;
                }
                else if (acceptEncoding.Contains("gzip"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionLevel.Optimal);
                }
                else if (acceptEncoding.Contains("deflate"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionLevel.Optimal);
                }
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ActionInfo = filterContext.ActionDescriptor;
            ViewBag.CurrentUser = SysUserBll.GetCookie();
        }
        
        /// <summary>
        /// json转换类
        /// </summary>
        public class JsonNetResult : JsonResult
        {
            public JsonSerializerSettings Settings { get; private set; }

            public JsonNetResult()
            {
                Settings = JsonHelper.Settings;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");
                if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("JSON GET is not allowed");
                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
                if (this.ContentEncoding != null)
                    response.ContentEncoding = this.ContentEncoding;
                if (this.Data == null)
                    return;
                var scriptSerializer = JsonSerializer.Create(this.Settings);
                using (var sw = new StringWriter())
                {
                    scriptSerializer.Serialize(sw, this.Data);
                    response.Write(sw.ToString());
                }
            }
        }
        #endregion

    }
}