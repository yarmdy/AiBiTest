using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AiBi.Test.Bll;
using AiBi.Test.Common;
using System.Web.Security;
using AiBi.Test.Dal.Model;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Caching;
using System.Data.Entity;

namespace AiBi.Test.Web.Controllers
{
    public class WomanUserBll : SysUserBll
    {

    }
    public class HomeController : BaseController<SysUser, UserReq.Page>
    {
        public SysUserBll CurBll { get; set; }
        public override BaseBll<SysUser, UserReq.Page> Bll => CurBll;
        public SysFuncBll SysFuncBll { get; set; }
        public SysRoleBll SysRoleBll { get; set; }
        public override ActionResult Index()
        {
            //, Funcs = a.SysUserRoleUsers.SelectMany(b => b.Role.SysRoleFuncs.Select(c => c.Func)).GroupBy(b=>b.Id).Select(b=>b.FirstOrDefault()).ToList() 
            var userId = SysUserBll.CurrentUserId;
            var roles = SysRoleBll.GetListFilter(a => a.Where(b => b.SysUserRoles.Any(c => c.UserId == userId)),a=>a.OrderBy(b=>b.Id),true);
            var roleids = roles.Select(a => a.Id).ToArray();
            var funcs = SysFuncBll.GetListFilter(a => a.Where(b => b.SysRoleFuncs.Any(c => roleids.Contains(c.RoleId))),a=>a.OrderBy(b=>b.Id),true);
            ViewBag.CurrentUser = SysUserBll.CurrentUser.LoadChild(a => new { a.Avatar, Funcs=funcs,Roles=roles });
            
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}