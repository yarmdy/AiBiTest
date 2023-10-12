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
        public ActionResult ModifyAgent(SysUser model)
        {
            return Json(CurBll.ModifyAgent(model));
        }
        public ActionResult ModifyTestor(SysUser model)
        {
            return Json(CurBll.ModifyTestor(model));
        }
        public ActionResult ModifyTested(SysUser model)
        {
            return Json(CurBll.ModifyTested(model));
        }
        public ActionResult ModifyVisitor(SysUser model)
        {
            return Json(CurBll.ModifyVisitor(model));
        }

        public ActionResult EnableUser(int id,int status)
        {
            return Json(CurBll.EditProperties(id,null,new { Status=status}));
        }
        public ActionResult ShowPassword(int id,string password)
        {
            var res = new Response<string>();
            res.data = Crypt.AesDecrypt(password);
            if (res.data == null)
            {
                res.msg = $"密码解密失败，密码原文：{password}";
                res.code = EnumResStatus.Fail;
            }
            return Json(res);
        }
        public ActionResult SetPassword(int id, string password)
        {
            var res = new Response<string>();
            if (password.Length < 6)
            {
                res.code=EnumResStatus.Fail;
                res.msg = "密码长度不能少于6位";
                return Json(res);
            }
            var pwd = Crypt.AesEncrypt(password);
            var ret = CurBll.EditProperties(id,null,new { Password = pwd});
            res.CopyFrom(ret);
            if (res.code == EnumResStatus.Succ)
            {
                res.data = pwd;
            }
            return Json(res);
        }
        public ActionResult ChangePassword(string password,string oldPassword)
        {
            var res = new Response<string>();
            var old = Crypt.AesDecrypt( CurBll.Find(CurrentUserId).Password);
            if (oldPassword != old)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "旧密码错误";
                return Json(res);
            }
            if (password.Length < 6)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "密码长度不能少于6位";
                return Json(res);
            }
            var pwd = Crypt.AesEncrypt(password);
            var ret = CurBll.EditProperties(CurrentUserId, null, new { Password = pwd });
            res.CopyFrom(ret);
            if (res.code == EnumResStatus.Succ)
            {
                res.data = pwd;
                res.msg = "密码修改成功";
            }
            return Json(res);
        }
    }
}