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
                    }break;
            }
            return query;
        }
    }
}
