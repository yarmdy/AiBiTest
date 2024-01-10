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

            if (!req.IsSpecial)
            {
                query = query.Where(a => a.SpecialType == null);
            }
            else
            {
                query = query.Where(a => a.SpecialType != null);
            }
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
            res.data.BusExampleQuestions = res.data.BusExampleQuestions.OrderBy(a => a.SortNo).ThenBy(a=>a.SortNo2).Select(a => {
                a.Question.BusQuestionOptions = a.Question.BusQuestionOptions.OrderBy(b => b.SortNo).ToList();
                return a;
            }).ToList();
        }

        public Response<BusExample, object, object, object> GetResults(int id) {
            var res = new Response<BusExample, object, object, object>();
            var data = GetFirstOrDefault(a => a.Include("BusExampleResults.Image").Where(b => b.Id == id));

            if (data == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "无法找到相关量表";
            }
            data.BusExampleResults = data.BusExampleResults.OrderBy(a => a.SortNo).ToList();
            res.data = data;
            return res;
        }
        public Response<BusExample, object, object, object> SaveResults(int id,BusExample model)
        {
            var res = new Response<BusExample, object, object, object>();
            var old = GetFirstOrDefault(a => a.Include("BusExampleResults.Image").Where(b => b.Id == id),false);
            if (old==null)
            {
                res.code=EnumResStatus.Fail;
                res.msg = "无法找到相关量表";
            }
            var addResults = model.BusExampleResults.Where(a => a.Id == 0).ToList();
            var editResults = old.BusExampleResults.Where(a=>model.BusExampleResults.Any(b=>b.Id==a.Id)).ToList();
            var delResults = old.BusExampleResults.Where(a=>!model.BusExampleResults.Any(b=>b.Id==a.Id)).ToList();
            addResults.ForEach(a => {
                SysAttachmentBll.Apply(a.ImageId??0);
                a.Example = old;
                a.CreateTime= DateTime.Now;
                a.CreateUserId = CurrentUserId;
                old.BusExampleResults.Add(a);
            });
            editResults.ForEach(a => {
                var newr = model.BusExampleResults.First(b=>b.Id==a.Id);
                if(a.ImageId!=null && a.ImageId != newr.ImageId)
                {
                    SysAttachmentBll.Cancel(a.Image);
                    if (newr.ImageId != null)
                    {
                        SysAttachmentBll.Apply(newr.ImageId??0);
                    }
                }
                if (a.DiffCopy(newr, b => new {b.ImageId,b.Title,b.NContent,b.Code,b.SortNo,b.MaxScore,b.MinScore,b.MaxQuestionNo,b.MinQuestionNo }))
                {
                    a.ModifyTime = DateTime.Now;
                    a.ModifyUserId = CurrentUserId;
                }
            });
            delResults.ForEach(a => {
                SysAttachmentBll.Cancel(a.Image);
                Context.BusExampleResults.Remove(a);
            });

            var ret = Context.SaveChanges();

            if (ret <= 0)
            {
                res.code = EnumResStatus.Fail;
                res.msg = ret==0?"未做任何修改":"修改失败";
            }
            else
            {
                old.BusExampleResults.ToList().ForEach(a => SysAttachmentBll.ApplyFile(a.ImageId ?? 0));
            }

            return res;
        }
        #endregion

        #region 新增
        public override bool AddValidate(out string errorMsg, BusExample model)
        {
            var busExampleOptions = model.BusExampleOptions.ToList();
            var exampleQuestionsAdds = model.BusExampleQuestions.ToList();

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
                SysAttachmentBll.Apply(a.Question.ImageId ?? 0);


                var localOptions = busExampleOptions.Where(b => b.Option.Question.BusExampleQuestions.Any(c => c.SortNo == a.SortNo && c.SortNo2 == a.SortNo2)).ToList();
                localOptions.ForEach(b => {
                    b.Example = model;
                    b.Option = a.Question.BusQuestionOptions.FirstOrDefault(c => c.SortNo == b.Option.SortNo);
                    b.CreateTime = DateTime.Now;
                    b.CreateUserId = CurrentUserId;
                });
            });
            model.QuestionNum = exampleQuestionsAdds.Count;
            return base.AddValidate(out errorMsg, model);
        }
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
            if (res.code != EnumResStatus.Succ) return;
            if (res.data.Image != null)
            {
                SysAttachmentBll.ApplyFile(res.data.Image);
            }
            res.data.BusExampleQuestions.ToList().ForEach(a => {
                if (a.Question.Image != null)
                {
                    SysAttachmentBll.ApplyFile(a.Question.Image);
                }
            });
        }
        #endregion

        #region 修改
        public override bool ModifyValidate(out string errorMsg, BusExample model)
        {
            if (!string.IsNullOrWhiteSpace(model.Keys))
            {
                model.Keys = $"|{model.Keys}|";
            }
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
            
            model.LoadChild(a => new { a=a.BusExampleOptions.ToList(),b=a.BusExampleQuestions.SelectMany(b=>b.Question.BusQuestionOptions).ToList()});
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

                var localOptions = busExampleOptions.Where(b => b.Option.Question.BusExampleQuestions.Any(c => c.SortNo==a.SortNo && c.SortNo2==a.SortNo2)).ToList();
                localOptions.ForEach(b => {
                    b.Example = model;
                    b.Option = a.Question.BusQuestionOptions.FirstOrDefault(c => c.SortNo == b.Option.SortNo);
                    b.CreateTime= DateTime.Now;
                    b.CreateUserId = CurrentUserId;
                    model.BusExampleOptions.Add(b);
                });
                model.BusExampleQuestions.Add(a);
                
            });
            exampleQuestionsEdits.ForEach(a =>
            {
                var newdata = exampleQuestions.First(b=>b.Question.Id==a.QuestionId);
                if (a.DiffCopy(newdata, b => new { b.SortNo,b.SortNo2,b.Duration,b.Prompt,b.PromptMsg}))
                {
                    a.ModifyUserId = CurrentUserId;
                    a.ModifyTime = DateTime.Now;
                }
                if(a.Question.ImageId!=null && newdata.Question.ImageId != a.Question.ImageId)
                {
                    SysAttachmentBll.Cancel(a.Question.ImageId ?? 0);
                }
                if(newdata.Question.ImageId!=null && newdata.Question.ImageId != a.Question.ImageId)
                {
                    SysAttachmentBll.Apply(newdata.Question.ImageId ?? 0);
                }
                if (a.Question.DiffCopy(newdata.Question, b => new { b.ImageId, b.OptionNum, b.Title,b.NContent }))
                {
                    a.Question.ModifyTime= DateTime.Now;
                    a.Question.ModifyUserId= CurrentUserId;
                }

                var newOptions = newdata.Question.BusQuestionOptions.Where(b=>b.Id==0).ToList();
                var modifyOptions = a.Question.BusQuestionOptions.Where(b => newdata.Question.BusQuestionOptions.Any(c => c.Id == b.Id)).ToList();
                var delOptions = a.Question.BusQuestionOptions.Where(b => !newdata.Question.BusQuestionOptions.Any(c => c.Id == b.Id)).ToList();

                var localOptions = busExampleOptions.Where(b => b.Option.Question.BusExampleQuestions.Any(c => c.SortNo == a.SortNo && c.SortNo2==a.SortNo2)).ToList();
                newOptions.ForEach(b => {
                    b.Question = a.Question;
                    b.CreateTime = DateTime.Now;
                    b.CreateUserId = CurrentUserId;
                    b.Remark = "";
                    a.Question.BusQuestionOptions.Add(b);
                });
                modifyOptions.ForEach(b => {
                    var newOption = newdata.Question.BusQuestionOptions.First(c=>c.Id==b.Id);
                    if (b.DiffCopy(newOption, c => new { c.Code, c.SortNo }))
                    {
                        b.ModifyUserId = CurrentUserId;
                        b.ModifyTime = DateTime.Now;
                    }
                    var examOption = model.BusExampleOptions.FirstOrDefault(c=>c.OptionId==b.Id);
                    var newExamOption = localOptions.First(c => c.Option.Id == b.Id);
                    if (examOption.DiffCopy(newExamOption,c=>c.Score))
                    {
                        examOption.ModifyTime = DateTime.Now;
                        examOption.ModifyUserId = CurrentUserId;
                    }
                });
                delOptions.ForEach(b => {
                    var examOption = model.BusExampleOptions.FirstOrDefault(c => c.OptionId == b.Id);
                    Context.BusQuestionOptions.Remove(b);
                    Context.BusExampleOptions.Remove(examOption);
                });

            });
            exampleQuestionsDels.ForEach(a => {
                SysAttachmentBll.Cancel(a.Question.Image);
                var localOptions = model.BusExampleOptions.Where(b => a.Question.BusQuestionOptions.Any(c=>c.Id==b.OptionId)).ToList();
                Context.BusQuestions.Remove(a.Question);
                Context.BusExampleQuestions.Remove(a);
                //Context.BusQuestionOptions.RemoveRange(a.Question.BusQuestionOptions);
                Context.BusExampleOptions.RemoveRange(localOptions);
            });

            model.QuestionNum = model.BusExampleQuestions.Count;
            //errorMsg = "系统停止修改了";
            //return false;
            return true;
        }
        public override void ModifyAfter(Response<BusExample, object, object, object> res, BusExample inModel, BusExample oldModel)
        {
            if (res.code != EnumResStatus.Succ) return;
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
