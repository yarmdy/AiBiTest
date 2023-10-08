using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusTestPlanUserExample:BaseEntity
    {
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
        /// 进度 0 未答 1 正在答题 2 答完
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 当前问题
        /// </summary>
        public int? CurrentQuestion { get; set; }
        /// <summary>
        /// 完成的问题
        /// </summary>
        public int FinishQuestion { get; set; }
        /// <summary>
        /// 已用时长
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 结果id
        /// </summary>
        public int? ResultId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public virtual BusExample Example { get; set; }
        public virtual BusTestPlan Plan { get; set; }
        public virtual BusExampleResult Result { get; set; }
        public virtual SysUser User { get; set; }
    }
}
