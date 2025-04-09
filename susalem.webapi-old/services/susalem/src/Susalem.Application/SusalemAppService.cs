using Susalem.Localization;

using Volo.Abp.Application.Services;

namespace Susalem;

/* Inherit your application services from this class.
 */
public abstract class SusalemAppService : ApplicationService
{
    protected SusalemAppService()
    {
        LocalizationResource = typeof(SusalemResource);
    }
}
