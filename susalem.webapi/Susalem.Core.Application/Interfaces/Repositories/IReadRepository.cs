using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository interface for read operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadRepository<T>
    {
        bool Exists(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        T GetFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    }
}
