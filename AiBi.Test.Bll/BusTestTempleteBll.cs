using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;

namespace AiBi.Test.Bll
{
    public partial class BusTestTempleteBll : BaseBll<BusTestTemplete,TestTempleteReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        public override IQueryable<BusTestTemplete> PageWhere(TestTempleteReq.Page req, IQueryable<BusTestTemplete> query)
        {
            if (req.ClassifyId!=null) {
                query = query.Where(a=>a.ClassifyId==req.ClassifyId);
            }
            if (req.SubClassifyId != null)
            {
                query = query.Where(a=>a.SubClassifyId==req.SubClassifyId);
            }
            var userId =  SysUserBll.GetCookie().Id;
            if (req.Tag + "" == "my")
            {
                var classIds = BusUserClassifyBll.GetListFilter(a=>a.Where(b=>b.UserId==userId)).Select(a=>a.ClassifyId).ToArray();
                return query.Where(a => a.BusUserTestTempletes.Any(b=>b.UserId== userId) || classIds.Contains(a.ClassifyId) || classIds.Contains(a.SubClassifyId.Value));
            }
            query = getIncludeQuery(query, a => new { a.Image});
            return query;
        }
        public Response<List<BusTestTemplete>, object, object, object> GetMyList(TestTempleteReq.Page req)
        {
            req.Tag = "my";
            return base.GetPageList(req);
        }
    }
}
