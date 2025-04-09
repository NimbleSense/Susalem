using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Infrastructure.Models;
using Susalem.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Susalem.Infrastructure.Repositories
{
    internal class DataEntityRepository<T,TId>: IEntityRepository<T,TId> where T:DataEntityBase<TId>
    {
        private readonly DbSet<T> _entities;

        private readonly ApplicationDbContext _context;

        public DataEntityRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _entities.Any(predicate);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return _entities.AnyAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return GetEntities(false);
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return GetEntities().Where(predicate);
        }

        public T GetFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return GetEntities(true).FirstOrDefault(predicate);
        }

        public Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return GetEntities().FirstOrDefaultAsync(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return GetEntities().SingleOrDefault(predicate);
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return GetEntities().SingleOrDefaultAsync(predicate);
        }

        public IUnitOfWork UnitOfWork => _context;

        public T Find(TId id)
        {
            return _entities.Find(id);
        }

        public async Task<T> FindAsync(TId id)
        {
            return await _entities.FindAsync(id);
        }

        public void Add(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            _entities.Add(entity);
        }

        public async ValueTask AddAsync(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            await _entities.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return _entities.AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            _entities.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            Guard.Against.Null(entities, nameof(entities));
            _entities.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            Guard.Against.Null(entity, nameof(entity));
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            Guard.Against.Null(entities, nameof(entities));
            _entities.UpdateRange(entities);
        }

        private IQueryable<T> GetEntities(bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return _entities.AsNoTracking();
            }

            return _entities;
        }
    }

    public static class IncludeExtension
    {
        public static IQueryable<TEntity> Include<TEntity>(this DbSet<TEntity> dbSet,
            params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            IQueryable<TEntity> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }
            return query ?? dbSet;
        }
    }
}
