using Volo.Abp.Reflection;

namespace Susalem.DC.Permissions;

public class DCPermissions
{
    public const string GroupName = "DC";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DCPermissions));
    }
}
