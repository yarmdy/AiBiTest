using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace AiBi.Test.Dal.Model
{
    public partial class BusExampleResult : IdEntity
    {
        public BusExampleResult()
        {
            BusTestPlanUserExamples = new HashSet<BusTestPlanUserExample>();
        }

        /// <summary>
        /// 用例Id
        /// </summary>
        public int ExampleId { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string NContent { get; set; }
        public int? ImageId { get; set; }
        /// <summary>
        /// 最小分值
        /// </summary>
        public int MinScore { get; set; }
        /// <summary>
        /// 最大分值
        /// </summary>
        public int MaxScore { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
        public virtual BusExample Example { get; set; }
        public virtual ICollection<BusTestPlanUserExample> BusTestPlanUserExamples { get; set; }
        public SysAttachment Image { get; set; }
    }
}
