namespace Susalem.Core.Application.Interfaces.Services;

/// <summary>
/// 平台功能接口
/// </summary>
public interface IPlatformService
{
    /// <summary>
    /// 数据库目录
    /// </summary>
    string DbPath { get; }

    /// <summary>
    /// 网址目录
    /// </summary>
    string WebRootPath { get; }

    /// <summary>
    /// 日志目录
    /// </summary>
    string LogsPath { get; }

    /// <summary>
    /// 获取可用的端口号
    /// </summary>
    /// <returns></returns>
    string[] AvailablePorts();

    /// <summary>
    /// 重启程序
    /// </summary>
    void Restart();
}
