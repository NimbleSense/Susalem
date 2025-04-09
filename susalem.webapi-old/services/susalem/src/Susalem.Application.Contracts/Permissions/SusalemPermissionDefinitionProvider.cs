using Susalem.Localization;

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Susalem.Permissions;

public class SusalemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SusalemPermissions.GroupName);

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SusalemResource>(name);
    }
}
