using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoSync.EFC.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        T Get(Expression<Func<T, bool>> where);
        void Insert(T entity);
        void Insert(List<T> entity);
        void Update(T entity);
        void Update(List<T> entity);
        void Delete(T entity);
        void Delete(List<T> entity);
        IQueryable<T> QueryAll();
        IQueryable<T> Where(Expression<Func<T, bool>> where);
        bool Any(Expression<Func<T, bool>> where);
    }
}
