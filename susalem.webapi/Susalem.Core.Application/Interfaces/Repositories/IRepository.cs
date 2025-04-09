namespace Susalem.Core.Application.Interfaces.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
