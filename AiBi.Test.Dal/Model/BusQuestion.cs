using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusQuestion : IdEntity
    {
        public BusQuestion()
        {
            BusExampleQuestions = new HashSet<BusExampleQuestion>();
            BusQuestionOptions = new HashSet<BusQuestionOption>();
        }

        /// <summary>
        /// 标题题面
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片 附件id
        /// </summary>
        public int? ImageId { get; set; }
        /// <summary>
        /// 1单选题 2多选题 3判断题
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 选项数
        /// </summary>
        public int OptionNum { get; set; }

        /// <summary>
        /// 总题面
        /// </summary>
        public string NContent { get; set; }
        public virtual SysAttachment Image { get; set; }
        public virtual ICollection<BusExampleQuestion> BusExampleQuestions { get; set; }
        public virtual ICollection<BusQuestionOption> BusQuestionOptions { get; set; }
    }
}
