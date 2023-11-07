using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.ComponentModel;
using Autofac.Features.OwnedInstances;
using System.Linq.Expressions;
using System.Web.Security;
using System.Collections;
using System.Data;
using System.Reflection;

namespace AiBi.Test.Bll
{
    public partial class SysUserBll:BaseBll<SysUser, UserReq.Page>
    {
        public BusUserInfoBll BusUserInfoBll { get; set; }

        

        #region 高级重写
        public override IQueryable<SysUser> PageWhere(UserReq.Page req, IQueryable<SysUser> query)
        {
            query = base.PageWhere(req, query).Include("SysUserRoleUsers.Role");
            const EnumUserType MyTestor = EnumUserType.Testor | (EnumUserType)(256 * 256);
            switch (req.Tag)
            {
                case EnumUserType.Agent:
                    {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
                case EnumUserType.Testor:
                    {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
                case EnumUserType.Tested:
                    {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
                case EnumUserType.Visitor:
                    {
                        query = query.Where(a => a.Type == (int)req.Tag);
                    }
                    break;
                case MyTestor: {
                        query = query.Where(a => a.Type == (int)EnumUserType.Testor && a.CreateUserId==CurrentUserId);
                    }
                    break;
            }
            return query;
        }
        public override void PageAfter(UserReq.Page req, Response<List<SysUser>, object, object, object> res)
        {
            var ids = res.data.Select(a => a.Id).Distinct().ToArray();
            var infos = BusUserInfoBll.GetListFilter(a => a.Where(b => b.UserId == b.OwnerId && ids.Contains(b.UserId)));
            Context.Configuration.LazyLoadingEnabled = false;
            res.data.ForEach(a => {
                a.SysUserRoleUsers.ForEach((b,i) => { b.User = null;b.Role.SysUserRoles = null; });
                
                var thisroles = a.SysUserRoleUsers.Select(c => c.Role).ToList();
                a.ObjectTag = new
                {
                    Roles = thisroles,
                    RoleNames = thisroles.Select(c => c.Name).ToList(),
                    UserInfo = infos.FirstOrDefault(c => c.UserId == a.Id)
                };
            });
        }

        public override void DetailAfter(int id, int? id2, Response<SysUser, object, object, object> res)
        {
            if (res.code != EnumResStatus.Succ)
            {
                return;
            }
            //res.data.LoadChild(a => new { BusUserInfoUser = a.BusUserInfoUsers.ToList() });
            res.data.BusUserInfoUsers = res.data.BusUserInfoUsers.Where(a=>a.UserId==a.OwnerId).ToList();
        }
        public override bool AddValidate(out string errorMsg, SysUser model)
        {
            errorMsg = null;
            var res = true;
            var types = EnumConvert.ToList<EnumUserType>().Select(a => (int)a.Value);
            if (model.ObjectTag == null)
            {
                if (!types.Contains(model.Type))
                {
                    errorMsg = "无效的角色";
                    return false;
                }

            }
            if (string.IsNullOrEmpty(model.Account))
            {
                errorMsg = "账号不能为空";
                return false;
            }
            var old = GetFirstOrDefault(a => a.Where(b => b.Account == model.Account || b.Mobile == model.Mobile && model.Mobile != null));
            if (old != null)
            {
                errorMsg = "账号或手机号不能重复";
                return false;
            }
            return res;
        }
        public override bool ModifyValidate(out string errorMsg, SysUser model)
        {
            errorMsg = null;
            var res = true;
            var types = EnumConvert.ToList<EnumUserType>().Select(a => (int)a.Value);
            if (model.ObjectTag == null)
            {
                if (!types.Contains(model.Type))
                {
                    errorMsg = "无效的角色";
                    return false;
                }

            }
            if (string.IsNullOrEmpty(model.Account))
            {
                errorMsg = "账号不能为空";
                return false;
            }
            var old = GetFirstOrDefault(a => a.Where(b => b.Id != model.Id && (b.Account == model.Account || b.Mobile == model.Mobile && model.Mobile != null)));
            if (old != null)
            {
                errorMsg = "账号或手机号不能重复";
                return false;
            }
            return res;
        }
        public override bool AddBefore(out string errorMsg, SysUser model, SysUser inModel)
        {
            var res = true;
            errorMsg = null;
            var roleId = 0;
            if (model.ObjectTag == null)
            {
                model.ObjectTag = (EnumUserType)model.Type;
            }
            switch (model.ObjectTag)
            {
                case EnumUserType.Agent:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Testor:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Tested:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Visitor:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Admin:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
            }
            roleId = SysRoleBll.GetRoleIdByEnum((EnumUserType)model.ObjectTag);
            model.Status = (int)EnumEnableState.Enabled;
            model.Password = Crypt.AesEncrypt(model.Password);
            var tempuserInfo = inModel.BusUserInfoUsers.FirstOrDefault() ?? new BusUserInfo { };
            tempuserInfo.User = model;
            tempuserInfo.Owner = model;
            tempuserInfo.CreateTime = DateTime.Now;
            tempuserInfo.CreateUserId = CurrentUserId;
            model.SysUserRoleUsers.Add(new SysUserRole { User = model, RoleId = roleId, CreateUserId = CurrentUserId, CreateTime = DateTime.Now });
            //model.BusUserInfoUsers.Add(tempuserInfo);
            return res;
        }

        public override bool ModifyBefore(out string errorMsg, SysUser model, SysUser inModel, SysUser oldModel)
        {
            var res = true;
            errorMsg = null;
            var roleId = 0;
            if (model.ObjectTag == null)
            {
                model.ObjectTag = (EnumUserType)model.Type;
            }
            switch (model.ObjectTag)
            {
                case EnumUserType.Agent:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Testor:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Tested:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Visitor:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
                case EnumUserType.Admin:
                    {
                        model.Type = (int)model.ObjectTag;
                    }
                    break;
            }
            roleId = SysRoleBll.GetRoleIdByEnum((EnumUserType)model.ObjectTag);
            model.SysUserRoleUsers.Clear();
            model.SysUserRoleUsers.Add(new SysUserRole { User = model, RoleId = roleId, CreateUserId = CurrentUserId, CreateTime = DateTime.Now });
            Context.Entry(model).Property(a => a.Password).IsModified = false;
            var info = model.BusUserInfoUsers.FirstOrDefault(a => a.UserId == model.Id && a.OwnerId == model.Id);
            if (info == null)
            {
                info = new BusUserInfo { UserId = model.Id, OwnerId = model.Id, CreateTime = DateTime.Now, CreateUserId = CurrentUserId };
                model.BusUserInfoUsers.Add(info);
            }
            info.CopyFrom(inModel.BusUserInfoUsers.FirstOrDefault(), a => new { a.CreateTime, a.CreateUserId, a.UserId, a.OwnerId }, new[] { typeof(BaseEntity), typeof(ICollection<>) });
            info.ModifyTime = DateTime.Now;
            info.ModifyUserId = CurrentUserId;
            return res;
        }
        public override Expression<Func<SysUser, object>> ModifyExcepts(SysUser model)
        {
            return a => new { a.Status };
        }
        #endregion

        public Response<SysUser, object> Enable(int id)
        {
            var res = EditProperties(id, null, new { });
            return res;
        }

        #region 身份局增删改查
        public Response<List<SysUser>, object, object, object> GetAgentList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Agent;
            return GetPageList(req);
        }

        public Response<List<SysUser>, object, object, object> GetTestorList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Testor;
            return GetPageList(req);
        }
        public Response<List<SysUser>, object, object, object> GetMyTestorList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Testor|(EnumUserType)(256*256);
            return GetPageList(req);
        }
        public Response<List<SysUser>, object, object, object> GetTestedList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Tested;
            return GetPageList(req);
        }
        public Response<List<SysUser>, object, object, object> GetVisitorList(UserReq.Page req)
        {
            req.Tag = EnumUserType.Visitor;
            return GetPageList(req);

        }
        public Response<SysUser, object, object, object> AddAgent(SysUser model)
        {
            model.ObjectTag = EnumUserType.Agent;
            return Add(model);
        }
        public Response<SysUser, object, object, object> AddTestor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Testor;
            return Add(model);
        }
        public Response<SysUser, object, object, object> AddTested(SysUser model)
        {
            model.ObjectTag = EnumUserType.Tested;
            return Add(model);
        }
        public Response<SysUser, object, object, object> AddVisitor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Visitor;
            return Add(model);
        }

        public Response<SysUser, object, object, object> ModifyAgent(SysUser model)
        {
            model.ObjectTag = EnumUserType.Agent;
            return Modify(model);
        }
        public Response<SysUser, object, object, object> ModifyTestor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Testor;
            return Modify(model);
        }
        public Response<SysUser, object, object, object> ModifyTested(SysUser model)
        {
            model.ObjectTag = EnumUserType.Tested;
            return Modify(model);
        }
        public Response<SysUser, object, object, object> ModifyVisitor(SysUser model)
        {
            model.ObjectTag = EnumUserType.Visitor;
            return Modify(model);
        }
        #endregion

        #region 登录
        public Response<SysUser> Login(HomeReq.Login req)
        {
            var res = new Response<SysUser>();
            var user = GetFirstOrDefault(a=>a.Where(b=>b.Account == req.Account));
            if (user == null)
            {
                res.code =EnumResStatus.Fail;
                res.msg = "账号不存在";
                return res;
            }
            
            if (Crypt.AesDecrypt(user.Password) != req.Password)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "账号密码错误";
                return res;
            }
            if (user.Status == (int)EnumEnableState.Disabled)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "账号已禁用";
                return res;
            }
            if (user.ExpireTime >=DateTime.Now)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "账号已过期";
                return res;
            }
            res.data = user;
            res.msg = "登陆成功";
            return res;
        }
        #endregion

        #region 当前状态
        public static SysUser GetCookie()
        {
            var arr = (HttpContext.Current?.User?.Identity?.Name + "").Split(new[] { '|'},StringSplitOptions.RemoveEmptyEntries);
            if(arr==null || arr.Length < 3)
            {
                return null;
            }
            return new SysUser { Id = Convert.ToInt32(arr[0]),Account = arr[1],Name = arr[2] };
        }
        public static SysUser CurrentUser
        {
            get
            {
                if (!HttpContextCache.CanUse)
                {
                    return null;
                }
                var _currentUser = HttpContextCache.Cache.G<SysUser>("CurrentUser");
                if (_currentUser == null)
                {
                    try
                    {
                        //获取当前登录用户信息
                        var cuser = GetCookie();
                        if (cuser == null) {
                            return null;
                        }
                        var userBll = AutofacExt.GetService<SysUserBll>();
                        _currentUser = userBll.Find(false,cuser.Id);
                        if (_currentUser == null)
                        {
                            return null;
                        }
                        HttpContextCache.Cache["CurrentUser"] = _currentUser;
                    }
                    catch (Exception)
                    {
                        _currentUser = null;
                    }
                }

                return _currentUser;
            }
        }
        #endregion

        #region 页面辅助
        public static string GetAvatar(SysUser user)
        {
            return (!string.IsNullOrWhiteSpace(user?.AvatarName)) ? (user.AvatarName) : (user?.Avatar?.FullName ?? "/static/avatars/defaultavatar.png");
        }
        public static string GetRoleName(SysUser user)
        {
            return user?.SysUserRoleUsers?.OrderBy(a => a.RoleId)?.FirstOrDefault()?.Role?.Name;
        }
        public static string[] GetRoleNames(SysUser user)
        {
            return user?.SysUserRoleUsers?.OrderBy(a => a.RoleId)?.Select(a => a.Role.Name)?.ToArray() ?? new string[0];
        }


        #endregion

        #region 导入
        public readonly Dictionary<string, string> PropColumnMap = new Dictionary<string, string> {
            {"Account" , "登录名"},
            {"Mobile" , "手机号"},
            {"Name" , "用户名"},
            {"Password" , "密码"},
        };
        public readonly Dictionary<string, string> InfoPropColumnMap = new Dictionary<string, string> {
            {"RealName" , "真实姓名"},
            {"Sex" , "性别"},
            {"Birthday" , "生日"},
            {"IdCardNo" , "身份证号"},
            {"UnitName" , "单位名称"},
        };
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Response<List<SysUser>> Import(UserReq.Upload req)
        {
            var res = new Response<List<SysUser>>();
            if (req.Stream == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "未找到文件";
                return res;
            }
            var dt = ExcelHelper.ConvertExcel2DataTable(req.Stream, "xlsx", 0, 0, 1);
            if (dt.Rows.Count <= 0)
            {
                throw new Exception("Excel没有任何数据");
            }
            if (dt.Columns.Count <= 0)
            {
                throw new Exception("Excel没有任何列");
            }
            var cols = new DataColumn[dt.Columns.Count];
            dt.Columns.CopyTo(cols,0);
            var colsList = cols.ToList();
            cols = null;
            var type = typeof(SysUser);
            var typeInfo = typeof(BusUserInfo);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField).ToList();
            var InfoProps = typeInfo.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField).ToList();
            var dicMapIndex = new Dictionary<PropertyInfo, int>();
            props.ForEach(prop => {
                if (!PropColumnMap.ContainsKey(prop.Name))
                {
                    return;
                }
                var key = PropColumnMap[prop.Name];
                var val = colsList.FirstOrDefault(a => a.ColumnName.Contains(key));
                if (val == null)
                {
                    return;
                }
                dicMapIndex[prop] = colsList.IndexOf(val);
            });
            InfoProps.ForEach(prop => {
                if (!InfoPropColumnMap.ContainsKey(prop.Name))
                {
                    return;
                }
                var key = InfoPropColumnMap[prop.Name];
                var val = colsList.FirstOrDefault(a => a.ColumnName.Contains(key));
                if (val == null)
                {
                    return;
                }
                dicMapIndex[prop] = colsList.IndexOf(val);
            });
            if (dicMapIndex.Count != 9)
            {
                throw new Exception("Excel格式错误");
            }

            var userList = new List<SysUser>();
            dt.AsEnumerable().ToList().ForEach((row,rowid)=>{
                var one = new SysUser { };
                var two = new BusUserInfo { };
                one.BusUserInfoUsers=new List<BusUserInfo> { two };
                var dicList = dicMapIndex.ToList();
                dicList.ForEach((prop,index) => {
                    var val = row[prop.Value];
                    var valstr = (val + "").Trim();
                    if (valstr == "")
                    {
                        val = null;
                    }
                    switch (prop.Key.Name)
                    {
                        case "Account": {
                                
                                if (valstr == "")
                                {
                                    throw new Exception($"第{(rowid + 2)}行，登录名不能为空");
                                }
                            }
                            break;
                        case "Name":
                            {
                                if (valstr == "")
                                {
                                    throw new Exception($"第{(rowid + 2)}行，用户名不能为空");
                                }
                            }
                            break;
                        case "Password":
                            {
                                if (valstr.Length<6)
                                {
                                    throw new Exception($"第{(rowid + 2)}行，密码不能少于6位");
                                }
                            }
                            break;
                        case "Sex":
                            {
                                if (valstr == "男")
                                {
                                    val = (int?)EnumSex.Male;
                                }
                                else if (valstr == "女")
                                {
                                    val = (int?)EnumSex.Female;
                                }
                                else if(valstr=="")
                                {
                                    val = (int?)null;
                                }
                                else
                                {
                                    throw new Exception($"第{(rowid + 2)}行，性别不正确，只能输入男或女");
                                }
                            }
                            break;
                        case "Birthday":
                            {
                                if (DateTime.TryParse(valstr, out DateTime resdt))
                                {
                                    val = (DateTime?)resdt;
                                }
                                else if(valstr=="")
                                {
                                    val= (DateTime?)null;
                                }
                                else
                                {
                                    throw new Exception($"第{(rowid + 2)}行，生日格式错误，日期转化失败");
                                }

                            }
                            break;

                    }
                    if (index < 4)
                    {
                        prop.Key.SetValue(one, val);
                    }
                    else
                    {
                        prop.Key.SetValue(two, val);
                    }
                    
                });

                userList.Add(one);
                one.ObjectTag = (rowid + 2);
            });
            var accountError = userList.GroupBy(a => a.Account).Select(a => a.ToList()).Where(a => a.Count > 1).OrderBy(a => a.Min(b => (int)b.ObjectTag)).FirstOrDefault();
            if (accountError != null)
            {
                throw new Exception($"第（{string.Join(",", accountError.Select(a => a.ObjectTag))}）行，登录名（{accountError[0].Account}）重复");
            }
            var accounts = userList.Select(a=>a.Account).ToArray();
            var exists = GetListFilter(a => a.Where(b => accounts.Contains(b.Account))).Select(a => a.Account).ToArray();
            if (exists.Length > 0)
            {
                var firstExists = userList.Where(a => exists.Contains(a.Account)).OrderBy(a => (int)a.ObjectTag).FirstOrDefault();
                throw new Exception($"第（{firstExists.ObjectTag}）行，登录名（{firstExists.Account}）已存在");
            }
            using(var trans = Context.Database.BeginTransaction())
            {
                userList.ForEach((user, index) =>
                {
                    var hang = (int)user.ObjectTag;
                    user.ObjectTag = req.UserType;
                    var ret = Add(user);
                    if (ret.code != EnumResStatus.Succ)
                    {
                        throw new Exception($"第{hang}行导入失败："+ret.msg);
                    }
                });
                trans.Commit();
            }
            

            return res;
        }
        #endregion

    }
}
