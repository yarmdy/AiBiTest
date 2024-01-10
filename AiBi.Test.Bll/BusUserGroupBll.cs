using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace AiBi.Test.Bll
{
    public partial class BusUserGroupBll : BaseBll<BusUserGroup, GroupReq.Page>
    {
        public override IQueryable<BusUserGroup> PageWhere(GroupReq.Page req, IQueryable<BusUserGroup> query)
        {
            query = GetIncludeQuery(base.PageWhere(req, query), a => new { a.Parent});
            query = query.Where(a => a.CreateUserId == CurrentUserId);
            return query;
        }
        public override void PageAfter(GroupReq.Page req, Response<List<BusUserGroup>, object, object, object> res)
        {
            var ids = res.data.Select(a => a.Id).ToArray();
            var list = Context.BusUserGroups.AsNoTracking().Where(a => ids.Contains(a.Id)).Select(a => new { a.Id, isParent = a.Children.Any() }).ToDictionary(a=>a.Id,a=>a.isParent);
            res.data.ForEach(a => a.ObjectTag = new { IsParent = list.G(a.Id,false)});
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

        public override bool DirectDel => true;


        public int[] GetChildrenIds(int id)
        {
            var sql = @"with cte as(
select Id,ParentId,0 lvl from test.bus_UserGroup where Id=@id
union all
select b.Id,b.ParentId,a.lvl+1 lvl from cte a
join test.bus_UserGroup b on a.Id=b.ParentId and a.lvl<20
)select distinct Id from cte";
            var ids = Context.Database.SqlQuery<int>(sql,new SqlParameter("@id",id)).ToArray();
            return ids;
        }
        class BusUserGroupX : BusUserGroup { }
        public Dictionary<int,string> GetFullName(int[] groupIds)
        {
            groupIds = groupIds != null ? (groupIds.Where(a=>a>0).Distinct().ToArray()) : null;
            if (groupIds==null || groupIds.Length <= 0) {
                return new Dictionary<int, string>();
            }
            var cteSql = $@"with cte as(
select Id,ParentId,Name,cast(Name as nvarchar(4000)) FullName,Id BaseId from test.bus_UserGroup where Id in ({string.Join(",", groupIds)})
union all
select a.Id,a.ParentId,a.Name,cast(a.Name+'/'+b.FullName as nvarchar(4000)) FullName,b.BaseId from test.bus_UserGroup a
join cte b on a.Id=b.ParentId
)select BaseId Id,FullName Name from cte where ParentId is null";
            
            var dic = Context.Database.SqlQuery<BusUserGroupX>(cteSql).ToDictionary(a=>a.Id,a=>a.Name);

            return dic;
        }

        public Response<List<BusUserGroup>,object> GetGroupTree(int userId,bool includeChildrenIds=false)
        {
            var res = GetListFilter(a => a.Where(b => b.CreateUserId == userId), null, false);
            res.ForEach(a => {
                a.Parent = res.FirstOrDefault(b => b.Id == a.ParentId);
                a.Children = res.Where(b => b.ParentId == a.Id).OrderBy(b => b.SortNo).ToList();
            });
            res = res.Where(a => a.ParentId == null).OrderBy(a => a.SortNo).ToList();
            if (includeChildrenIds)
            {
                var dgList = res.ToList();
                while (dgList.Count > 0)
                {
                    var one = dgList.Last();
                    dgList.Remove(one);
                    dgList.AddRange(one.Children);

                    if (one.Children.Count <= 0)
                    {
                        continue;
                    }
                    var dg2List = one.Children.ToList();
                    var oneChildren = one.Children.Select(a=>a.Id).ToList();
                    one.ObjectTag = oneChildren;
                    while (dg2List.Count > 0)
                    {
                        var two = dg2List.Last();
                        dg2List.Remove(two);
                        dg2List.AddRange(two.Children);
                        oneChildren.AddRange(two.Children.Select(a => a.Id));
                    }
                }
            }
            return new Response<List<BusUserGroup>, object> { data=res};
        }
    }
}
