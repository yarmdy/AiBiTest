using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace AiBi.Test.Bll
{
    public partial class BusUserInfoBll : BaseBll<BusUserInfo, UserInfoReq.Page>
    {
        public override IQueryable<BusUserInfo> PageWhere(UserInfoReq.Page req, IQueryable<BusUserInfo> query)
        {
            return GetIncludeQuery( 
                base.PageWhere(req, query)
                    .Where(a => a.OwnerId == CurrentUserId)
                , a => new { a.User,a.User.Avatar}
                );
        }
        public override Expression<Func<BusUserInfo, bool>> PageWhereKeyword(UserInfoReq.Page req)
        {
            var query = base.PageWhereKeyword(req);
            if (string.IsNullOrEmpty(req.keyword))
            {
                return query;
            }
            return query.Or(a=>
            a.User.Name.Contains(req.keyword)
            ||a.User.Account.Contains(req.keyword)
            ||a.User.Mobile.Contains(req.keyword)
            ||a.RealName.Contains(req.keyword)
            ||a.IdCardNo.Contains(req.keyword));
        }
    }
}
