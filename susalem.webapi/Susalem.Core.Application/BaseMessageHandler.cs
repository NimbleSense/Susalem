using System.Threading.Tasks;
using Susalem.Core.Application.Interfaces;
using MassTransit;

namespace Susalem.Core.Application
{
    public abstract class BaseMessageHandler<TRequest,TResponse> :IHandler<TRequest,TResponse>,IConsumer<TRequest>
            where TRequest:class,IRequest<TResponse>
            where TResponse:class
    {
        public abstract Task<TResponse> HandleAsync(TRequest request);

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            var messageResult = await HandleAsync(context.Message);
            await context.RespondAsync(messageResult);
        }
    }

    public abstract class BaseNotificationMessageHandler<TRequest> : INotificationHandler<TRequest>, IConsumer<TRequest>
        where TRequest : class, INotification
    {
        public abstract Task HandleAsync(TRequest request);

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            await HandleAsync(context.Message);
        }
    }
}
