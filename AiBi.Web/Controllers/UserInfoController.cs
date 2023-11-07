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

namespace AiBi.Test.Web.Controllers
{
    public class UserInfoController : BaseController<BusUserInfo,UserInfoReq.Page>
    {
        public BusUserInfoBll CurBll { get; set; }

        public override BaseBll<BusUserInfo, UserInfoReq.Page> Bll => CurBll;

        public override ActionResult Select()
        {
            return View("List");
        }
        public ActionResult Import(UserInfoReq.Upload req)
        {
            var res = new Response<List<BusUserInfo>>();
            try
            {
                if ((Request.Files?.Count ?? 0) <= 0)
                {
                    res.code = EnumResStatus.Fail;
                    res.msg = "未找到文件";
                    return Json(res);
                }
                req.Stream = Request.Files[0].InputStream;
                res = CurBll.Import(req);
            }
            catch (Exception ex)
            {
                res.code = EnumResStatus.Fail;
                res.msg = ex.Message;
            }
            res.data = null;
            return Json(res);
        }
    }
}