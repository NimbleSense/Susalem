using Susalem.Core.Application.Interfaces;

namespace Susalem.Infrastructure.Models
{
    /// <summary>
    /// Base data entity class to be inherited from every entity in database context.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class DataEntityBase<TId> : IDataEntity<TId>
    {
        public TId Id { get; set; }
    }
}
