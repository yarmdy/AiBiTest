using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using AiBi.Test.Dal.Model;
using System.Data.Entity;

namespace AiBi.Test.Bll
{
    public abstract class BaseBll<T> where T:BaseEntity
    {
        public TestContext Context { get; set; }

        #region lambda查询
        /// <summary>
        /// 获取列表query
        /// </summary>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private IQueryable<T> getListQuery(Expression<Func<T, object>> includeSelector, Expression<Func<T, bool>> predicate) 
        {
            var query = getIncludeQuery(includeSelector);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        private  IQueryable<T> getFilterQuery<TKey>( Expression<Func<T, object>> includeSelector, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderSelector, bool asc = false)
        {
            var query = getListQuery(includeSelector, predicate);
            if (orderSelector == null)
            {
                query = asc ? query.OrderBy(a => a.CreateTime) : query.OrderByDescending(a => a.CreateTime);
            }
            else
            {
                query = asc ? query.OrderBy(orderSelector) : query.OrderByDescending(orderSelector);
            }

            return query;
        }

        /// <summary>
        /// include一下
        /// </summary>
        /// <returns></returns>
        private IQueryable<T> getFullQuery()
        {
            var iquery = Context.Set<T>().AsQueryable();
            var otype = typeof(T);
            otype.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty).Where(a => a.PropertyType.IsInstanceOfType(typeof(BaseEntity))).Select(a => {
                iquery = iquery.Include(a.Name);
                return 0;
            });
            return iquery;
        }
        /// <summary>
        /// include一下
        /// </summary>
        /// <returns></returns>
        private IQueryable<T> getIncludeQuery<TKey>(Expression<Func<T, TKey>> includeSelector)
        {
            var query = Context.Set<T>().AsQueryable();
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
        private  string getMembersPath(Expression expr)
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

        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <typeparam name="T">对应实体</typeparam>
        /// <typeparam name="TKey">排序对应字段类型</typeparam>
        /// <param name="app">对应app</param>
        /// <param name="includeSelector">包含什么表</param>
        /// <param name="predicate">条件where</param>
        /// <param name="orderSelector">排序方法</param>
        /// <param name="asc">是否正序</param>
        /// <param name="onlySearch">只查询不进入ef缓存</param>
        /// <returns></returns>
        public  T GetFirstOrDefault<TKey>( Expression<Func<T, object>> includeSelector, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderSelector, bool asc = false, bool onlySearch = false)
        {
            var query = getFilterQuery(includeSelector, predicate, orderSelector, asc);
            if (onlySearch)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefault();

        }
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="app">app</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public  T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetFirstOrDefault<object>(null, predicate, null);
        }
        /// <summary>
        /// 获取单个实例 不走ef缓存
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="app">app</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public  T GetFirstOrDefaultNoTracking(Expression<Func<T, bool>> predicate)
        {
            return GetFirstOrDefault<object>(null, predicate, null, false, true);
        }
        /// <summary>
        /// 获取过滤列表
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderSelector"></param>
        /// <returns></returns>
        public  List<T> GetListFilter<TKey>(Expression<Func<T, object>> includeSelector, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderSelector, bool asc = false, bool onlySearch = false)
        {
            var query = getFilterQuery(includeSelector, predicate, orderSelector, asc);
            if (onlySearch)
            {
                query = query.AsNoTracking();
            }
            return query.ToList();

        }
        /// <summary>
        /// 获取过滤列表
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderSelector"></param>
        /// <returns></returns>
        public  List<T> GetListFilter(Expression<Func<T, bool>> predicate)
        {
            return GetListFilter<object>(null, predicate, null);

        }
        /// <summary>
        /// 获取过滤列表 不走ef缓存
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderSelector"></param>
        /// <returns></returns>
        public  List<T> GetListFilterNoTracking(Expression<Func<T, bool>> predicate)
        {
            return GetListFilter<object>(null, predicate, null, false, true);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderSelector"></param>
        /// <returns></returns>
        public  List<T> GetListPage<TKey>(out int count, int page, int limit, Expression<Func<T, object>> includeSelector, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderSelector, bool asc = false)
        {
            var query = getListQuery(includeSelector, predicate).AsNoTracking();

            count = query.Count();
            if (count <= 0)
            {
                return new List<T>();
            }
            if (orderSelector == null)
            {
                return asc ? query.OrderBy(a => a.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList() : query.OrderByDescending(a => a.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();

            }
            return asc ? query.OrderBy(orderSelector).Skip(limit * (page - 1)).Take(limit).ToList() : query.OrderByDescending(orderSelector).Skip(limit * (page - 1)).Take(limit).ToList();
        }
        /// <summary>
        /// 高级排序分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="app"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <param name="funcOrder"></param>
        /// <returns></returns>
        public  List<T> GetListPageOrder(out int count, int page, int limit, Expression<Func<T, object>> includeSelector, Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> funcOrder)
        {
            var query = getListQuery(includeSelector, predicate).AsNoTracking();
            count = query.Count();
            if (count <= 0)
            {
                return new List<T>();
            }
            if (funcOrder == null)
            {
                if (limit > 0)
                    query.OrderByDescending(a => a.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList();
                else
                    query.OrderByDescending(a => a.CreateTime).ToList();
            }

            if (limit > 0)
                return funcOrder(query).Skip(limit * (page - 1)).Take(limit).ToList();
            else
                return funcOrder(query).ToList();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="includeSelector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderSelector"></param>
        /// <returns></returns>
        public  List<T> GetListPage(out int count, int page, int limit, Expression<Func<T, bool>> predicate)
        {
            return GetListPage<object>(out count, page, limit, null, predicate, null, false);
        }

        public  T GetByPrimaryKey(params object[] keys) 
        {
            return Context.Set<T>().Find(keys);
        }

        #endregion
    }
}
