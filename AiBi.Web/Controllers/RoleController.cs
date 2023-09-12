﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AiBi.Test.Bll;
using AiBi.Test.Common;
using System.Web.Security;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Web.Controllers
{
    public class RoleController : BaseController<SysRole,RoleReq.Page>
    {
        public SysRoleBll CurBll { get; set; }

        public override BaseBll<SysRole, RoleReq.Page> Bll => CurBll;
    }
}