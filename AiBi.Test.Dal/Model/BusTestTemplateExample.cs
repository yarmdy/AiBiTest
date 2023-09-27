using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusTestTemplateExample:BaseEntity
    {
        /// <summary>
        /// 模板id
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// 用例id
        /// </summary>
        public int ExampleId { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 时长（分钟） 0不限
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 可以暂停
        /// </summary>
        public bool? CanPause { get; set; }
        public virtual BusExample Example { get; set; }
        public virtual BusTestTemplate Template { get; set; }
    }
}
