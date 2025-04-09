using Susalem.Mes.Localization;
using Volo.Abp.Application.Services;

namespace Susalem.Mes;

public abstract class MesAppService : ApplicationService
{
    protected MesAppService()
    {
        LocalizationResource = typeof(MesResource);
        ObjectMapperContext = typeof(MesApplicationModule);
    }
}
