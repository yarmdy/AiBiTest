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
using System.Web.Security;
using System.Web.Razor.Generator;

namespace AiBi.Test.Bll
{
    public partial class BusUserInfoBll : BaseBll<BusUserInfo, UserInfoReq.Page>
    {
        public override IQueryable<BusUserInfo> PageWhere(UserInfoReq.Page req, IQueryable<BusUserInfo> query)
        {
            query = GetIncludeQuery( 
                base.PageWhere(req, query)
                    .Where(a => a.OwnerId == CurrentUserId)
                , a => new { a.User,a.User.Avatar}
                );
            if (req.GroupId != null)
            {
                query = query.Where(a=>a.GroupId==req.GroupId);
            }
            return query;
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

        public override void DetailAfter(int id, int? id2, Response<BusUserInfo, object, object, object> res)
        {
            res.data.LoadChild(a => new { a.User.Avatar});
        }

        public override bool AddValidate(out string errorMsg, BusUserInfo model)
        {
            errorMsg = "";
            if (model.User == null)
            {
                errorMsg = "非法请求";
                return false;
            }
            if ((model.User.Password + "").Length < 6)
            {
                errorMsg = "密码必须大于6位";
                return false;
            }
            var SysUserBll = AutofacExt.GetService<SysUserBll>();
            var users = SysUserBll.GetListFilter(a => a.Where(b => b.Id != model.UserId && (b.Account == model.User.Account || b.Mobile == model.User.Mobile && b.Mobile != null)),null,false);
            if (users.Count >1)
            {
                errorMsg = "登录名和手机号对应多个现有用户，添加失败";
                return false;
            }
            if (users.Count <= 0)
            {
                
                users = new List<SysUser> { new SysUser { Password=Crypt.AesEncrypt(model.User.Password),Account=model.User.Account,Name=model.User.Account,Mobile=model.User.Mobile,Type=(int)EnumUserType.Tested,Status=1,CreateTime=DateTime.Now,CreateUserId=CurrentUserId} };
                var mainInfo = new BusUserInfo { }.CopyFrom(model);
                mainInfo.User = users[0];
                mainInfo.Owner = users[0];
                mainInfo.CreateTime = DateTime.Now;
                mainInfo.CreateUserId = CurrentUserId;
                Context.BusUserInfos.Add(mainInfo);
            }
            else
            {
                var tmpinfo = users[0].BusUserInfoUsers.FirstOrDefault(a => a.OwnerId == CurrentUserId);
                if (tmpinfo != null)
                {
                    errorMsg = "用户已存在，添加失败";
                    return false;
                }
            }
            var user = users[0];
            var roleId = SysRoleBll.GetRoleIdByEnum(EnumUserType.Tested);
            if (!user.SysUserRoleUsers.Any(a => a.RoleId == roleId))
            {
                user.SysUserRoleUsers.Add(new SysUserRole { User = users[0], RoleId = roleId, CreateUserId = CurrentUserId, CreateTime = DateTime.Now });
            }
            model.User = user;
            model.OwnerId = CurrentUserId;

            return true;
        }
        public override bool AddBefore(out string errorMsg, BusUserInfo model, BusUserInfo inModel)
        {
            return base.AddBefore(out errorMsg, model, inModel);
        }

        public override bool ModifyValidate(out string errorMsg, BusUserInfo model)
        {
            errorMsg = "";
            if (model.User == null) {
                errorMsg = "非法请求";
                return false;
            }
            return true;
        }
        public override bool ModifyBefore(out string errorMsg, BusUserInfo model, BusUserInfo inModel, BusUserInfo oldModel)
        {
            errorMsg = "";
            if(!model.User.DiffCopy(inModel.User,a=>new { a.Account, a.Mobile }))
            {
                goto next;
            }
            var SysUserBll = AutofacExt.GetService<SysUserBll>();
            var exists = SysUserBll.GetListFilter(a=>a.Where(b=>b.Id!=model.UserId&&(b.Account==model.User.Account || b.Mobile==model.User.Mobile && b.Mobile!=null)));
            if (exists.Count <= 0)
            {
                goto next;
            }
            if (exists.Any(a => a.Account == model.User.Account))
            {
                errorMsg = "登录名已存在，无法重命名";
                return false;
            }
            if (exists.Any(a => a.Mobile == model.User.Mobile))
            {
                errorMsg = "手机号已存在，无法修改";
                return false;
            }

            next:


            return true;
        }
    }
}
