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
    public partial class SysFuncBll : BaseBll<SysFunc, FuncReq.Page>
    {
        public bool UserHasFunc(int userId,string func)
        {
            return Context.SysUserRoles.AsNoTracking().Any(a => a.UserId==userId && a.Role.SysRoleFuncs.Any(b=>b.Func.Code==func));
        }
    }
}
