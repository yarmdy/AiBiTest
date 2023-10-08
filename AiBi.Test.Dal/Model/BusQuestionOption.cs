using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusQuestionOption : IdEntity
    {
        public BusQuestionOption()
        {
            BusExampleOptions = new HashSet<BusExampleOption>();

            BusTestPlanUserOptions = new HashSet<BusTestPlanUserOption>();
        }

        /// <summary>
        /// 问题Id
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 按钮文字
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Remark { get; set; }
        public virtual BusQuestion Question { get; set; }
        public virtual ICollection<BusExampleOption> BusExampleOptions { get; set; }

        public virtual ICollection<BusTestPlanUserOption> BusTestPlanUserOptions { get; set; }
    }
}
