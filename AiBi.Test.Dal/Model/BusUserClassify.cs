using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusUserClassify:BaseEntity
    {
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int ClassifyId { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        public virtual BusClassify Classify { get; set; }
        public virtual SysUser User { get; set; }
    }
}
