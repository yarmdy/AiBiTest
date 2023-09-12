using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;

namespace AiBi.Test.Bll
{
    public partial class BusTestTemplateBll : BaseBll<BusTestTemplate,TestTemplateReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        #region 重写
        public override IQueryable<BusTestTemplate> PageWhere(TestTemplateReq.Page req, IQueryable<BusTestTemplate> query)
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
            var userId = CurrentUserId;
            if (req.Tag + "" == "my")
            {
                var classIds = BusUserClassifyBll.GetListFilter(a => a.Where(b => b.UserId == userId)).Select(a => a.ClassifyId).ToArray();
                return query.Where(a => a.BusUserTestTemplates.Any(b => b.UserId == userId) || classIds.Contains(a.ClassifyId) || classIds.Contains(a.SubClassifyId.Value));
            }
            query = GetIncludeQuery(query, a => new { a.Image });
            return query;
        }
        #endregion

        public Response<List<BusTestTemplate>, object, object, object> GetMyList(TestTemplateReq.Page req)
        {
            req.Tag = "my";
            return base.GetPageList(req);
        }


    }
}
