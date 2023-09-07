using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AiBi.Test.Dal.Model
{
    public partial class BaseEntity
    {
        public BaseEntity()
        {
            
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int? ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public int? DelUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DelTime { get; set; }

        [JsonIgnore]
        public virtual SysUser CreateUser { get; set; }
        [JsonIgnore]
        public virtual SysUser DelUser { get; set; }
        [JsonIgnore]
        public virtual SysUser ModifyUser { get; set; }
        [NotMapped]
        public object ObjectTag { get; set; }
    }
}
