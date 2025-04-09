using Susalem.Common.Results;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces.Providers;

public interface IWebhookProvider
{
    Task<Result> PostDataAsync<T>(T data, string url, string requestUri);
}
