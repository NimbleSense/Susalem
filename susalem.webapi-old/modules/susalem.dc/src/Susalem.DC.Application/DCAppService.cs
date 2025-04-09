using Susalem.DC.Localization;
using Volo.Abp.Application.Services;

namespace Susalem.DC;

public abstract class DCAppService : ApplicationService
{
    protected DCAppService()
    {
        LocalizationResource = typeof(DCResource);
        ObjectMapperContext = typeof(DCApplicationModule);
    }
}
