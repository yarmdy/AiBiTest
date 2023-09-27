using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.Web.UI;
using System.Linq.Expressions;

namespace AiBi.Test.Bll
{
    public partial class BusTestTemplateBll : BaseBll<BusTestTemplate,TestTemplateReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        public SysAttachmentBll SysAttachmentBll { get; set; }
        public BusExampleBll BusExampleBll { get; set; }
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
                return query.Where(a => a.BusUserTestTemplates.Any(b => b.UserId == userId) || classIds.Contains(a.ClassifyId.Value) || classIds.Contains(a.SubClassifyId.Value));
            }
            query = GetIncludeQuery(query, a => new { a.Image });
            return query;
        }
        public override void PageAfter(TestTemplateReq.Page req, Response<List<BusTestTemplate>, object, object, object> res)
        {
            res.data.ForEach(a => a.Keys = a.ShowKeys);
        }
        #endregion

        public override void DetailAfter(int id, int? id2, Response<BusTestTemplate, object, object, object> res)
        {
            res.data.Keys = res.data.ShowKeys;
            res.data.LoadChild(a => new { a.Image, Examples = a.BusTestTemplateExamples.Select(b=>b.Example).ToList()});
            res.data.BusTestTemplateExamples = res.data.BusTestTemplateExamples.OrderBy(a => a.SortNo).ToList();
        }

        public override bool AddBefore(out string errorMsg, BusTestTemplate model, BusTestTemplate inModel)
        {
            errorMsg = "";

            if (model.ImageId != null)
            {
                SysAttachmentBll.Apply(model.ImageId.Value);
            }
            if (!string.IsNullOrWhiteSpace(model.Keys))
            {
                model.Keys = $"|{model.Keys}|";
            }
            var examples = model.BusTestTemplateExamples.ToList();
            var exampleModels = BusExampleBll.ByIds(inModel.BusTestTemplateExamples.Select(a => a.ExampleId).ToArray());
            var addExamples = examples.ToList();
            
            addExamples.ForEach(a => {
                a.Template = model;
                a.ExampleId = a.ExampleId;
                a.SortNo = a.SortNo;
                a.Duration = a.Duration;
                a.CanPause = a.CanPause;
                a.CreateTime = DateTime.Now;
                a.CreateUserId = CurrentUserId;
            });
            model.ExampleNum = inModel.BusTestTemplateExamples.Count;

            model.QuestionNum = exampleModels.Sum(a => a.QuestionNum);
            model.Duration = exampleModels.Sum(a => a.Duration);

            return true;
        }
        public override void AddAfter(Response<BusTestTemplate, object, object, object> res, BusTestTemplate inModel)
        {
            if (res.code != EnumResStatus.Succ) return;
            if (res.data.Image != null)
            {
                SysAttachmentBll.ApplyFile(res.data.Image);
            }
        }
        public override Expression<Func<BusTestTemplate, object>> ModifyExcepts(BusTestTemplate model)
        {
            return a => new { a.ExampleNum,a.QuestionNum,a.Duration};
        }
        public override bool ModifyBefore(out string errorMsg, BusTestTemplate model, BusTestTemplate inModel, BusTestTemplate oldModel)
        {
            errorMsg = "";

            if (model.ImageId != null && model.ImageId!=inModel.ImageId)
            {
                SysAttachmentBll.Apply(model.ImageId.Value);
            }
            if (oldModel.ImageId != null && model.ImageId != oldModel.ImageId)
            {
                SysAttachmentBll.Cancel(oldModel.ImageId.Value);
            }
            var examples = model.BusTestTemplateExamples.ToList();
            var exampleModels = BusExampleBll.ByIds(inModel.BusTestTemplateExamples.Select(a => a.ExampleId).ToArray());
            var addExamples = inModel.BusTestTemplateExamples.Where(a=>!examples.Any(b=>b.ExampleId==a.ExampleId)).ToList();
            var editExamples = model.BusTestTemplateExamples.Where(a => inModel.BusTestTemplateExamples.Any(b => b.ExampleId == a.ExampleId)).ToList();
            var deleteExamples = model.BusTestTemplateExamples.Where(a => !inModel.BusTestTemplateExamples.Any(b => b.ExampleId == a.ExampleId)).ToList();
            addExamples.ForEach(a => { 
                model.BusTestTemplateExamples.Add(new BusTestTemplateExample { Template=model,ExampleId=a.ExampleId,SortNo=a.SortNo,Duration=a.Duration,CanPause=a.CanPause,CreateTime=DateTime.Now,CreateUserId=CurrentUserId});
            });
            editExamples.ForEach(a => {
                var newt = inModel.BusTestTemplateExamples.First(b=>b.ExampleId==a.ExampleId);
                if (a.DiffCopy(newt, b => new {b.Duration, b.SortNo, b.CanPause }))
                {
                    a.ModifyTime = DateTime.Now;
                    a.ModifyUserId = CurrentUserId;
                }
            });
            deleteExamples.ForEach(a =>
            {
                Context.BusTestTemplateExamples.Remove(a);
            });

            model.ExampleNum = inModel.BusTestTemplateExamples.Count;
            
            model.QuestionNum = exampleModels.Sum(a => a.QuestionNum);
            model.Duration = exampleModels.Sum(a => a.Duration);

            return true;
        }
        public override void ModifyAfter(Response<BusTestTemplate, object, object, object> res, BusTestTemplate inModel, BusTestTemplate oldModel)
        {
            if (res.code != EnumResStatus.Succ) return;
            if (res.data.Image != null)
            {
                SysAttachmentBll.ApplyFile(res.data.Image);
            }
            
        }

        public Response<List<BusTestTemplate>, object, object, object> GetMyList(TestTemplateReq.Page req)
        {
            req.Tag = "my";
            return base.GetPageList(req);
        }


    }
}
