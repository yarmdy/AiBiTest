using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AiBi.Test.Bll
{
    public partial class BusTestPlanUserBll : BaseBll<BusTestPlanUser,PlanUserReq.Page>
    {
        public BusTestPlanBll BusTestPlanBll { get; set; }
        public override void DetailAfter(int id, int? id2, Response<BusTestPlanUser, object, object, object> res)
        {
            res.data.LoadChild(a => new
            {
                UserInfo = a.User.BusUserInfoUsers.ToList(),
            });
            
            
            Context.Configuration.LazyLoadingEnabled = false;
            var plan = BusTestPlanBll.GetFirstOrDefault(a => {
                
                return BusTestPlanBll.GetIncludeQuery(a, b => new {
                    Image3 = b.Template.BusTestTemplateExamples.First().Example.BusExampleQuestions.First().Question.Image,
                    b.BusTestPlanUserOptions.First().Option,
                    b.BusTestPlanUserExamples,
                    b.BusTestPlanUserExamples.First().Result.Image,
                    Options = b.Template.BusTestTemplateExamples.First().Example.BusExampleOptions,
                    Image4 = b.Template.BusTestTemplateExamples.First().Example.BusExampleResults.First().Image,
                    Image2 =b.Template.Image
                }).Where(b => b.Id == id);
            },true);
            plan.BusTestPlanUserOptions = plan.BusTestPlanUserOptions.Where(b => b.UserId == id2).ToList();
            plan.BusTestPlanUserExamples = plan.BusTestPlanUserExamples.Where(b => b.UserId == id2).OrderBy(a=>a.BeginTime).ToList();
            plan.Template.BusTestTemplateExamples = plan.Template.BusTestTemplateExamples.OrderBy(a=>a.SortNo).ToList();
            plan.Template.BusTestTemplateExamples.ToList().ForEach(a => a.Example.BusExampleQuestions = a.Example.BusExampleQuestions.OrderBy(b => b.SortNo).ThenBy(b => b.SortNo2).ToList());
            res.data.User.BusUserInfoUsers = res.data.User.BusUserInfoUsers.Where(a => a.OwnerId == plan.CreateUserId).ToList();
            res.data.Plan = plan;
            Context.Configuration.LazyLoadingEnabled = true;
        }
    }
}
