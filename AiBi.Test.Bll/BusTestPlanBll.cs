using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace AiBi.Test.Bll
{
    public partial class BusTestPlanBll : BaseBll<BusTestPlan,PlanReq.Page>
    {
        public BusTestTemplateBll BusTestTemplateBll { get; set; }
        public override IQueryable<BusTestPlan> PageWhere(PlanReq.Page req, IQueryable<BusTestPlan> query)
        {
            query = GetIncludeQuery(query, a => new { a.Template,a.CreateUser});
            return base.PageWhere(req, query);
        }
        public override void PageAfter(PlanReq.Page req, Response<List<BusTestPlan>, object, object, object> res)
        {
            res.data.ForEach(a=>a.LoadChild(b=>new { CreateUserName = b.CreateUser.Name}));
        }
        public override void DetailAfter(int id, int? id2, Response<BusTestPlan, object, object, object> res)
        {
            res.data.LoadChild(a => new {a.Template.Image,Avatars = a.BusTestPlanUsers.Select(b=>b.User.BusUserInfoUsers.Where(c=>c.OwnerId==res.data.CreateUserId).FirstOrDefault()).ToList()});
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
    }
}
