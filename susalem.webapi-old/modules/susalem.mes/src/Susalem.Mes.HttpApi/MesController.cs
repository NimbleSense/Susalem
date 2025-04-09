using Susalem.Mes.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Susalem.Mes;

public abstract class MesController : AbpControllerBase
{
    protected MesController()
    {
        LocalizationResource = typeof(MesResource);
    }
}
