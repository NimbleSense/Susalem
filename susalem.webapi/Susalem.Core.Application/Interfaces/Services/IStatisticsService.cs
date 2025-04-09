using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.ReadModels.Statistics;

namespace Susalem.Core.Application.Interfaces.Services;

/// <summary>
/// 信息统计接口
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// 基本数量信息
    /// </summary>
    /// <returns></returns>
    Result<BasicInfoQueryModel> GetBasicInfo();

    /// <summary>
    /// 点位功能信息分布
    /// </summary>
    /// <returns></returns>
    Task<Result<IList<PositionDistributionQueryModel>>> GetPositionDistributionAsync();

    /// <summary>
    /// 获取当天报警数量分布
    /// </summary>
    /// <returns></returns>
    Result<AlarmCountDistributionQueryModel> GetAlarmCountDistributionByDay();

    /// <summary>
    /// 获取当天点位数量分布
    /// </summary>
    /// <returns></returns>
    Result<PositionCountDistributionQueryModel> GetPositionCountDistributionByDay();
}