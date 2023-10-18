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
    public partial class BusUserGroupBll : BaseBll<BusUserGroup, GroupReq.Page>
    {
        public override IQueryable<BusUserGroup> PageWhere(GroupReq.Page req, IQueryable<BusUserGroup> query)
        {
            return GetIncludeQuery(base.PageWhere(req, query), a => new { a.Parent});
        }

        public override IQueryable<BusUserGroup> PageWhereCustom(GroupReq.Page req, IQueryable<BusUserGroup> query, KeyValuePair<string, string> where)
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

        public override bool AddValidate(out string errorMsg, BusUserGroup model)
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
                
            }

            return true;
        }
        public override bool ModifyValidate(out string errorMsg, BusUserGroup model)
        {
            errorMsg = "";
            var ret = AddValidate(out errorMsg, model);
            if (!ret)
            {
                return false;
            }
            return true;
        }
        public override bool ModifyBefore(out string errorMsg, BusUserGroup model, BusUserGroup inModel, BusUserGroup oldModel)
        {
            errorMsg = "";
            
            return true;
        }
        public override IQueryable<BusUserGroup> PageOrder(GroupReq.Page req, IQueryable<BusUserGroup> query)
        {
            return query.OrderBy(a=>a.ParentId).ThenBy(a=>a.SortNo);
        }
    }
}
