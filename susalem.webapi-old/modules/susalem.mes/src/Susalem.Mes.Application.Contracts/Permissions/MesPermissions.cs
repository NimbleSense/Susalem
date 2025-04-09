using Volo.Abp.Reflection;

namespace Susalem.Mes.Permissions;

public class MesPermissions
{
    public const string GroupName = "Mes";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MesPermissions));
    }
}
