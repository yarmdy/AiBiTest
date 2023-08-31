using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusExampleOption:BaseEntity
    {
        /// <summary>
        /// 测试
        /// </summary>
        public int ExampleId { get; set; }
        /// <summary>
        /// 选项
        /// </summary>
        public int OptionId { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 跳转
        /// </summary>
        public int? Jump { get; set; }
        public virtual BusExample Example { get; set; }
        public virtual BusQuestionOption Option { get; set; }
    }
}
