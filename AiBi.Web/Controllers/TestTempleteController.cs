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
    public class TestTempleteController : BaseController<BusTestTemplete,TestTempleteReq.Page>
    {
        public BusTestTempleteBll BusTestTempleteBll { get; set; }

        public override BaseBll<BusTestTemplete, TestTempleteReq.Page> Bll => BusTestTempleteBll;

        public ActionResult MyList()
        {
            return View("List");
        }
        public ActionResult GetMyList(TestTempleteReq.Page req)
        {
            var res = BusTestTempleteBll.GetMyList(req);
            return Json(res);
        }
    }
}