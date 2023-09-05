﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using AiBi.Test.Dal.Model;
using System.Data.Entity;
using AiBi.Test.Common;

namespace AiBi.Test.Bll
{
    public abstract class BaseBll<T, PageReqT> where T:BaseEntity where PageReqT:PageReq
    {
        #region 依赖
        /// <summary>
        /// 上下文
        /// </summary>
        public TestContext Context { get; set; }
        #endregion

        #region 抽象方法

        #endregion


        #region 分页
        public virtual IQueryable<T> PageWhereKeyword(PageReqT req, IQueryable<T> query)
        {
            if (string.IsNullOrEmpty(req.keyword))
            {
                return query;
            }
            var where = (Expression<Func<T, bool>>)(a => false);
            if (TypeHelper.HasProperty<T>("Title"))
            {
                where = where.Or(PredicateBuilder.DotExpression<T,string>("Title").Like(req.keyword));
            }
            if (TypeHelper.HasProperty<T>("Name"))
            {
                where = where.Or(PredicateBuilder.DotExpression<T, string>("Name").Like(req.keyword));
            }
            if (TypeHelper.HasProperty<T>("Remark"))
            {
                where = where.Or(PredicateBuilder.DotExpression<T, string>("Mobile").Like(req.keyword));
            }
            return query.Where(where);
        }
        /// <summary>
        /// 分页条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IQueryable<T> PageWhere(PageReqT req, IQueryable<T> query)
        {
            query = PageWhereKeyword(req, query);
            return query;
        }
        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IQueryable<T> PageOrder(PageReqT req, IQueryable<T> query)
        {
            return query.OrderByDescending(a=>a.CreateTime);
        }
        /// <summary>
        /// 分页完成后处理数据
        /// </summary>
        /// <param name="req"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual List<T> PageAfter(PageReqT req, List<T> list)
        {
            return list;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<T> GetPageList(PageReqT req)
        {

            var list = GetListPage(out int count, req.Page, req.Size, a => PageWhere(req, a), a => PageOrder(req, a));

            req.OutCount = count;
            list = PageAfter(req, list);
            return list;
        }
        #endregion

        #region lambda查询
        public IQueryable<T> getListQuery(Func<IQueryable<T>, IQueryable<T>> where,bool notracking)
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
            return where==null?query: where(query);
        }
        
        public List<T> GetListPage(out int count,int page,int size,Func<IQueryable<T>, IQueryable<T>> where=null, Func<IQueryable<T>, IQueryable<T>> order=null)
        {
            var query = getListQuery(where,true);
            
            count = query.Count();
            if (count == 0)
            {
                return new List<T>();
            }
            if (order == null)
            {
                query = query.OrderByDescending(a=>a.CreateTime);
            }
            else
            {
                query = order(query);
            }
            return query.Skip(size * (page - 1)).Take(size).ToList();
        }
        public List<T> GetListFilter(Func<IQueryable<T>, IQueryable<T>> where)
        {
            return GetListFilter(where,null,true);
        }
        public List<T> GetListFilter(Func<IQueryable<T>, IQueryable<T>> where, Func<IQueryable<T>, IQueryable<T>> order,bool notracking)
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
        public T GetFirstOrDefault(Func<IQueryable<T>, IQueryable<T>> where, bool notracking=true)
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
        public T Find(params object[] keys)
        {
            return Find(true,keys);
        }
        public  T Find(bool notrack,params object[] keys) 
        {
            var obj = Context.Set<T>().Find(keys);
            if (obj == null)
            {
                return obj;
            }
            if (notrack)
            {
                Context.Entry(obj).State = EntityState.Detached;
            }
            return obj;
        }
        #endregion
    }
}
