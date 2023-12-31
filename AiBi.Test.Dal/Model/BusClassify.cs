﻿using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class BusClassify: IdEntity
    {
        public BusClassify()
        {
            BusExampleClassifies = new HashSet<BusExample>();
            BusExampleSubClassifies = new HashSet<BusExample>();
            BusUserClassifies = new HashSet<BusUserClassify>();
            InverseParent = new HashSet<BusClassify>();
            BusTestTemplateClassifies = new HashSet<BusTestTemplate>();
            BusTestTemplateSubClassifies = new HashSet<BusTestTemplate>();
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
        public virtual BusClassify Parent { get; set; }
        public virtual ICollection<BusExample> BusExampleClassifies { get; set; }
        public virtual ICollection<BusExample> BusExampleSubClassifies { get; set; }
        public virtual ICollection<BusTestTemplate> BusTestTemplateClassifies { get; set; }
        public virtual ICollection<BusTestTemplate> BusTestTemplateSubClassifies { get; set; }
        public virtual ICollection<BusUserClassify> BusUserClassifies { get; set; }
        public virtual ICollection<BusClassify> InverseParent { get; set; }
    }
}
