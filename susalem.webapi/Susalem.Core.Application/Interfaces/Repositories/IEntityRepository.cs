using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces.Repositories
{
    public interface IEntityRepository<T, TId>:IReadRepository<T>, IWriteRepository<T> where T:class,IEntity<TId>
    {
        T Find(TId id);

        Task<T> FindAsync(TId id);
    }
}
