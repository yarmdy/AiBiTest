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
    public class HomeController : BaseController<SysUser>
    {
        public SysUserBll SysUserBll { get; set; }

        public override BaseBll<SysUser> Bll => SysUserBll;

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
            if (Request.IsAjaxRequest())
            {
                Res.code = Models.EnumResStatus.NoPermissions;
                return Json(Res,JsonRequestBehavior.AllowGet);
            }

            var err = "";
            if (Request.HttpMethod.ToLower()=="post" && !string.IsNullOrEmpty(req.Account))
            {
                if (string.IsNullOrEmpty(req.Password))
                {
                    err = "请输入密码";
                    goto noredirect;
                }
                var user = SysUserBll.Login(req);
                if (user == null)
                {
                    err = req.OutMsg;
                    goto noredirect;
                }
                var cookie = $"{user.Id}|{user.Account}|{user.Name}";
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