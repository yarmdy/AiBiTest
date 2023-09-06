using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;

namespace AiBi.Test.Bll
{
    public partial class BusTestPlanBll : BaseBll<BusTestPlan,PlanReq.Page>
    {
        public override IQueryable<BusTestPlan> PageWhere(PlanReq.Page req, IQueryable<BusTestPlan> query)
        {
            return getIncludeQuery(query, a => new { a.Templete});
        }
        public override void PageAfter(PlanReq.Page req, Response<List<BusTestPlan>, object, object, object> res)
        {
            
        }
    }
}
