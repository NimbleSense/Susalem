using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Susalem;

[Dependency(ReplaceServices = true)]
public class SusalemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Susalem";
}
