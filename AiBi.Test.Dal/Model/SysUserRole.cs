using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
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
        public virtual SysRole Role { get; set; }
        public virtual SysUser User { get; set; }
    }
}
