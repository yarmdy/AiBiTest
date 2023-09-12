using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AiBi.Test.Bll;
using AiBi.Test.Common;
using System.Web.Security;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Web.Controllers
{
    public class TestTemplateController : BaseController<BusTestTemplate,TestTemplateReq.Page>
    {
        public BusTestTemplateBll CurBll { get; set; }

        public override BaseBll<BusTestTemplate, TestTemplateReq.Page> Bll => CurBll;

        public ActionResult MyList()
        {
            return View("List");
        }
        public ActionResult GetMyList(TestTemplateReq.Page req)
        {
            var res = CurBll.GetMyList(req);
            return Json(res);
        }

        public ActionResult MySelect()
        {
            return View("List");
        }
    }
}