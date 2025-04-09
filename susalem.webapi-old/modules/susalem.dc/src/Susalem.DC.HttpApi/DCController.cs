using Susalem.DC.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Susalem.DC;

public abstract class DCController : AbpControllerBase
{
    protected DCController()
    {
        LocalizationResource = typeof(DCResource);
    }
}
