﻿using System;
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
            query = GetIncludeQuery(query, a => new { a.Template,a.CreateUser});
            return base.PageWhere(req, query);
        }
        public override void PageAfter(PlanReq.Page req, Response<List<BusTestPlan>, object, object, object> res)
        {
            res.data.ForEach(a=>a.LoadChild(b=>new { CreateUserName = b.CreateUser.Name}));
        }

        public override void DetailAfter(int id, int? id2, Response<BusTestPlan, object, object, object> res)
        {
            res.data.LoadChild(a => new { a.Template, a.Template.Image});
        }
    }
}
