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
using System.Web;

namespace AiBi.Test.Bll
{
    public partial class BusTestPlanBll : BaseBll<BusTestPlan,PlanReq.Page>
    {
        public BusTestTemplateBll BusTestTemplateBll { get; set; }
        public BusExampleBll BusExampleBll { get; set; }
        public override IQueryable<BusTestPlan> PageWhere(PlanReq.Page req, IQueryable<BusTestPlan> query)
        {
            query = GetIncludeQuery(query, a => new { a.Template, a.CreateUser });
            if (req.Tag + "" == "report")
            {
                query = query.Include("BusTestPlanUsers");
            }
            if(req.Tag+""== "ownreport")
            {
                query = query.Include("BusTestPlanUsers");
                query = query.Where(a => a.CreateUserId == CurrentUserId);
            }
            if (req.Tag + "" == "own")
            {
                query = query.Where(a=>a.CreateUserId==CurrentUserId);
            }
            if(req.Tag+""=="my")
            {
                var now = DateTime.Now;
                query = query.Where(a=>a.BusTestPlanUsers.Any(b=>b.UserId==CurrentUserId) && a.StartTime<= now && a.EndTime>= now);
            }
            return base.PageWhere(req, query);
        }
        public override void PageAfter(PlanReq.Page req, Response<List<BusTestPlan>, object, object, object> res)
        {
            var planUserBll = AutofacExt.GetService<BusTestPlanUserBll>();
            if (req.Tag + "" == "my")
            {
                var planIds = res.data.Select(a => a.Id).ToArray();
                var planUserDic = planUserBll.GetListFilter(a => a.Where(b => planIds.Contains(b.PlanId) && b.UserId == CurrentUserId)).GroupBy(a=>a.PlanId).ToDictionary(a=>a.Key,a=>a.ToList());
                res.data.ForEach(a => {
                    a.BusTestPlanUsers = planUserDic.G(a.Id,new List<BusTestPlanUser>());
                });
            }
        }
        public override void DetailAfter(int id, int? id2, Response<BusTestPlan, object, object, object> res)
        {
            var uid = res.data.CreateUserId;
            if (Tag+""== "test")
            {
                res.data.LoadChild(a => new { a.Template.Image ,Image2 = a.Template.BusTestTemplateExamples.SelectMany(b=>b.Example.BusExampleQuestions.Select(c=>c.Question.Image)).ToList(),options = a.Template.BusTestTemplateExamples.SelectMany(b=>b.Example.BusExampleQuestions.SelectMany(c=>c.Question.BusQuestionOptions)).ToList()});
                res.data.BusTestPlanUsers = res.data.BusTestPlanUsers.Where(a => a.UserId == CurrentUserId).ToList();
            }
            else if (Tag + "" == "report")
            {
                res.data.LoadChild(a => new { a.Template.Image,Examples = a.Template.BusTestTemplateExamples.Select(b=>b.Example), Avatars = a.BusTestPlanUsers.Select(b => b.User.BusUserInfoUsers.Where(c => c.OwnerId == uid).FirstOrDefault()).ToList() });
                res.data.BusTestPlanUsers = res.data.BusTestPlanUsers.OrderBy(a => a.EndTime ?? DateTime.Parse("2099-12-31")).ThenBy(a => a.FinishQuestion).ToList();
                var ids = res.data.BusTestPlanUsers.Select(a => a.UserId).ToArray();
                
                var userinfos = Context.BusUserInfos.Where(a=>ids.Contains(a.UserId) && a.OwnerId== uid).ToList();
                res.data.BusTestPlanUsers.ToList().ForEach(a => a.User.BusUserInfoUsers = new List<BusUserInfo> { userinfos.FirstOrDefault(b => b.UserId == a.UserId) });
            }
            else
            {
                res.data.LoadChild(a => new { a.Template.Image, Avatars = a.BusTestPlanUsers.Select(b => b.User.BusUserInfoUsers.Where(c => c.OwnerId == uid).FirstOrDefault()).ToList() });
            }
            
        }
        public override Expression<Func<BusTestPlan, object>> ModifyExcepts(BusTestPlan model)
        {
            return a => new { a.Status,a.CanPause,a.UserNum,a.ExampleNum,a.QuestionNum};
        }
        public override bool AddValidate(out string errorMsg, BusTestPlan model)
        {
            errorMsg = "";
            var temp = BusTestTemplateBll.GetMyList(new TestTemplateReq.Page {Id=model.TemplateId }).data.FirstOrDefault();
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
        public Response<List<BusTestPlan>, object, object, object> GetReports(PlanReq.Page req)
        {
            req.Tag = "report";
            return GetPageList(req);
        }

        public Response<List<BusTestPlan>, object, object, object> GetOwnList(PlanReq.Page req)
        {
            req.Tag = "own";
            return GetPageList(req);
        }
        public Response<List<BusTestPlan>, object, object, object> GetOwnReports(PlanReq.Page req)
        {
            req.Tag = "ownreport";
            return GetPageList(req);
        }

        public Response<BusTestPlan, object, object, object> GetReport(int id)
        {
            Tag = "report";
            return GetDetail(id, null);
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
            if (planUser.Status != (int)EnumPlanUserStatus.Answer)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "非法操作";
                return res;
            }
            if (plan.CanPause)
            {
                planUser.Duration = planUser.Duration + (int)(DateTime.Now - planUser.ModifyTime.Value).TotalSeconds;
            }else if (!plan.CanPause)
            {
                planUser.Duration = (int)(DateTime.Now - planUser.BeginTime.Value).TotalSeconds;
            }
            if (planUser.Duration >= plan.Template.Duration*60)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "已超时，无法继续测试";
                planUser.Status = (int)EnumPlanUserStatus.Finish;
                Context.SaveChanges();
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
            var newOptions = oldOptions
                .Where(a => !list.Any(b => a.ExampleId == b.ExampleId && a.QuestionId == b.QuestionId))
                .ToList();
            newOptions.AddRange(list.Where(a => !oldOptions.Any(b => a.ExampleId == b.ExampleId && a.QuestionId == b.QuestionId && a.OptionId == b.OptionId))
                .ToList());

            planUser.CurrentExample = list.LastOrDefault()?.ExampleId?? planUser.CurrentExample;
            planUser.CurrentQuestion = list.LastOrDefault()?.QuestionId?? planUser.CurrentQuestion;
            
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
                exam.Score = newOptions.Sum(a=>optionDic.G(a.OptionId)?.Score??0);
                
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
            if (planUser.BeginTime == null)
            {
                planUser.BeginTime = DateTime.Now;
            }
            if (!plan.CanPause)
            {
                planUser.Duration = (int)(DateTime.Now - planUser.BeginTime.Value).TotalSeconds;
            }


            planUser.ModifyTime = DateTime.Now;
            planUser.ModifyUserId= CurrentUserId;
            planUser.Status = (int)EnumPlanUserStatus.Answer;

            if (planUser.Duration >= plan.Template.Duration*60)
            {
                planUser.Status = (int)EnumPlanUserStatus.Finish;
                res.code= EnumResStatus.Fail;
                res.msg = "您已超时，无法继续测试";
            }
            Context.SaveChanges();
            res.data = planUser.Duration;
            return res;
        }
        public Response<int, object, object, object> PauseAnswer(int id)
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
            if (!plan.CanPause)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "本场测试不允许中途离开，关闭后计时仍然会继续";
                return res;
            }
            planUser.ModifyTime = DateTime.Now;
            planUser.ModifyUserId = CurrentUserId;
            planUser.Status = (int)EnumPlanUserStatus.Leave;
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

            var myExam = plan.BusTestPlanUserExamples.Where(a => a.UserId == CurrentUserId).OrderBy(a=>a.BeginTime).ToList();
            var resultDic = myExam.SelectMany(a => a.Example.BusExampleResults).GroupBy(a=>a.ExampleId).ToDictionary(a => a.Key,a=>a.ToList());
            var myOptions = plan.BusTestPlanUserOptions.Where(a => a.UserId == CurrentUserId).ToList();
            var examIds = myExam.Select(a=>a.ExampleId).Distinct().ToArray();
            var exams = BusExampleBll.GetListFilter(a=> a.Include(b=>b.BusExampleOptions).Include(b=>b.BusExampleQuestions).Where(b=> examIds.Contains(b.Id))).ToDictionary(a=>a.Id);
            Context.Configuration.LazyLoadingEnabled = false;
            myExam.ForEach(a => {
                var scores = new List<int>();
                var results = resultDic.G(a.ExampleId)?.OrderBy(b=>b.SortNo)?.Where(b => {
                    if(b.MinQuestionNo==null && b.MaxQuestionNo == null)
                    {
                        
                        var rt1 = a.Score >= b.MinScore && a.Score <= b.MaxScore;
                        if (rt1)
                        {
                            scores.Add(a.Score);
                        }
                        return rt1;
                    }
                    var query = b.Example.BusExampleQuestions.ToList().AsQueryable();
                    if (b.MinQuestionNo != null)
                    {
                        query = query.Where(c => c.SortNo >= b.MinQuestionNo);
                    }
                    if (b.MaxQuestionNo != null)
                    {
                        query = query.Where(c => c.SortNo <= b.MaxQuestionNo);
                    }
                    var questionIds = query.Select(c => c.QuestionId).ToArray();
                    var optionIds = myOptions.Where(c => questionIds.Contains(c.QuestionId)).Select(c=>c.OptionId).ToArray();
                    var score = exams.G(b.ExampleId).BusExampleOptions.Where(c => optionIds.Contains(c.OptionId)).Sum(c => c.Score);
                    
                    var rt2 = score >= b.MinScore && score <= b.MaxScore;
                    if (rt2)
                    {
                        scores.Add(score);
                    }
                    return rt2;
                }).ToList();
                a.Scores = string.Join(",", scores);
                //a.Result
                if (results.Count == 1)
                {
                    a.Result = results[0];
                }
                a.ResultIds = string.Join(",", results.Select(c => c.Id));
                a.ResultCode = string.Join("->",results.Select(c=>c.Code));
                a.ModifyTime = DateTime.Now;
                a.ModifyUserId = CurrentUserId;
            });
            planUser.ResultCode = string.Join("|",myExam.Select(a=>a.ResultCode));
            Context.Configuration.LazyLoadingEnabled = false;
            Context.SaveChanges();
            return res;
        }
    }
}
