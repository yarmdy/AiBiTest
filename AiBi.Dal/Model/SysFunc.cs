using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class SysFunc : IdEntity
    {
        public SysFunc()
        {
            SysRoleFuncs = new HashSet<SysRoleFunc>();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 功能代码
        /// </summary>
        public string Code { get; set; } = null!;
        /// <summary>
        /// 路由
        /// </summary>
        public string Route { get; set; } = null!;
        /// <summary>
        /// 0 大功能 1按钮或小功能
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        public virtual ICollection<SysRoleFunc> SysRoleFuncs { get; set; }
    }
}
