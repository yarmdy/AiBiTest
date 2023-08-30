using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class SysRole : IdEntity
    {
        public SysRole()
        {
            SysRoleFuncs = new HashSet<SysRoleFunc>();
            SysUserRoles = new HashSet<SysUserRole>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        public virtual ICollection<SysRoleFunc> SysRoleFuncs { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}
