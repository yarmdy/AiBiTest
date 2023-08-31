using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class SysAttachment : IdEntity
    {
        public SysAttachment()
        {
            BusExamples = new HashSet<BusExample>();
            BusQuestions = new HashSet<BusQuestion>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }
        /// <summary>
        /// 0 未知 100 问题图片 200
        /// </summary>
        public int Module { get; set; }
        /// <summary>
        /// 0创建 1应用 每天状态为0的会被自动清掉以释放空间
        /// </summary>
        public int Status { get; set; }
        public virtual ICollection<BusExample> BusExamples { get; set; }
        public virtual ICollection<BusQuestion> BusQuestions { get; set; }
    }
}
