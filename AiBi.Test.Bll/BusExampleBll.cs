using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace AiBi.Test.Bll
{
    public partial class BusExampleBll : BaseBll<BusExample, ExampleReq.Page>
    {
        public BusUserClassifyBll BusUserClassifyBll { get; set; }
        public SysAttachmentBll SysAttachmentBll { get; set; }
        #region 重写

        #region 查询
        public override Expression<Func<BusExample, bool>> PageWhereKeyword(ExampleReq.Page req)
        {
            return base.PageWhereKeyword(req).Or(a=>a.Classify.Name.Contains(req.keyword) || a.SubClassify.Name.Contains(req.keyword));
        }
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
            query = GetIncludeQuery(query, a => new { a.Image, a.SubClassify, a.Classify });
            return query;
        }

        public override void PageAfter(ExampleReq.Page req, Response<List<BusExample>, object, object, object> res)
        {
            res.data.ForEach(a => a.Keys = a.ShowKeys);
        }
        public override void DetailAfter(int id, int? id2, Response<BusExample, object, object, object> res)
        {
            res.data.Keys = res.data.ShowKeys;
            res.data.LoadChild(a => new { a.Image,data2 =  a.BusExampleOptions.ToList(),data3 = a.BusExampleQuestions.SelectMany(b=>b.Question.BusQuestionOptions).ToList(),data4=a.BusExampleQuestions.Select(b=>b.Question.Image).ToList() });
            res.data.BusExampleQuestions = res.data.BusExampleQuestions.OrderBy(a => a.SortNo).Select(a => {
                a.Question.BusQuestionOptions = a.Question.BusQuestionOptions.OrderBy(b => b.SortNo).ToList();
                return a;
            }).ToList();
        }
        #endregion

        #region 新增
        public override bool AddBefore(out string errorMsg, BusExample model, BusExample inModel)
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
            return true;
        }
        public override void AddAfter(Response<BusExample, object, object, object> res, BusExample inModel)
        {
            if (res.data.Image != null)
            {
                SysAttachmentBll.ApplyFile(res.data.Image);
            }

        }
        #endregion

        #region 修改
        public override bool ModifyValidate(out string errorMsg, BusExample model)
        {
            return base.ModifyValidate(out errorMsg, model);
        }
        public override bool ModifyBefore(out string errorMsg, BusExample model, BusExample inModel, BusExample oldModel)
        {
            errorMsg = "";

            if (model.ImageId != null)
            {
                SysAttachmentBll.Apply(model.ImageId.Value);
            }
            if (oldModel.ImageId != null && model.ImageId != oldModel.ImageId)
            {
                SysAttachmentBll.Cancel(oldModel.ImageId.Value);
            }
            if (!string.IsNullOrWhiteSpace(model.Keys))
            {
                model.Keys = $"|{model.Keys}|";
            }

            var exampleQuestions = inModel.BusExampleQuestions.ToList();
            var busExampleOptions = inModel.BusExampleOptions.ToList();
            var exampleQuestionsDels = model.BusExampleQuestions.Where(a => !exampleQuestions.Any(b => b.Question.Id == a.QuestionId)).ToList();
            var exampleQuestionsEdits = model.BusExampleQuestions.Where(a => exampleQuestions.Any(b => b.Question.Id == a.QuestionId)).ToList();
            var exampleQuestionsAdds = exampleQuestions.Where(a=>a.Question.Id==0).ToList();

            exampleQuestionsAdds.ForEach(a => {
                a.CreateTime = DateTime.Now;
                a.CreateUserId = CurrentUserId;
                a.Example = model;
                a.Question.CreateTime = DateTime.Now;
                a.Question.CreateUserId = CurrentUserId;
                a.Question.BusQuestionOptions.ToList().ForEach(b => { 
                    b.CreateUserId = CurrentUserId;
                    b.CreateTime = DateTime.Now;
                    b.Question = a.Question;
                    b.Remark = "";
                });
                SysAttachmentBll.Apply(a.Question.ImageId??0);

                var localOptions = busExampleOptions.Where(b => b.Option.Question.BusExampleQuestions.Any(c => c.SortNo==a.SortNo)).ToList();
                localOptions.ForEach(b => {
                    b.Example = model;
                    b.Option = a.Question.BusQuestionOptions.FirstOrDefault(c => c.SortNo == b.Option.SortNo);
                    b.CreateTime= DateTime.Now;
                    b.CreateUserId = CurrentUserId;
                    model.BusExampleOptions.Add(b);
                });
                model.BusExampleQuestions.Add(a);
                
            });

            return true;
        }
        public override void ModifyAfter(Response<BusExample, object, object, object> res, BusExample inModel, BusExample oldModel)
        {
            if (res.data.Image != null)
            {
                SysAttachmentBll.ApplyFile(res.data.Image);
            }
            res.data.BusExampleQuestions.ToList().ForEach(a =>{
                if (a.Question.Image != null)
                {
                    SysAttachmentBll.ApplyFile(a.Question.Image);
                }
            });
        }
        public override Expression<Func<BusExample, object>> ModifyExcepts(BusExample model)
        {
            return a => new { a.QuestionNum, a.Status };
        }
        #endregion

        #endregion

    }
}
