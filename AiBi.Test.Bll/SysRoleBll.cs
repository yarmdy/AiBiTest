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
    public partial class SysRoleBll : BaseBll<SysRole, RoleReq.Page>
    {
        public static int GetRoleIdByEnum(EnumUserType type)
        {
            var roleId = 0;
            switch (type)
            {
                case EnumUserType.Agent:
                    {
                        roleId = 2;
                    }
                    break;
                case EnumUserType.Testor:
                    {
                        roleId = 3;
                    }
                    break;
                case EnumUserType.Tested:
                    {
                        roleId = 4;
                    }
                    break;
                case EnumUserType.Visitor:
                    {
                        roleId = 5;
                    }
                    break;
                case EnumUserType.Admin:
                    {
                        roleId = 1;
                    }
                    break;
            }
            return roleId;
        }
    }
}
