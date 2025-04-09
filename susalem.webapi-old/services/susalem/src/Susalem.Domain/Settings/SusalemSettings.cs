namespace Susalem.Settings;

/// <summary>
/// 系统自定义配置
/// </summary>
public static class SusalemSettings
{

    /// <summary>
    /// 默认邮箱
    /// </summary>
    public const string AdminEmailDefaultValue = "admin@susalem.io";
    
    /// <summary>
    /// 默认密码
    /// </summary>
   
    public const string AdminPasswordDefaultValue = "Susalem321..";

    /// <summary>
    /// 配置ABP框架数据库表名
    /// </summary>
    public static void ConfigureDataTableName()
    {
        #region Abp框架
        //权限管理
        //Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbSchema = SusalemConsts.AbpDbSchema;
        Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbTablePrefix = SusalemConsts.AbpPrefix;

        //审计日志
        //Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbSchema = SusalemConsts.AbpDbSchema;
        Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbTablePrefix = SusalemConsts.AbpPrefix;
        
        //身份管理
        //Volo.Abp.Identity.AbpIdentityDbProperties.DbSchema = SusalemConsts.AbpDbSchema;
        Volo.Abp.Identity.AbpIdentityDbProperties.DbTablePrefix = SusalemConsts.AbpPrefix;
        //openiddict
        //Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbSchema = SusalemConsts.AbpDbSchema;
        Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbTablePrefix = SusalemConsts.OpenIddictPrefix;
        #endregion

        //MES系统

    }


}
