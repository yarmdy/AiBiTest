using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using iText.Svg.Renderers.Impl;

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
                    b.BusTestPlanExamples,
                    Image3 = b.Template.BusTestTemplateExamples.First().Example.BusExampleQuestions.First().Question.Image,
                    b.BusTestPlanUserOptions.First().Option,
                    b.BusTestPlanUserExamples,
                    b.BusTestPlanUserExamples.First().Result.Image,
                    Options = b.Template.BusTestTemplateExamples.First().Example.BusExampleOptions,
                    Image4 = b.Template.BusTestTemplateExamples.First().Example.BusExampleResults.First().Image,
                    Image2 =b.Template.Image,
                    b.CreateUser.BusUserInfoUsers
                }).Where(b => b.Id == id);
            },true);
            plan.BusTestPlanUserOptions = plan.BusTestPlanUserOptions.Where(b => b.UserId == id2).ToList();
            plan.BusTestPlanUserExamples = plan.BusTestPlanUserExamples.Where(b => b.UserId == id2).OrderBy(a=>a.BeginTime).ToList();
            plan.Template.BusTestTemplateExamples = plan.Template.BusTestTemplateExamples.OrderBy(a=>a.SortNo).ToList();
            plan.Template.BusTestTemplateExamples.ToList().ForEach(a => a.Example.BusExampleQuestions = a.Example.BusExampleQuestions.OrderBy(b => b.SortNo).ThenBy(b => b.SortNo2).ToList());
            plan.CreateUser.BusUserInfoUsers = plan.CreateUser.BusUserInfoUsers.Where(a => a.OwnerId==a.UserId).ToList();
            plan.ObjectTag = plan.CreateUser;
            res.data.User.BusUserInfoUsers = res.data.User.BusUserInfoUsers.Where(a => a.OwnerId == plan.CreateUserId).ToList();
            res.data.Plan = plan;
            Context.Configuration.LazyLoadingEnabled = true;
        }

        private BusTestPlan getExportPlan(int planId, int[] userIds)
        {
            if (userIds == null || userIds.Length <= 0)
            {
                throw new Exception("没有任何学员被导入");
            }
            Context.Configuration.LazyLoadingEnabled = false;
            var plan = BusTestPlanBll.GetFirstOrDefault(a => {

                return BusTestPlanBll.GetIncludeQuery(a, b => new {
                    b.BusTestPlanExamples,
                    PlanuserInfo = b.BusTestPlanUsers.First().User.BusUserInfoUsers.First().UserGroup,
                    Image3 = b.Template.BusTestTemplateExamples.First().Example.BusExampleQuestions.First().Question.Image,
                    b.BusTestPlanUserOptions.First().Option,
                    b.BusTestPlanUserExamples,
                    b.BusTestPlanUserExamples.First().Result.Image,
                    Options = b.Template.BusTestTemplateExamples.First().Example.BusExampleOptions,
                    Image4 = b.Template.BusTestTemplateExamples.First().Example.BusExampleResults.First().Image,
                    Image2 = b.Template.Image,
                    b.CreateUser.BusUserInfoUsers
                }).Where(b => b.Id == planId);
            }, true);
            if (plan == null)
            {
                throw new Exception("测评任务不存在");
            }
            
            plan.BusTestPlanUserOptions = plan.BusTestPlanUserOptions.Where(b => userIds.Contains(b.UserId)).ToList();
            plan.BusTestPlanUserExamples = plan.BusTestPlanUserExamples.Where(b => userIds.Contains(b.UserId)).OrderBy(a => a.BeginTime).ToList();
            plan.Template.BusTestTemplateExamples = plan.Template.BusTestTemplateExamples.OrderBy(a => a.SortNo).ToList();
            plan.Template.BusTestTemplateExamples.ToList().ForEach(a => a.Example.BusExampleQuestions = a.Example.BusExampleQuestions.OrderBy(b => b.SortNo).ThenBy(b => b.SortNo2).ToList());
            plan.CreateUser.BusUserInfoUsers = plan.CreateUser.BusUserInfoUsers.Where(a => a.OwnerId == a.UserId).ToList();
            plan.ObjectTag = plan.CreateUser;
            plan.BusTestPlanUsers = plan.BusTestPlanUsers.Where(a => userIds.Contains(a.UserId)).ToList();
            if (userIds.Any(a => !plan.BusTestPlanUsers.Any(b => a == b.UserId)))
            {
                throw new Exception("要导出的学员不存在");
            }
            var groupIds = plan.BusTestPlanUsers.Select(a=>a.User.BusUserInfoUsers.FirstOrDefault()?.GroupId).Where(a=>a.HasValue).Select(a=>(int)a).Distinct().ToArray();
            var dic = AutofacExt.GetService<BusUserGroupBll>().GetFullName(groupIds);
            plan.BusTestPlanUsers.ForEach((a,i) => {
                var group = a.User?.BusUserInfoUsers?.FirstOrDefault()?.UserGroup;
                if (group == null)
                {
                    return;
                }
                group.Name = dic.G(group.Id) ?? group.Name;
            });

            return plan;
        }
        public Stream Export(out string fileName,int planId, int[] userIds)
        {
            fileName = "测评报告.pdf";
            var plan = getExportPlan(planId,userIds);
            fileName = $"{plan.Name}的测评报告.xlsx";
            var stream = planToExcel(plan);

            return stream;
        }
        public Stream ExportDetails(out string fileName, int planId, int[] userIds)
        {
            fileName = "测评报告.pdf";
            var plan = getExportPlan(planId, userIds);
            fileName = $"{plan.Name}的测评报告明细.pdf";
            var stream = planToPdfDetails(plan);

            return stream;
        }


    }
}
