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
    public class TestPlanUserController : BaseController<BusTestPlanUser, PlanUserReq.Page>
    {
        public BusTestPlanUserBll CurBll { get; set; }

        public override BaseBll<BusTestPlanUser, PlanUserReq.Page> Bll => CurBll;

        public ActionResult Export(int planId, int[] userIds)
        {
            var stream = CurBll.Export(out string fileName,planId, userIds);
            return File(stream, "application/pdf",fileName);
        }
        public ActionResult ExportDetails(int planId, int[] userIds)
        {
            var stream = CurBll.ExportDetails(out string fileName, planId, userIds);
            return File(stream, "application/pdf", fileName);
        }
    }
}