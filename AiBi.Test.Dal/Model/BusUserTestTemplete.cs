using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusUserTestTemplete : BaseEntity
    {
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用例
        /// </summary>
        public int TempleteId { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }
        public virtual BusTestTemplete Templete { get; set; }
        public virtual SysUser User { get; set; }
    }
}
