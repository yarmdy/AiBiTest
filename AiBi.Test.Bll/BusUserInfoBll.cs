using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web.Security;
using System.Web.Razor.Generator;
using System.Data;
using System.Reflection;

namespace AiBi.Test.Bll
{
    public partial class BusUserInfoBll : BaseBll<BusUserInfo, UserInfoReq.Page>
    {
        public BusUserGroupBll BusUserGroupBll { get; set; }
        public override IQueryable<BusUserInfo> PageWhere(UserInfoReq.Page req, IQueryable<BusUserInfo> query)
        {
            query = GetIncludeQuery( 
                base.PageWhere(req, query)
                    .Where(a => a.OwnerId == CurrentUserId)
                , a => new { a.User,a.User.Avatar}
                );
            if (req.GroupId != null)
            {
                var groupIds = BusUserGroupBll.GetChildrenIds(req.GroupId.Value);
                query = query.Where(a=> groupIds.Contains(a.GroupId.Value));
            }
            return query;
        }
        public override void PageAfter(UserInfoReq.Page req, Response<List<BusUserInfo>, object, object, object> res)
        {
            Context.Configuration.LazyLoadingEnabled = false;
            res.data.ForEach(a => {
                a.Owner = null;
                a.CreateUser = null;
                a.ModifyUser = null;
                a.DelUser = null;
                a.User.CreateUser = null;
                a.User.DelUser = null;
                a.User.ModifyUser = null;
                a.User.BusUserInfoUsers = null;
            });
        }
        public override Expression<Func<BusUserInfo, bool>> PageWhereKeyword(UserInfoReq.Page req)
        {
            var query = base.PageWhereKeyword(req);
            if (string.IsNullOrEmpty(req.keyword))
            {
                return query;
            }
            return query.Or(a=>
            a.User.Name.Contains(req.keyword)
            ||a.User.Account.Contains(req.keyword)
            ||a.User.Mobile.Contains(req.keyword)
            ||a.RealName.Contains(req.keyword)
            ||a.IdCardNo.Contains(req.keyword));
        }

        public override void DetailAfter(int id, int? id2, Response<BusUserInfo, object, object, object> res)
        {
            res.data.LoadChild(a => new { a.User.Avatar,a.UserGroup});
        }

        public override bool AddValidate(out string errorMsg, BusUserInfo model)
        {
            errorMsg = "";
            if (model.User == null)
            {
                errorMsg = "非法请求";
                return false;
            }
            if ((model.User.Password + "").Length < 6)
            {
                errorMsg = "密码必须大于6位";
                return false;
            }
            var SysUserBll = AutofacExt.GetService<SysUserBll>();
            var users = SysUserBll.GetListFilter(a => a.Where(b => b.Id != model.UserId && (b.Account == model.User.Account || b.Mobile == model.User.Mobile && b.Mobile != null)),null,false);
            if (users.Count >1)
            {
                errorMsg = "登录名和手机号对应多个现有用户，添加失败";
                return false;
            }
            if (users.Count <= 0)
            {
                
                users = new List<SysUser> { new SysUser { Password=Crypt.AesEncrypt(model.User.Password),Account=model.User.Account,Name=model.User.Account,Mobile=model.User.Mobile,Type=(int)EnumUserType.Tested,Status=1,CreateTime=DateTime.Now,CreateUserId=CurrentUserId} };
                var mainInfo = new BusUserInfo { }.CopyFrom(model);
                mainInfo.User = users[0];
                mainInfo.Owner = users[0];
                mainInfo.CreateTime = DateTime.Now;
                mainInfo.CreateUserId = CurrentUserId;
                Context.BusUserInfos.Add(mainInfo);
            }
            else
            {
                var tmpinfo = users[0].BusUserInfoUsers.FirstOrDefault(a => a.OwnerId == CurrentUserId);
                if (tmpinfo != null)
                {
                    errorMsg = "用户已存在，添加失败";
                    return false;
                }
            }
            var user = users[0];
            var roleId = SysRoleBll.GetRoleIdByEnum(EnumUserType.Tested);
            if (!user.SysUserRoleUsers.Any(a => a.RoleId == roleId))
            {
                user.SysUserRoleUsers.Add(new SysUserRole { User = users[0], RoleId = roleId, CreateUserId = CurrentUserId, CreateTime = DateTime.Now });
            }
            model.User = user;
            model.OwnerId = CurrentUserId;

            return true;
        }
        public override bool AddBefore(out string errorMsg, BusUserInfo model, BusUserInfo inModel)
        {
            return base.AddBefore(out errorMsg, model, inModel);
        }

        public override bool ModifyValidate(out string errorMsg, BusUserInfo model)
        {
            errorMsg = "";
            if (model.User == null) {
                errorMsg = "非法请求";
                return false;
            }
            return true;
        }
        public override bool ModifyBefore(out string errorMsg, BusUserInfo model, BusUserInfo inModel, BusUserInfo oldModel)
        {
            errorMsg = "";
            if(!model.User.DiffCopy(inModel.User,a=>new { a.Account, a.Mobile }))
            {
                goto next;
            }
            var SysUserBll = AutofacExt.GetService<SysUserBll>();
            var exists = SysUserBll.GetListFilter(a=>a.Where(b=>b.Id!=model.UserId&&(b.Account==model.User.Account || b.Mobile==model.User.Mobile && b.Mobile!=null)));
            if (exists.Count <= 0)
            {
                goto next;
            }
            if (exists.Any(a => a.Account == model.User.Account))
            {
                errorMsg = "登录名已存在，无法重命名";
                return false;
            }
            if (exists.Any(a => a.Mobile == model.User.Mobile))
            {
                errorMsg = "手机号已存在，无法修改";
                return false;
            }

            next:


            return true;
        }

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
            {"ObjectTag" , "分组"},
        };
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Response<List<BusUserInfo>> Import(UserInfoReq.Upload req)
        {
            var res = new Response<List<BusUserInfo>>();
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
            dt.Columns.CopyTo(cols, 0);
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
            if (dicMapIndex.Count != 10)
            {
                throw new Exception("Excel格式错误");
            }

            var userList = new List<BusUserInfo>();
            dt.AsEnumerable().ToList().ForEach((row, rowid) => {
                var one = new SysUser { };
                var two = new BusUserInfo { };
                two.User = one;
                var dicList = dicMapIndex.ToList();
                dicList.ForEach((prop, index) => {
                    var val = row[prop.Value];
                    var valstr = (val + "").Trim();
                    if (valstr == "")
                    {
                        val = null;
                    }
                    switch (prop.Key.Name)
                    {
                        case "Account":
                            {

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
                                if (valstr.Length < 6)
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
                                else if (valstr == "")
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
                                else if (valstr == "")
                                {
                                    val = (DateTime?)null;
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

                userList.Add(two);
                one.ObjectTag = (rowid + 2);
            });
            var accountError = userList.GroupBy(a => a.User.Account).Select(a => a.ToList()).Where(a => a.Count > 1).OrderBy(a => a.Min(b => (int)b.User.ObjectTag)).FirstOrDefault();
            if (accountError != null)
            {
                throw new Exception($"第（{string.Join(",", accountError.Select(a => a.User.ObjectTag))}）行，登录名（{accountError[0].User.Account}）重复");
            }
            var accounts = userList.Select(a => a.User.Account).ToArray();
            var exists = GetListFilter(a => a.Where(b => accounts.Contains(b.User.Account))).Select(a => a.User.Account).ToArray();
            if (exists.Length > 0)
            {
                var firstExists = userList.Where(a => exists.Contains(a.User.Account)).OrderBy(a => (int)a.User.ObjectTag).FirstOrDefault();
                throw new Exception($"第（{firstExists.User.ObjectTag}）行，登录名（{firstExists.User.Account}）已存在");
            }
            Context.Configuration.LazyLoadingEnabled = false;
            var groupBll = AutofacExt.GetService<BusUserGroupBll>();
            var groupList = groupBll.GetListFilter(a => a.Where(b => b.CreateUserId == CurrentUserId), null, false);
            groupList.ForEach(a => {
                a.Parent = groupList.FirstOrDefault(b => b.Id == a.ParentId);
                a.Children = groupList.Where(b => b.ParentId == a.Id).OrderBy(b=>b.SortNo).ToList();
            });
            groupList = groupList.Where(a=>a.ParentId==null).OrderBy(a=>a.SortNo).ToList();
            using (var trans = Context.Database.BeginTransaction())
            {
                userList.ForEach((userInfo, index) =>
                {
                    var group = (userInfo.ObjectTag + "").Trim();
                    var groups = group.Split(new[] { '/'},StringSplitOptions.RemoveEmptyEntries);
                    BusUserGroup localGroup = null;
                    groups.ForEach((g, i) => {
                        var children = localGroup == null ? groupList : localGroup.Children;
                        var nextGroup = children.FirstOrDefault(a=>a.Name==g);
                        if (nextGroup == null)
                        {
                            nextGroup = new BusUserGroup();
                            nextGroup.Name = g;
                            nextGroup.Parent = localGroup;
                            nextGroup.SortNo = children.Count;
                            nextGroup.CreateUserId = CurrentUserId;
                            nextGroup.CreateTime = DateTime.Now;
                            children.Add(nextGroup);
                        }
                        localGroup = nextGroup;
                    });
                    userInfo.UserGroup = localGroup;
                    var hang = (int)userInfo.User.ObjectTag;
                    userInfo.User.ObjectTag = EnumUserType.Tested;
                    var ret = Add(userInfo);
                    if (ret.code != EnumResStatus.Succ)
                    {
                        throw new Exception($"第{hang}行导入失败：" + ret.msg);
                    }
                });
                trans.Commit();
            }


            return res;
        }
        #endregion
    }
}
