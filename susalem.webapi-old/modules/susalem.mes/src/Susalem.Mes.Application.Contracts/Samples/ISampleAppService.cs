using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Susalem.Mes.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
