namespace Susalem.Core.Application.Interfaces
{
    /// <summary>
    /// Generic marker interface for a data model.
    /// Used to specifically identify data(persistence-related) models.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IDataEntity<TId>:IEntity<TId>
    {
    }
}
