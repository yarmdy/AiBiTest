using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusTestPlan : IdEntity
    {
        public BusTestPlan()
        {
            BusTestPlanExamples = new HashSet<BusTestPlanExample>();
            BusTestPlanUserExamples = new HashSet<BusTestPlanUserExample>();
            BusTestPlanUsers = new HashSet<BusTestPlanUser>();
        }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 0 创建中 1 已创建 2 已发布
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 可以暂停
        /// </summary>
        public bool CanPause { get; set; }
        /// <summary>
        /// 用例数量
        /// </summary>
        public int ExampleNum { get; set; }
        /// <summary>
        /// 题数
        /// </summary>
        public int QuestionNum { get; set; }
        /// <summary>
        /// 学员数
        /// </summary>
        public int UserNum { get; set; }
        public virtual ICollection<BusTestPlanExample> BusTestPlanExamples { get; set; }
        public virtual ICollection<BusTestPlanUserExample> BusTestPlanUserExamples { get; set; }
        public virtual ICollection<BusTestPlanUser> BusTestPlanUsers { get; set; }
    }
}
