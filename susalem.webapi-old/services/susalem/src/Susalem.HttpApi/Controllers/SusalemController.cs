using Susalem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Susalem.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SusalemController : AbpControllerBase
{
    protected SusalemController()
    {
        LocalizationResource = typeof(SusalemResource);
    }
}
