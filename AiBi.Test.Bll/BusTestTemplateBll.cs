using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.Web.UI;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AiBi.Test.Bll
{
    public partial class BusTestTemplateBll : BaseBll<BusTestTemplate,TestTemplateReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        public SysAttachmentBll SysAttachmentBll { get; set; }
        public BusExampleBll BusExampleBll { get; set; }
        public BusUserTestTemplateBll BusUserTestTemplateBll { get; set; }
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
            if (req.Id.HasValue)
            {
                query = query.Where(a => a.Id == req.Id);
            }
            var userId = CurrentUserId;
            if (req.Tag + "" == "my")
            {
                var classIds = BusUserClassifyBll.GetListFilter(a => a.Where(b => b.UserId == userId)).Select(a => a.ClassifyId).ToArray();
                query = query.Where(a => a.BusUserTestTemplates.Any(b => b.UserId == userId) || classIds.Contains(a.ClassifyId.Value) || classIds.Contains(a.SubClassifyId.Value));
            }
            if (req.CanUseUserId.HasValue)
            {
                var classIds = BusUserClassifyBll.GetListFilter(a => a.Where(b => b.UserId == req.CanUseUserId)).Select(a => a.ClassifyId).ToArray();
                query = query.Where(a => a.BusUserTestTemplates.Any(b => b.UserId == req.CanUseUserId) || classIds.Contains(a.ClassifyId.Value) || classIds.Contains(a.SubClassifyId.Value));
            }
            query = GetIncludeQuery(query, a => new { a.Image });
            if (req.ExampleType != null)
            {
                query = query.Where(a => a.ExampleType == (int)req.ExampleType);
            }
            
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

            if (model.ImageId != null && model.ImageId!=oldModel.ImageId)
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
                model.BusTestTemplateExamples.Add(new BusTestTemplateExample { Template=model,ExampleId=a.ExampleId,SortNo=a.SortNo,Duration=a.Duration,CanPause=a.CanPause,Enabled = a.Enabled,CreateTime=DateTime.Now,CreateUserId=CurrentUserId});
            });
            editExamples.ForEach(a => {
                var newt = inModel.BusTestTemplateExamples.First(b=>b.ExampleId==a.ExampleId);
                if (a.DiffCopy(newt, b => new {b.Duration, b.SortNo, b.CanPause,b.Enabled }))
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
        public Response<List<BusTestTemplate>, object, object, object> GetItsList(TestTemplateReq.Page req)
        {
            req.CanUseUserId = req.CanUseUserId ?? 0;
            return base.GetPageList(req);
        }


        public Response<int> AddToIt(int id, int[] ids)
        {
            var res=new Response<int>();
            if(ids==null || ids.Length <= 0)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "参数错误";
                return res;
            }
            var old = BusUserTestTemplateBll.GetListFilter(a=>a.Where(b=>b.UserId==id),null,false);
            ids.ToList().ForEach(a => {
                if (old.Any(b => b.TemplateId == a))
                {
                    return;
                }
                Context.BusUserTestTemplates.Add(new BusUserTestTemplate { UserId=id,TemplateId=a,CreateUserId=CurrentUserId,CreateTime=DateTime.Now});
            });
            var ret = Context.SaveChanges();
            if (ret < 0)
            {
                res.code=EnumResStatus.Fail;
                res.msg = "添加失败";
            }else if (ret == 0)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "没有添加任何数据";
            }
            return res;
        }
        public Response<int> Remove(int id, int[] ids)
        {
            var res = new Response<int>();
            if (ids == null || ids.Length <= 0)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "参数错误";
                return res;
            }
            var old = BusUserTestTemplateBll.GetListFilter(a => a.Where(b => b.UserId == id), null, false);
            ids.ToList().ForEach(a => {
                var oldd = old.FirstOrDefault(b => b.TemplateId == a);
                if (oldd == null)
                {
                    return;
                }
                Context.BusUserTestTemplates.Remove(oldd);
            });
            var ret = Context.SaveChanges();
            if (ret < 0)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "移除失败";
            }
            else if (ret == 0)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "没有移除任何数据";
            }
            return res;
        }
    }
}
