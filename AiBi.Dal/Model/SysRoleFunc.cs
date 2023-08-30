using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class SysRoleFunc:BaseEntity
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public int FuncId { get; set; }
        public virtual SysFunc Func { get; set; } = null!;
        public virtual SysRole Role { get; set; } = null!;
    }
}
