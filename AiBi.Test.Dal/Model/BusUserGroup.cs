using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusUserGroup: IdEntity
    {
        public BusUserGroup()
        {
            InverseParent = new HashSet<BusUserGroup>();
            BusUserInfos = new HashSet<BusUserInfo>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上级分类
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNo { get; set; }
        public virtual BusUserGroup Parent { get; set; }
        public virtual ICollection<BusUserGroup> InverseParent { get; set; }
        public virtual ICollection<BusUserInfo> BusUserInfos { get; set; }
    }
}
