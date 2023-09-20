using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace AiBi.Test.Bll
{
    public partial class BusExampleBll : BaseBll<BusExample, ExampleReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        public SysAttachmentBll SysAttachmentBll { get; set; }
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
            query = GetIncludeQuery(query, a => new { a.Image,a.SubClassify,a.Classify });
            return query;
        }

        public override void PageAfter(ExampleReq.Page req, Response<List<BusExample>, object, object, object> res)
        {
            res.data.ForEach(a => a.Keys = a.ShowKeys);
        }
        public override void DetailAfter(int id, int? id2, Response<BusExample, object, object, object> res)
        {
            res.data.Keys = res.data.ShowKeys;
            res.data.LoadChild(a => new { a.Image });
        }

        public override bool ModifyBefore(out string errorMsg, BusExample model, BusExample inModel, BusExample oldModel)
        {
            errorMsg = "";
            
            if (model.ImageId!=null)
            {
                SysAttachmentBll.Apply(model.ImageId.Value);
            }
            if(oldModel.ImageId!=null && model.ImageId != oldModel.ImageId)
            {
                SysAttachmentBll.Cancel(oldModel.ImageId.Value);
            }
            return true;
        }
        public override void ModifyAfter(Response<BusExample, object, object, object> res, BusExample inModel, BusExample oldModel)
        {
            if (res.data.Image != null)
            {
                SysAttachmentBll.ApplyFile(res.data.Image);
            }
        }
        #endregion

    }
}
