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

namespace AiBi.Test.Web.Controllers
{
    public class HomeController : BaseController<SysUser, UserReq.Page>
    {
        public override BaseBll<SysUser, UserReq.Page> Bll => SysUserBll;
        public override ActionResult Index()
        {
            //, Funcs = a.SysUserRoleUsers.SelectMany(b => b.Role.SysRoleFuncs.Select(c => c.Func)).GroupBy(b=>b.Id).Select(b=>b.FirstOrDefault()).ToList() 
            ViewBag.CurrentUser = SysUserBll.CurrentUser.LoadChild(a => new { a.Avatar});
            
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

        [AllowAnonymous]
        public ActionResult Login(HomeReq.Login req) {
            FormsAuthentication.SignOut();
            if (Request.IsAjaxRequest())
            {
                var res = new Response();
                res.code = EnumResStatus.NoPermissions;
                return Json(res);
            }

            var err = "";
            if (Request.HttpMethod.ToLower()=="post" && !string.IsNullOrEmpty(req.Account))
            {
                if (string.IsNullOrEmpty(req.Password))
                {
                    err = "请输入密码";
                    goto noredirect;
                }
                var res = SysUserBll.Login(req);
                if (res.code <0)
                {
                    err = res.msg;
                    goto noredirect;
                }
                var cookie = $"{res.data.Id}|{res.data.Account}|{res.data.Name}";
                FormsAuthentication.SetAuthCookie(cookie,false);
                return Redirect(string.IsNullOrWhiteSpace(req.ReturnUrl) ? "/Home/Index":req.ReturnUrl);

            }
            

        noredirect:
            ViewBag.Error = err;
            ViewBag.ReturnUrl = req.ReturnUrl;
            return View();
        }
        
    }
}