using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;

namespace AiBi.Test.Bll
{
    public partial class SysUserBll:BaseBll<SysUser, UserReq.Page>
    {
        public SysUser Login(HomeReq.Login req)
        {
            var user = GetFirstOrDefault(a=>a.Where(b=>b.Account == b.Account));
            if (user == null)
            {
                req.OutMsg = "账号不存在";
                req.OutCode = "";
                return null;
            }
            
            if (Crypt.AesDecrypt(user.Password) != req.Password)
            {
                req.OutMsg = "账号密码错误";
                req.OutCode = "";
                return null;
            }
            if (user.Status == (int)EnumEnableState.Disabled)
            {
                req.OutMsg = "账号已禁用";
                req.OutCode = "";
                return null;
            }
            if (user.ExpireTime >=DateTime.Now)
            {
                req.OutMsg = "账号已过期";
                req.OutCode = "";
                return null;
            }
            return user;
        }

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
                        _currentUser = userBll.Find(cuser.Id);
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

        //#region 重写
        //public override void ByKeysAfter(Response<SysUser, object, object, object> res, params object[] keys)
        //{
            
        //}
        //#endregion
    }
}
