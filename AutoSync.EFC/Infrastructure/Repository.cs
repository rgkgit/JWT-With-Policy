using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoSync.EFC.Infrastructure
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        private readonly AutoSyncDbContext _dbContext;
        private DbSet<T> entities;

        public Repository(AutoSyncDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return entities.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return entities.Where(where).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
        }
        public void Insert(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
        }

        public void Update(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.UpdateRange(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public void Delete(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.RemoveRange(entity);
        }

        public IQueryable<T> QueryAll()
        {
            return entities;
        }
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                GC.SuppressFinalize(_dbContext);
            }
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return entities.Where(where);
        }
        public bool Any(Expression<Func<T, bool>> where)
        {
            return entities.Any(where);
        }
    }
}
