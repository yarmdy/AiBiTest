using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;

namespace AiBi.Test.Bll
{
    public partial class BusExampleBll : BaseBll<BusExample, ExampleReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        #region 重写
        public override IQueryable<BusExample> PageWhere(ExampleReq.Page req, IQueryable<BusExample> query)
        {
            query = base.PageWhere(req, query);
            if (req.ClassifyId != null)
            {
                query = query.Where(a => a.ClassifyId == req.ClassifyId);
            }
            if (req.SubClassifyId != null)
            {
                query = query.Where(a => a.SubClassifyId == req.SubClassifyId);
            }
            query = GetIncludeQuery(query, a => new { a.Image });
            return query;
        }
        #endregion

    }
}
