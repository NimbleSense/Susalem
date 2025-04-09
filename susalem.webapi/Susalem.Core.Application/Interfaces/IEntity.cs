namespace Susalem.Core.Application.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
