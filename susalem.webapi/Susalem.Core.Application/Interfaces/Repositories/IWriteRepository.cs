using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository interface for write operations.
    /// Includes a IUnitOfWork
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWriteRepository<T> :IRepository
    {
        void Add(T entity);

        ValueTask AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);
    }
}
