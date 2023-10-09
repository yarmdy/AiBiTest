﻿using System;
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
    public class TestPlanController : BaseController<BusTestPlan,PlanReq.Page>
    {
        public BusTestPlanBll CurBll { get; set; }

        public override BaseBll<BusTestPlan,PlanReq.Page> Bll => CurBll;

        public ActionResult GetMyList(PlanReq.Page req)
        {
            return Json(CurBll.GetMyList(req));
        }
        public ActionResult Test(int id) {
            return View();
        }
        public ActionResult GetTest(int id)
        {
            return Json(CurBll.GetTest(id));
        }

        public ActionResult Answer(int id,List<BusTestPlanUserOption> list)
        {
            return Json(CurBll.Answer(id,list));
        }

        public ActionResult StartAnswer(int id)
        {
            return Json(CurBll.StartAnswer(id));
        }
        public ActionResult PauseAnswer(int id)
        {
            return Json(CurBll.PauseAnswer(id));
        }
        public ActionResult EndAnswer(int id)
        {
            return Json(CurBll.EndAnswer(id));
        }
    }
}