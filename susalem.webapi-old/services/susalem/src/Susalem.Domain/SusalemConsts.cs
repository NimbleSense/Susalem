namespace Susalem;

/// <summary>
/// 数据库常量配置
/// </summary>
public static class SusalemConsts
{

    #region 各个表的前缀
    /// <summary>
    /// 默认
    /// </summary>
    internal const string Prefix = null;

    /// <summary>
    /// Abp
    /// </summary>
    internal const string AbpPrefix = "Abp";

    /// <summary>
    /// OIDC
    /// </summary>
    internal const string OpenIddictPrefix = "OIDC";
    #endregion

    #region 架构  Mysql不支持  根据数据库启用
    /// <summary>
    /// 默认架构
    /// </summary>
    internal const string DbSchema  = null;
    /// <summary>
    /// ABP数据库表类型标识
    /// </summary>
    internal const string AbpDbSchema = "Abp";
    #endregion


    #region 数据库连接字符串
    /// <summary>
    /// 默认连接字符串
    /// </summary>
    internal const string  DefaultConnectionStringName = "Default";

    #endregion

}
