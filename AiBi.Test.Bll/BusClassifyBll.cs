using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Data.Entity;

namespace AiBi.Test.Bll
{
    public partial class BusClassifyBll : BaseBll<BusClassify, ClassifyReq.Page>
    {
        public override IQueryable<BusClassify> PageWhere(ClassifyReq.Page req, IQueryable<BusClassify> query)
        {
            return GetIncludeQuery(base.PageWhere(req, query), a => new { a.Parent});
        }

        public override IQueryable<BusClassify> PageWhereCustom(ClassifyReq.Page req, IQueryable<BusClassify> query, KeyValuePair<string, string> where)
        {
            switch (where.Key.ToLower())
            {
                case "notparentid": {
                        if (!int.TryParse(where.Value, out int val)) {
                            return query;
                        }
                        
                        return query.Where(a => a.Id != val);
                    }
            }
            return query;
        }

        public override bool AddValidate(out string errorMsg, BusClassify model)
        {
            errorMsg = "";
            if (model.ParentId != null)
            {
                var parent = Find(false, model.ParentId.Value);
                if (parent == null)
                {
                    errorMsg = "上级不存在";
                    return false;

                }
                if (parent.ParentId != null)
                {
                    errorMsg = parent.Name + "是子分类，请选择父分类";
                    return false;
                }
            }

            return true;
        }
        public override bool ModifyValidate(out string errorMsg, BusClassify model)
        {
            errorMsg = "";
            var ret = AddValidate(out errorMsg, model);
            if (!ret)
            {
                return false;
            }
            return true;
        }
        public override bool ModifyBefore(out string errorMsg, BusClassify model, BusClassify inModel)
        {
            errorMsg = "";
            if (model.ParentId != null)
            {
                if (model.InverseParent.Count > 0)
                {
                    errorMsg = $"本分类旗下拥有{model.InverseParent.Count}个子分类，无法变更为子分类";
                    return false;
                }
            }
            return true;
        }
    }
}
