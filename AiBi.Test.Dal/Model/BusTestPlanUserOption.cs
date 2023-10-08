using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusTestPlanUserOption : BaseEntity
    {
        public BusTestPlanUserOption()
        {
            
        }

        /// <summary>
        /// 计划id
        /// </summary>
        public int PlanId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 实例id
        /// </summary>
        public int ExampleId { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 选项id
        /// </summary>
        public int OptionId { get; set; }

        /// <summary>
        /// 计划
        /// </summary>
        public virtual BusTestPlan BusTestPlan { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual SysUser User { get; set; }
        /// <summary>
        /// 实例
        /// </summary>
        public virtual BusExample Example { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public virtual BusQuestion Question { get; set; }
        /// <summary>
        /// 选项
        /// </summary>
        public virtual BusQuestionOption Option { get; set; }
    }
}
