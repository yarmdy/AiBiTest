using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class BusTestPlanUser:BaseEntity
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
        /// 进度 0 创建 1 进入系统 2 开始答题 3 离开 4 答题完成
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 当前实例
        /// </summary>
        public int? CurrentExample { get; set; }
        /// <summary>
        /// 完成的实例
        /// </summary>
        public int FinishExample { get; set; }
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
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 结果代码 | 分割
        /// </summary>
        public string? ResultCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        public virtual BusTestPlan Plan { get; set; } = null!;
        public virtual SysUser User { get; set; } = null!;
    }
}
