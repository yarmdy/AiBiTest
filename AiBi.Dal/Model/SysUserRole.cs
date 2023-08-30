using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class SysUserRole:BaseEntity
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public int RoleId { get; set; }
        public virtual SysRole Role { get; set; } = null!;
        public virtual SysUser User { get; set; } = null!;
    }
}
