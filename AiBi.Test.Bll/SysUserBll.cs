using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.ComponentModel;
using Autofac.Features.OwnedInstances;

namespace AiBi.Test.Bll
{
    public partial class SysUserBll:BaseBll<SysUser, UserReq.Page>
    {
        public BusUserInfoBll BusUserInfoBll { get; set; }

        public override IQueryable<SysUser> PageWhere(UserReq.Page req, IQueryable<SysUser> query)
        {
            query = base.PageWhere(req, query).Include("SysUserRoleUsers.Role");
            switch (req.Tag)
            {
                case EnumUserType.Agent: {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }break;
                case EnumUserType.Testor: {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
                case EnumUserType.Tested: {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
                case EnumUserType.Visitor: {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
            }
            return query;
        }
        public override void PageAfter(UserReq.Page req, Response<List<SysUser>, object, object, object> res)
        {
            var ids = res.data.Select(a => a.Id).Distinct().ToArray();
            var infos = BusUserInfoBll.GetListFilter(a => a.Where(b => b.UserId == b.OwnerId && ids.Contains(b.UserId)));
            res.data.ForEach(a => a.LoadChild(b => {
                var thisroles = b.SysUserRoleUsers.Select(c => c.Role).ToList();
                var ret = new {
                    Roles = thisroles,
                    RoleNames = thisroles.Select(c => c.Name).ToList(),
                    UserInfo = infos.FirstOrDefault(c=>c.UserId==a.Id)
                };
                return ret;
            }));
        }

        public override bool AddValidate(out string errorMsg, SysUser model)
        {
            errorMsg = null;
            var res = true;
            var types = EnumConvert.ToList<EnumUserType>().Select(a => (int)a.Value);
            if (model.ObjectTag == null)
            {
                if (!types.Contains(model.Type))
                {
                    errorMsg = "无效的角色";
                    return false;
                }

            }
            if (string.IsNullOrEmpty(model.Account))
            {
                errorMsg = "账号不能为空";
                return false;
            }
            var old = GetFirstOrDefault(a=>a.Where(b=>b.Account==model.Account || b.Mobile==model.Mobile));
            if (old != null)
            {
                errorMsg = "账号或手机号不能重复";
                return false;
            }
            return res;
        }
        public override bool AddBefore(out string errorMsg, SysUser model, SysUser inModel)
        {
            var res = true;
            errorMsg = null;
            var roleId = 0;
            switch (model.ObjectTag)
            {
                case EnumUserType.Agent:
                    {
                        model.Type = (int)model.ObjectTag;
                        roleId = 2;
                    }
                    break;
                case EnumUserType.Testor:
                    {
                        model.Type = (int)model.ObjectTag;
                        roleId = 3;
                    }
                    break;
                case EnumUserType.Tested:
                    {
                        model.Type = (int)model.ObjectTag;
                        roleId = 4;
                    }
                    break;
                case EnumUserType.Visitor:
                    {
                        model.Type = (int)model.ObjectTag;
                        roleId = 5;
                    }
                    break;
            }
            var tempuserInfo = inModel.BusUserInfoUsers.FirstOrDefault() ?? new BusUserInfo { };
            tempuserInfo.User = model;
            tempuserInfo.Owner = model;
            tempuserInfo.CreateTime = DateTime.Now;
            tempuserInfo.CreateUserId = CurrentUserId;
            model.SysUserRoleUsers.Add(new SysUserRole {User=model,RoleId=roleId,CreateUserId=CurrentUserId,CreateTime=DateTime.Now });
            model.BusUserInfoUsers.Add(tempuserInfo);
            return res;
        }

        public Response<List<SysUser>, object, object, object> GetAgentList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Agent;
            return GetPageList(req);
        }
        
        public Response<List<SysUser>, object, object, object> GetTestorList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Testor;
            return GetPageList(req);
        }
        public Response<List<SysUser>, object, object, object> GetTestedList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Tested;
            return GetPageList(req);
        }
        public Response<List<SysUser>, object, object, object> GetVisitorList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Visitor;
            return GetPageList(req);

        }
        public Response<SysUser, object, object, object> AddAgent(SysUser model)
        {
            model.ObjectTag = EnumUserType.Agent;
            return Add(model);
        }
        public Response<SysUser, object, object, object> AddTestor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Testor;
            return Add(model);
        }
        public Response<SysUser, object, object, object> AddTested(SysUser model)
        {
            model.ObjectTag = EnumUserType.Tested;
            return Add(model);
        }
        public Response<SysUser, object, object, object> AddVisitor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Visitor;
            return Add(model);
        }
        public Response<SysUser, object, object, object> EditAgent(SysUser model)
        {
            model.ObjectTag = EnumUserType.Agent;
            return Edit(model);
        }
        public Response<SysUser, object, object, object> EditTestor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Testor;
            return Edit(model);
        }
        public Response<SysUser, object, object, object> EditTested(SysUser model)
        {
            model.ObjectTag = EnumUserType.Tested;
            return Edit(model);
        }
        public Response<SysUser, object, object, object> EditVisitor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Visitor;
            return Edit(model);
        }

        #region 登录
        public Response<SysUser> Login(HomeReq.Login req)
        {
            var res = new Response<SysUser>();
            var user = GetFirstOrDefault(a=>a.Where(b=>b.Account == req.Account));
            if (user == null)
            {
                res.code =EnumResStatus.Fail;
                res.msg = "账号不存在";
                return res;
            }
            
            if (Crypt.AesDecrypt(user.Password) != req.Password)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "账号密码错误";
                return res;
            }
            if (user.Status == (int)EnumEnableState.Disabled)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "账号已禁用";
                return res;
            }
            if (user.ExpireTime >=DateTime.Now)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "账号已过期";
                return res;
            }
            res.data = user;
            res.msg = "登陆成功";
            return res;
        }
        #endregion
        

        #region 当前状态
        public static SysUser GetCookie()
        {
            var arr = (HttpContext.Current?.User?.Identity?.Name + "").Split(new[] { '|'},StringSplitOptions.RemoveEmptyEntries);
            if(arr==null || arr.Length < 3)
            {
                return null;
            }
            return new SysUser { Id = Convert.ToInt32(arr[0]),Account = arr[1],Name = arr[2] };
        }
        public static SysUser CurrentUser
        {
            get
            {
                if (!HttpContextCache.CanUse)
                {
                    return null;
                }
                var _currentUser = HttpContextCache.Cache.G<SysUser>("CurrentUser");
                if (_currentUser == null)
                {
                    try
                    {
                        //获取当前登录用户信息
                        var cuser = GetCookie();
                        if (cuser == null) {
                            return null;
                        }
                        var userBll = AutofacExt.GetService<SysUserBll>();
                        _currentUser = userBll.Find(false,cuser.Id);
                        if (_currentUser == null)
                        {
                            return null;
                        }
                        HttpContextCache.Cache["CurrentUser"] = _currentUser;
                    }
                    catch (Exception)
                    {
                        _currentUser = null;
                    }
                }

                return _currentUser;
            }
        }
        #endregion

        

        public static string GetAvatar(SysUser user)
        {
            return (!string.IsNullOrWhiteSpace(user?.AvatarName)) ? (user.AvatarName) :( user?.Avatar?.FullName ?? "/static/avatars/defaultavatar.png");
        }
        public static string GetRoleName(SysUser user)
        {
            return user?.SysUserRoleUsers?.OrderBy(a => a.RoleId)?.FirstOrDefault()?.Role?.Name;
        }
        public static string[] GetRoleNames(SysUser user)
        {
            return user?.SysUserRoleUsers?.OrderBy(a => a.RoleId)?.Select(a => a.Role.Name)?.ToArray() ?? new string[0];
        }

    }
}
