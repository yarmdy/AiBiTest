using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;

namespace AiBi.Test.Bll
{
    public partial class SysUserBll:BaseBll<SysUser>
    {
        public SysUser Login(HomeReq.Login req)
        {
            var user = GetFirstOrDefaultNoTracking(a=>a.Account==req.Account);
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
    }
}
