using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class BusUserExample:BaseEntity
    {
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用例
        /// </summary>
        public int ExampleId { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }
        public virtual BusExample Example { get; set; } = null!;
        public virtual SysUser User { get; set; } = null!;
    }
}
