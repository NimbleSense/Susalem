namespace Susalem.Core.Application.Models;

/// <summary>
/// 系统设置
/// </summary>
public class SystemSetting
{
    /// <summary>
    /// 公司名称
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// 开启Excel导出
    /// </summary>
    public bool ExportExcel { get; set; }
}
