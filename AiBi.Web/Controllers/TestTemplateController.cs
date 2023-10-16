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
            ViewBag.isadmin = AutofacExt.GetService<SysFuncBll>().UserHasFunc(CurrentUserId, "admin");
            return View("List");
        }

        public override ActionResult Select() {
            return View("List");
        }

        public ActionResult ItsList(int id)
        {
            return View("List");
        }
        public ActionResult GetItsList(TestTemplateReq.Page req)
        {
            var res = CurBll.GetItsList(req);
            return Json(res);
        }
        public ActionResult AddToIt(int id, int[] ids) {
            return Json(CurBll.AddToIt(id,ids));
        }
        public ActionResult Remove(int id,int[] ids)
        {
            return Json(CurBll.Remove(id, ids));
        }
    }
}