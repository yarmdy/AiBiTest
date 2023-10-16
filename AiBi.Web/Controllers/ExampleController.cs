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
    public class ExampleController : BaseController<BusExample,ExampleReq.Page>
    {
        public BusExampleBll CurBll { get; set; }

        public override BaseBll<BusExample, ExampleReq.Page> Bll => CurBll;

        public ActionResult EditResult(int id)
        {
            return View();
        }

        public ActionResult GetResults(int id)
        {
            return Json(CurBll.GetResults(id));
        }
        public ActionResult SaveResults(int id,BusExample model)
        {
            return Json(CurBll.SaveResults(id,model));
        }

        public override ActionResult Select()
        {
            
            return View("List");
        }
    }
}