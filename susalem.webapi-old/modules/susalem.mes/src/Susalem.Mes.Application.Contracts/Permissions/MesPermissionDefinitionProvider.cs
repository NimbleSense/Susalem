using Susalem.Mes.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Susalem.Mes.Permissions;

public class MesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MesPermissions.GroupName, L("Permission:Mes"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MesResource>(name);
    }
}
