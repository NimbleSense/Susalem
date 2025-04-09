using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces
{
    public interface IServiceBus
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            where TResponse : class;

        Task Publish(INotification notification, CancellationToken cancellationToken = default);
    }
}
