using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using AiBi.Test.Dal.Model;
using System.Data.Entity;
using AiBi.Test.Common;
using System.Data.Entity.Infrastructure;
using AiBi.Test.Dal.Enum;
using System.Collections;
using System.Web.Mvc;

namespace AiBi.Test.Bll
{
    public abstract class BaseBll<T, PageReqT> where T : BaseEntity where PageReqT : PageReq
    {
        #region 依赖
        /// <summary>
        /// 上下文
        /// </summary>
        public TestContext Context { get; set; }

        public EnumDeleteFilterMode DelFilterMode { get; set; } = EnumDeleteFilterMode.Normal;
        #endregion

        #region 抽象方法

        #endregion


        #region 分页
        public virtual Expression<Func<T, bool>> PageWhereKeyword(PageReqT req)
        {
            var where = (Expression<Func<T, bool>>)(a => false);
            if (string.IsNullOrEmpty(req.keyword))
            {
                return where.Or(a=>true);
            }
            
            if (TypeHelper.HasPropertyBase<T>("Title"))
            {
                where = where.Or(PredicateBuilder.DotExpression<T, string>("Title").Like(req.keyword));
            }
            if (TypeHelper.HasPropertyBase<T>("Name"))
            {
                where = where.Or(PredicateBuilder.DotExpression<T, string>("Name").Like(req.keyword));
            }
            if (TypeHelper.HasPropertyBase<T>("Remark"))
            {
                where = where.Or(PredicateBuilder.DotExpression<T, string>("Mobile").Like(req.keyword));
            }
            if (TypeHelper.HasPropertyBase<T>("Keys"))
            {
                var tkey = req.keyword.Replace("|","");
                where = where.Or(PredicateBuilder.DotExpression<T,string>("Keys").Like(tkey));

            }
            where = where.Or(a => a.CreateUser.Name.Contains(req.keyword));
            return where;
        }
        /// <summary>
        /// 分页条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IQueryable<T> PageWhere(PageReqT req, IQueryable<T> query)
        {
            query = query.Where(PageWhereKeyword(req));
            if (req.Where != null)
            {
                foreach (var where in req.Where)
                {
                    if (!TypeHelper.HasProperty<T>(where.Key))
                    {
                        query = PageOrderCustom(req, query);
                        continue;
                    }
                    query = query.EqualTo(where.Key,where.Value);
                }
            }
            return query;
        }
        public virtual IQueryable<T> PageWhereCustom(PageReqT req, IQueryable<T> query)
        {
            return query;
        }
        public virtual IQueryable<T> DefOrderBy(PageReqT req, IOrderedQueryable<T> query) {
            if (TypeHelper.HasPropertyBase<T>("Id"))
            {
                return query.ThenByDescending("Id");
            }
            return query.ThenByDescending(a=>a.CreateTime);
        }
        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IQueryable<T> PageOrder(PageReqT req, IQueryable<T> query)
        {
            IOrderedQueryable<T> sortQuery = query.OrderBy(a => 0);
            if (req.Sort != null)
            {
                foreach (var sort in req.Sort)
                {
                    if (!TypeHelper.HasProperty<T>(sort.Key))
                    {
                        query = PageOrderCustom(req,query);
                        continue;
                    }
                    if (sort.Value)
                    {
                        sortQuery = sortQuery.ThenBy(sort.Key);
                    }
                    else
                    {
                        sortQuery = sortQuery.ThenByDescending(sort.Key);
                    }
                }
            }
            return DefOrderBy(req, sortQuery);
        }
        public virtual IQueryable<T> PageOrderCustom(PageReqT req, IQueryable<T> query)
        {
            return query;
        }
        /// <summary>
        /// 分页完成后处理数据
        /// </summary>
        /// <param name="req"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual void PageAfter(PageReqT req, Response<List<T>, object, object, object> res)
        {
            
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Response<List<T>, object, object, object> GetPageList(PageReqT req)
        {
            var res = new Response<List<T>, object, object, object>();
            var list = GetListPage(out int count, req.Page, req.Size, a => PageWhere(req, a), a => PageOrder(req, a));

            res.count = count;
            res.data = list;
            PageAfter(req, res);
            return res;
        }
        #endregion

        #region 详情接口
        public virtual void ByKeysAfter(Response<T, object, object, object> res, params object[] keys)
        {

        }
        public Response<T, object, object, object> GetByKeys(params object[] keys)
        {
            var res = new Response<T, object, object, object>();
            res.data = Find(false, keys);
            if (res.data == null)
            {
                res.code = EnumResStatus.Fail;
                res.msg = "您查询的数据不存在";
                return res;
            }
            ByKeysAfter(res, keys);
           
            return res;
        }
        #endregion

        #region lambda查询
        private IQueryable<T> getListQuery(Func<IQueryable<T>, IQueryable<T>> where, bool notracking)
        {
            IQueryable<T> query;
            if (notracking)
            {
                query = Context.Set<T>().AsNoTracking().AsQueryable();
            }
            else
            {
                query = Context.Set<T>().AsQueryable();
            }
            query = where == null ? query : where(query);
            switch (DelFilterMode)
            {
                case EnumDeleteFilterMode.Normal:
                    {
                        query = query.Where(a => !a.IsDel);
                    }
                    break;
                case EnumDeleteFilterMode.Deleted:
                    {
                        query = query.Where(a => a.IsDel);
                    }
                    break;
                case EnumDeleteFilterMode.All:
                    { }
                    break;
            }
            return query;
        }

        public List<T> GetListPage(out int count, int page, int size, Func<IQueryable<T>, IQueryable<T>> where = null, Func<IQueryable<T>, IQueryable<T>> order = null,bool notracking=false)
        {
            var query = getListQuery(where, notracking);

            count = query.Count();
            if (count == 0)
            {
                return new List<T>();
            }
            if (order == null)
            {
                query = DefOrderBy(null,query.OrderBy(a=>0));
            }
            else
            {
                query = order(query);
            }
            return query.Skip(size * (page - 1)).Take(size).ToList();
        }
        public List<T> GetListFilter(Func<IQueryable<T>, IQueryable<T>> where)
        {
            return GetListFilter(where, null, true);
        }
        public List<T> GetListFilter(Func<IQueryable<T>, IQueryable<T>> where, Func<IQueryable<T>, IQueryable<T>> order, bool notracking)
        {
            var query = getListQuery(where, notracking);

            if (order == null)
            {
                query = query.OrderByDescending(a => a.CreateTime);
            }
            else
            {
                query = order(query);
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(Func<IQueryable<T>, IQueryable<T>> where, bool notracking = true)
        {
            return GetFirstOrDefault(where, null, notracking);
        }
        public T GetFirstOrDefault(Func<IQueryable<T>, IQueryable<T>> where, Func<IQueryable<T>, IQueryable<T>> order, bool notracking)
        {
            var query = getListQuery(where, notracking);

            if (order == null)
            {
                query = query.OrderByDescending(a => a.CreateTime);
            }
            else
            {
                query = order(query);
            }
            return query.FirstOrDefault();
        }
        public List<T> ByIds(int[] Ids, Expression<Func<T, int>> expr=null) {
            return ByIds(true,Ids,expr);
        }
        public List<T> ByIds(bool notracking, int[] Ids, Expression<Func<T, int>> expr)
        {
            if (!typeof(T).IsSubclassOf(typeof(IdEntity)) && expr == null || Ids == null || Ids.Length<=0) {
                return new List<T>();
            }
            if(typeof(T).IsSubclassOf(typeof(IdEntity)))
            {
                expr = PredicateBuilder.DotExpression<T, int>("Id");
            }
            var where = PredicateBuilder.DotExpression<T, int>("Id").In(Ids);
            return GetListFilter(a => a.Where(where), a => a.OrderByDescending(b => b.CreateTime), notracking);
        }
        public T Find(params object[] keys)
        {
            return Find(true, keys);
        }
        public T Find(bool notracking, params object[] keys)
        {
            if (keys == null ) return null;
            keys = keys.Where(a => a != null).ToArray();
            if ( keys.Length <= 0) return null;
            keys = convertKeyType(keys);
            keys = keys.Select(a => a).ToArray();
            var obj = Context.Set<T>().Find(keys);
            if (obj == null)
            {
                return obj;
            }
            if (notracking)
            {
                Context.Entry(obj).State = EntityState.Detached;
            }
            switch (DelFilterMode)
            {
                case EnumDeleteFilterMode.Normal:
                    {
                        obj = obj.IsDel ? null : obj;
                    }
                    break;
                case EnumDeleteFilterMode.Deleted:
                    {
                        obj = (!obj.IsDel) ? null : obj;
                    }
                    break;
                case EnumDeleteFilterMode.All:
                    { }
                    break;
            }
            return obj;
        }
        private object[]  convertKeyType(params object[] keys) {
            var objset = (Context as IObjectContextAdapter).ObjectContext.CreateObjectSet<T>();
            int i = 0;
            var keyTypes = objset.EntitySet.ElementType.KeyProperties.Select(a => {
                var type = TypeHelper.GetPropertyBase<T>(a.Name).PropertyType;
                var res = Convert.ChangeType(keys[i],type,null);
                i++;
                return res;
            }).ToArray();
            return keyTypes;
        }

        /// <summary>
        /// include一下
        /// </summary>
        /// <returns></returns>
        protected static IQueryable<T> getIncludeQuery<TKey>(IQueryable<T> query,Expression<Func<T, TKey>> includeSelector)
        {
            if (includeSelector == null)
            {
                return query;
            }
            if (includeSelector.Body.NodeType != ExpressionType.New && includeSelector.Body.NodeType != ExpressionType.MemberAccess)
            {
                return query;
            }
            if (includeSelector.Body.NodeType == ExpressionType.MemberAccess)
            {
                var path = getMembersPath(includeSelector.Body);
                if (string.IsNullOrEmpty(path)) return query;
                return query.Include(path);
            }
            foreach (var arg in ((NewExpression)includeSelector.Body).Arguments)
            {
                var path = getMembersPath(arg);
                if (string.IsNullOrEmpty(path)) continue;
                query = query.Include(path);
            }
            return query;
        }
        /// <summary>
        /// 获取include路径
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private static string getMembersPath(Expression expr)
        {
            if (!(expr is MemberExpression)) return null;
            var exprList = new List<Expression> { expr };
            var path = "";
            while (exprList.Count > 0)
            {
                var last = exprList[exprList.Count - 1];
                exprList.RemoveAt(exprList.Count - 1);
                if (last is MemberExpression)
                {
                    var memExpr = (last as MemberExpression);
                    path = "." + memExpr.Member.Name + path;
                    exprList.Add(memExpr.Expression);
                }
                else
                {
                    var children = last.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Where(a => a.PropertyType.IsAssignableFrom(typeof(Expression))).OrderBy(a => a.PropertyType.IsAssignableFrom(typeof(MemberExpression)) ? 1 : 0).Select(a => (Expression)a.GetValue(last)).ToList();
                    exprList.AddRange(children);
                }

            }
            if (path.StartsWith("."))
            {
                path = path.Substring(1);
            }
            return path;
        }
        #endregion
    }

    public static class BaseEntityEx
    {
        public static T LoadChild<T>(this T obj, Func<T, object> func) where T : BaseEntity
        {
            if (func != null)
            {
                obj.ObjectTag = func(obj);
            }
            //var context = AutofacExt.GetService<TestContext>();
            //(context as IObjectContextAdapter).ObjectContext.ContextOptions.LazyLoadingEnabled = false;

            return obj;
        }
    }
}
