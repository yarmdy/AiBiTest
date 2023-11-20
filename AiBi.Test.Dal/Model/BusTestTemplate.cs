using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiBi.Test.Dal.Model
{
    public partial class BusTestTemplate : IdEntity
    {
        public BusTestTemplate()
        {
            BusTestTemplateExamples = new HashSet<BusTestTemplateExample>();
            BusUserTestTemplates = new HashSet<BusUserTestTemplate>();
            BusTestPlans=new HashSet<BusTestPlan>();
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int? ClassifyId { get; set; }
        /// <summary>
        /// 子类
        /// </summary>
        public int? SubClassifyId { get; set; }
        /// <summary>
        /// 关键字 | 分割
        /// </summary>
        public string Keys { get; set; }
        [NotMapped]
        public string ShowKeys
        {
            get
            {
                var arr = (Keys + "").Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                return string.Join("|", arr);
            }
        }
        /// <summary>
        /// 实例数
        /// </summary>
        public int ExampleNum { get; set; }
        /// <summary>
        /// 题数
        /// </summary>
        public int QuestionNum { get; set; }

        /// <summary>
        /// 时长（分钟）
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 图片附件Id
        /// </summary>
        public int? ImageId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 优惠价格
        /// </summary>
        public decimal? DiscountPrice { get; set; }
        /// <summary>
        /// 说明  给被测者
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 备注  给组织测试者
        /// </summary>
        public string NContent { get; set; }
        /// <summary>
        /// 0 创建中 1 创建完成 2已上架
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 可以暂停
        /// </summary>
        public bool CanPause { get; set; }

        /// <summary>
        /// 0 普通 1 基础认知
        /// </summary>
        public int ExampleType { get; set; } = 0;

        public virtual BusClassify Classify { get; set; }
        public virtual SysAttachment Image { get; set; }
        public virtual BusClassify SubClassify { get; set; }
        public virtual ICollection<BusTestTemplateExample> BusTestTemplateExamples { get; set; }
        public virtual ICollection<BusUserTestTemplate> BusUserTestTemplates { get; set; }
        public virtual ICollection<BusTestPlan> BusTestPlans { get; set; }
    }
}
