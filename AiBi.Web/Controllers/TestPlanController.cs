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
    public class TestPlanController : BaseController<BusTestPlan,PlanReq.Page>
    {
        public BusTestPlanBll CurBll { get; set; }

        public override BaseBll<BusTestPlan,PlanReq.Page> Bll => CurBll;

        public ActionResult SpecialEdit(int? id, int? id2 = null) {
            return View("Edit");
        }

        public ActionResult GetMyList(PlanReq.Page req)
        {
            return Json(CurBll.GetMyList(req));
        }
        public ActionResult Test(int id) {
            return View();
        }
        public ActionResult TestSpecial(int id)
        {
            return View();
        }
        public ActionResult GetTest(int id)
        {
            return Json(CurBll.GetTest(id));
        }
        public ActionResult ReportList()
        {
            return View();
        }
        public ActionResult GetReports(PlanReq.Page req)
        {
            return Json(CurBll.GetReports(req));
        }
        public ActionResult Report(int id)
        {
            return View();
        }
        public ActionResult GetReport(int id)
        {
            return Json(CurBll.GetReport(id));
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
        public ActionResult ExpireAnswer(int id)
        {
            return Json(CurBll.ExpireAnswer(id));
        }

        public ActionResult OwnList() {
            return View("List");
        }
        public ActionResult OwnSpecialList()
        {
            return View("List");
        }
        public ActionResult SpecialList()
        {
            return View("List");
        }
        public ActionResult OwnReportList()
        {
            return View("ReportList");
        }
        public ActionResult GetOwnList(PlanReq.Page req)
        {
            return Json(CurBll.GetOwnList(req));
        }
        public ActionResult GetOwnReports(PlanReq.Page req)
        {
            return Json(CurBll.GetOwnReports(req));
        }
    }
}