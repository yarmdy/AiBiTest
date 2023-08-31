using AiBi.Test.Web.Models;
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
    public abstract class BaseController : Controller
    {
        public Response Res = new Response();

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
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
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
        }
        public class AutoDateTimeFormat : DateTimeConverterBase
        {
            private static IsoDateTimeConverter dConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            private static IsoDateTimeConverter tConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return tConverter.ReadJson(reader, objectType, existingValue, serializer);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    tConverter.WriteJson(writer, value, serializer);
                    return;
                }
                DateTime dd = (DateTime)value;
                if (dd.Date == dd)
                {
                    dConverter.WriteJson(writer, dd, serializer);
                    return;
                }
                tConverter.WriteJson(writer, dd, serializer);
            }
        }
        /// <summary>
        /// json转换类
        /// </summary>
        public class JsonNetResult : JsonResult
        {
            public JsonSerializerSettings Settings { get; private set; }

            public JsonNetResult()
            {
                Settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //DateFormatString = "yyyy-MM-dd HH:mm:ss",
                };
                Settings.Converters.Add(new AutoDateTimeFormat { });
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
    }
}