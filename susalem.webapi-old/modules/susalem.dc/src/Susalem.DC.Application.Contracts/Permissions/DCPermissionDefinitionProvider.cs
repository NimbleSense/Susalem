using Susalem.DC.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Susalem.DC.Permissions;

public class DCPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DCPermissions.GroupName, L("Permission:DC"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DCResource>(name);
    }
}
