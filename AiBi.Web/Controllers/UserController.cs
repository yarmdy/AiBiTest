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
    public class UserController : BaseController<SysUser, UserReq.Page>
    {
        public SysUserBll CurBll { get; set; }
        public override BaseBll<SysUser, UserReq.Page> Bll => CurBll;
        public SysFuncBll SysFuncBll { get; set; }
        public SysRoleBll SysRoleBll { get; set; }

        #region 登录
        [AllowAnonymous]
        public ActionResult Login(HomeReq.Login req)
        {
            FormsAuthentication.SignOut();
            if (Request.IsAjaxRequest())
            {
                var res = new Response();
                res.code = EnumResStatus.NoPermissions;
                return Json(res);
            }

            var err = "";
            if (Request.HttpMethod.ToLower() == "post" && !string.IsNullOrEmpty(req.Account))
            {
                if (string.IsNullOrEmpty(req.Password))
                {
                    err = "请输入密码";
                    goto noredirect;
                }
                var res = SysUserBll.Login(req);
                if (res.code < 0)
                {
                    err = res.msg;
                    goto noredirect;
                }
                var cookie = $"{res.data.Id}|{res.data.Account}|{res.data.Name}";
                FormsAuthentication.SetAuthCookie(cookie, false);
                return Redirect(string.IsNullOrWhiteSpace(req.ReturnUrl) ? "/Home/Index" : req.ReturnUrl);

            }


        noredirect:
            ViewBag.Error = err;
            ViewBag.ReturnUrl = req.ReturnUrl;
            return View();
        }
        #endregion

        public ActionResult AgentList()
        {
            return View("List");
        }
        public ActionResult TestorList()
        {
            return View("List");
        }
        public ActionResult TestedList()
        {
            return View("List");
        }
        public ActionResult VisitorList()
        {
            return View("List");
        }


        public ActionResult GetAgentList(UserReq.Page req)
        {
            return Json(CurBll.GetAgentList(req));
        }
        public ActionResult GetTestorList(UserReq.Page req)
        {
            return Json(CurBll.GetTestorList(req));
        }
        public ActionResult GetTestedList(UserReq.Page req)
        {
            return Json(CurBll.GetTestedList(req));
        }
        public ActionResult GetVisitorList(UserReq.Page req)
        {
            return Json(CurBll.GetVisitorList(req));
        }

        public ActionResult AgentEdit(int? id)
        {
            return View("Edit");
        }
        public ActionResult TestorEdit(int? id)
        {
            return View("Edit");
        }
        public ActionResult TestedEdit(int? id)
        {
            return View("Edit");
        }
        public ActionResult VisitorEdit(int? id)
        {
            return View("Edit");
        }
        public ActionResult AddAgent(SysUser model)
        {
            return Json(CurBll.AddAgent(model));
        }
        public ActionResult AddTestor(SysUser model)
        {
            return Json(CurBll.AddTestor(model));
        }
        public ActionResult AddTested(SysUser model)
        {
            return Json(CurBll.AddTested(model));
        }
        public ActionResult AddVisitor(SysUser model)
        {
            return Json(CurBll.AddVisitor(model));
        }
    }
}