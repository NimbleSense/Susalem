using System.Threading;
using System.Threading.Tasks;
using Susalem.Core.Application.Interfaces;
using MassTransit.Mediator;

namespace Susalem.Infrastructure.Services
{
    public class ServiceBusMediator : IServiceBus
    {
        private readonly IMediator _mediator;

        public ServiceBusMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class
        {
            var client = _mediator.CreateRequestClient<IRequest<TResponse>>();
            cancellationToken.ThrowIfCancellationRequested();
            var response = await client.GetResponse<TResponse>(request, cancellationToken);
            return response.Message;
        }

        public async Task Publish(INotification notification, CancellationToken cancellationToken = default)
        {
            
            await _mediator.Publish(notification, cancellationToken);
        }
    }
}
