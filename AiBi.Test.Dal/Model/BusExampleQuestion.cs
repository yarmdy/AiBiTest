using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusExampleQuestion : BaseEntity
    {
        /// <summary>
        /// 测试
        /// </summary>
        public int ExampleId { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 小序号
        /// </summary>
        public int? SortNo2 { get; set; }
        /// <summary>
        /// 限时
        /// </summary>
        public int? Duration { get; set; }

        public bool Prompt { get; set; }

        public string PromptMsg { get; set; }
        public virtual BusExample Example { get; set; }
        public virtual BusQuestion Question { get; set; }
    }
}
