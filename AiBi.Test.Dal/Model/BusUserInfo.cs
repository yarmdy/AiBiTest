using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusUserInfo : BaseEntity
    {
        public BusUserInfo()
        {
            
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 所有者Id
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别 1女 2男
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }
        
        public virtual SysUser User { get; set; }
        public virtual SysUser Owner { get; set; }
    }
}
