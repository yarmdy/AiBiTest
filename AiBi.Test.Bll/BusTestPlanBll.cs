using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;

namespace AiBi.Test.Bll
{
    public partial class BusTestPlanBll : BaseBll<BusTestPlan,PlanReq.Page>
    {
        public BusTestTemplateBll BusTestTemplateBll { get; set; }
        public override IQueryable<BusTestPlan> PageWhere(PlanReq.Page req, IQueryable<BusTestPlan> query)
        {
            query = GetIncludeQuery(query, a => new { a.Template,a.CreateUser});
            if(req.Tag+""=="my")
            {
                query = query.Where(a=>a.BusTestPlanUsers.Any(b=>b.UserId==CurrentUserId));
            }
            return base.PageWhere(req, query);
        }
        public override void PageAfter(PlanReq.Page req, Response<List<BusTestPlan>, object, object, object> res)
        {
            res.data.ForEach(a=>a.LoadChild(b=>new { CreateUserName = b.CreateUser.Name}));
        }
        public override void DetailAfter(int id, int? id2, Response<BusTestPlan, object, object, object> res)
        {
            if(Tag+""== "test")
            {
                res.data.LoadChild(a => new { a.Template.Image ,Image2 = a.Template.BusTestTemplateExamples.SelectMany(b=>b.Example.BusExampleQuestions.Select(c=>c.Question.Image)).ToList(),options = a.Template.BusTestTemplateExamples.SelectMany(b=>b.Example.BusExampleQuestions.SelectMany(c=>c.Question.BusQuestionOptions)).ToList()});
                res.data.BusTestPlanUsers = res.data.BusTestPlanUsers.Where(a => a.UserId == CurrentUserId).ToList();
            }
            else
            {
                res.data.LoadChild(a => new { a.Template.Image, Avatars = a.BusTestPlanUsers.Select(b => b.User.BusUserInfoUsers.Where(c => c.OwnerId == res.data.CreateUserId).FirstOrDefault()).ToList() });
            }
            
        }
        public override Expression<Func<BusTestPlan, object>> ModifyExcepts(BusTestPlan model)
        {
            return a => new { a.Status,a.CanPause,a.UserNum,a.ExampleNum,a.QuestionNum};
        }
        public override bool AddValidate(out string errorMsg, BusTestPlan model)
        {
            errorMsg = "";
            var temp = BusTestTemplateBll.GetMyList(new TestTemplateReq.Page {Id=model.Id }).data.FirstOrDefault();
            if (temp == null)
            {
                errorMsg = "未找到任务分类";
                return false;
            }

            var addUsers = model.BusTestPlanUsers.ToList();
            addUsers.ForEach(a => {
                a.Plan = model; 
                a.CreateTime = DateTime.Now;
                a.CreateUserId = CurrentUserId;
            });

            model.UserNum = model.BusTestPlanUsers.Count;
            
            model.CanPause = temp.CanPause;
            model.ExampleNum = temp.ExampleNum;
            model.QuestionNum = temp.QuestionNum;

            return true;
        }
        public override bool ModifyValidate(out string errorMsg, BusTestPlan model)
        {
            errorMsg = "";
            var temp = BusTestTemplateBll.GetMyList(new TestTemplateReq.Page { Id = model.TemplateId }).data.FirstOrDefault();
            if (temp == null)
            {
                errorMsg = "未找到任务分类";
                return false;
            }
            return true;
        }
        public override bool ModifyBefore(out string errorMsg, BusTestPlan model, BusTestPlan inModel, BusTestPlan oldModel)
        {
            errorMsg = "";

            var users = model.BusTestPlanUsers.ToList();
            var addUsers = inModel.BusTestPlanUsers.Where(a => !users.Any(b => b.UserId == a.UserId)).ToList();
            var editUsers = model.BusTestPlanUsers.Where(a => inModel.BusTestPlanUsers.Any(b => b.UserId == a.UserId)).ToList();
            var deleteUsers = model.BusTestPlanUsers.Where(a => !inModel.BusTestPlanUsers.Any(b => b.UserId == a.UserId)).ToList();
            addUsers.ForEach(a => {
                model.BusTestPlanUsers.Add(new BusTestPlanUser { Plan=model,UserId=a.UserId, CreateTime = DateTime.Now, CreateUserId = CurrentUserId });
            });
            editUsers.ForEach(a => {
                var newt = inModel.BusTestPlanUsers.First(b => b.UserId == a.UserId);
                if (a.DiffCopy(newt, b => new { b.UserId }))
                {
                    a.ModifyTime = DateTime.Now;
                    a.ModifyUserId = CurrentUserId;
                }
            });
            deleteUsers.ForEach(a =>
            {
                Context.BusTestPlanUsers.Remove(a);
            });

            model.UserNum = inModel.BusTestPlanUsers.Count;
            var temp = BusTestTemplateBll.Find(model.TemplateId);
            model.CanPause = temp.CanPause;
            model.ExampleNum = temp.ExampleNum;
            model.QuestionNum = temp.QuestionNum;

            return true;
        }

        public Response<List<BusTestPlan>, object, object, object> GetMyList(PlanReq.Page req)
        {
            req.Tag = "my";
            return GetPageList(req);
        }

        public Response<BusTestPlan, object, object, object> GetTest(int id)
        {
            Tag = "test";
            return GetDetail(id,null);
        }

        public Response<int,object,object,object> Answer(int id, List<BusTestPlanUserOption> list)
        {
            var res = new Response<int,object,object,object>();
            if(list==null)
            {
                list=new List<BusTestPlanUserOption>();
            }
            var plan = Find(false, id);
            if (plan == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "未找到测试任务";
                return res;
            }
            var planUser = plan.BusTestPlanUsers.FirstOrDefault(a=>a.UserId== CurrentUserId);
            if (planUser == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您无法参加此次测试";
                return res;
            }
            if (planUser.Status == (int)EnumPlanUserStatus.Finish)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您已经答完了";
                return res;
            }
            var optionDic = plan.Template.BusTestTemplateExamples.SelectMany(a => a.Example.BusExampleOptions).ToDictionary(a=>a.OptionId);
            var myExam = plan.BusTestPlanUserExamples.Where(a=>a.UserId==CurrentUserId).ToList();
            var questions = plan.Template.BusTestTemplateExamples.OrderBy(a=>a.SortNo).SelectMany(a => a.Example.BusExampleQuestions.OrderBy(b=>b.SortNo).ThenBy(b=>b.SortNo2)).ToList();
            var questionsDic = questions.GroupBy(a => a.ExampleId).ToDictionary(a => a.Key, a => a.OrderBy(b => b.SortNo).ThenBy(b => b.SortNo2).ToList());
            var oldOptions = plan.BusTestPlanUserOptions.Where(a => a.UserId == CurrentUserId).ToList();
            oldOptions
                .Where(a => list.Any(b => a.ExampleId == b.ExampleId && a.QuestionId == b.QuestionId) && !list.Any(b=> a.OptionId == b.OptionId))
                .ToList()
                .ForEach(a => Context.BusTestPlanUserOptions.Remove(a));
            list.Where(a => !oldOptions.Any(b => a.ExampleId == b.ExampleId && a.QuestionId == b.QuestionId && a.OptionId == b.OptionId))
                .ToList().ForEach(a => {
                    a.PlanId = id;
                    a.UserId = CurrentUserId;
                    a.CreateUserId= CurrentUserId;
                    a.CreateTime = DateTime.Now;
                    plan.BusTestPlanUserOptions.Add(a);
                });
            
            planUser.CurrentExample = list.LastOrDefault()?.ExampleId?? planUser.CurrentExample;
            planUser.CurrentQuestion = list.LastOrDefault()?.QuestionId?? planUser.CurrentQuestion;
            planUser.Duration = planUser.Duration + (int)(DateTime.Now-planUser.ModifyTime.Value).TotalSeconds;
            if(planUser.CurrentExample!=null && planUser.CurrentQuestion != null)
            {
                planUser.FinishQuestion = questions.FindIndex(a=>a.QuestionId==planUser.CurrentQuestion)+1;
                var index = questions.FindIndex(a => a.ExampleId == planUser.CurrentExample && a.QuestionId == planUser.CurrentQuestion);
                var tempCount = questions.Take(index+1).GroupBy(a=>a.ExampleId).Count();
                if(questions.Skip(index+1).Any(a=>a.ExampleId== planUser.CurrentExample))
                {
                    tempCount -= 1;
                }
                planUser.FinishExample = tempCount;
            }

            res.data = planUser.Duration;

            if (planUser.CurrentExample != null)
            {
                var exam = myExam.FirstOrDefault(a=>a.UserId==CurrentUserId && a.ExampleId==planUser.CurrentExample.Value);
                if (exam == null)
                {
                    exam = new BusTestPlanUserExample() {
                        CreateTime = DateTime.Now,
                        CreateUserId = CurrentUserId,
                        PlanId = id,
                        UserId = CurrentUserId,
                        ExampleId = planUser.CurrentExample.Value,
                        BeginTime = DateTime.Now,
                    };
                    plan.BusTestPlanUserExamples.Add(exam);
                    myExam.Add(exam);
                }
                else
                {
                    exam.ModifyTime = DateTime.Now;
                    exam.ModifyUserId = CurrentUserId;
                }
                exam.CurrentQuestion = planUser.CurrentQuestion;
                if (exam.CurrentQuestion != null && planUser.CurrentExample!=null) {
                    exam.FinishQuestion = questionsDic.G(planUser.CurrentExample.Value).FindIndex(a=>a.QuestionId== planUser.CurrentQuestion)+1;
                } 
                exam.Score += list.Sum(a=>optionDic.G(a.OptionId)?.Score??0);
                
            }
            planUser.Score = string.Join("|", myExam.OrderBy(a=>a.BeginTime).Select(a => a.Score));
            planUser.ModifyTime = DateTime.Now;
            planUser.ModifyUserId = CurrentUserId;
            planUser.Status = (int)EnumPlanUserStatus.Answer;
            Context.SaveChanges();
            return res;
        }
        public Response<int, object, object, object> StartAnswer(int id)
        {
            var res = new Response<int, object, object, object>();
            var plan = Find(false,id);
            if (plan == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "未找到测试任务";
                return res;
            }
            var planUser = plan.BusTestPlanUsers.FirstOrDefault(a => a.UserId == CurrentUserId);
            if (planUser == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您无法参加此次测试";
                return res;
            }
            if(planUser.Status == (int)EnumPlanUserStatus.Finish)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您已经答完了";
                return res;
            }
            planUser.BeginTime = DateTime.Now;
            planUser.ModifyTime = DateTime.Now;
            planUser.ModifyUserId= CurrentUserId;
            Context.SaveChanges();
            res.data = planUser.Duration;
            return res;
        }
        public Response<int, object, object, object> EndAnswer(int id)
        {
            var res = new Response<int, object, object, object>();
            var plan = Find(false, id);
            if (plan == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "未找到测试任务";
                return res;
            }
            var planUser = plan.BusTestPlanUsers.FirstOrDefault(a => a.UserId == CurrentUserId);
            if (planUser == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您无法参加此次测试";
                return res;
            }
            if (planUser.Status == (int)EnumPlanUserStatus.Finish)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您已经答完了";
                return res;
            }
            planUser.EndTime = DateTime.Now;
            planUser.Status = (int)EnumPlanUserStatus.Finish;
            planUser.ModifyTime = DateTime.Now;
            planUser.ModifyUserId = CurrentUserId;
            Context.SaveChanges();
            return res;
        }
    }
}
