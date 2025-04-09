using System;
using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Record;
using Susalem.Core.Application.ReadModels.Alarm;

namespace Susalem.Core.Application.Interfaces.Services;

/// <summary>
/// 验证记录是否需要报警
/// </summary>
public interface IAlarmValidator
{
    /// <summary>
    /// 根据报警规则验证数据记录
    /// </summary>
    /// <param name="alarmRule">指定报警规则</param>
    /// <param name="records">单个点位单次产生的不同点位功能的记录</param>
    AlarmRequestDTO Validate(AlarmRuleQueryModel alarmRule, IList<RecordRequestDTO> records);

    /// <summary>
    /// 清除缓存中的报警
    /// </summary>
    void ClearCachedAlarms();
}

public class CachedAlarmDetail
{
    /// <summary>
    /// 缓存的报警次数
    /// </summary>
    public int Times { get; private set; }

    /// <summary>
    /// 最近一次时间
    /// </summary>
    public DateTime RecentTime { get; private set; }

    /// <summary>
    /// 报警内容
    /// </summary>
    public List<AlarmDetailDTO> AlarmDetails { get; private set; }

    public CachedAlarmDetail(List<AlarmDetailDTO> alarmDetails)
    {
        AlarmDetails = alarmDetails;
        Times = 1;
        RecentTime = DateTime.Now;
    }

    public void Clear()
    {
        AlarmDetails.Clear();
        Times = 0;
    }

    public void Add(IList<AlarmDetailDTO> newAlarmDetails)
    {
        Times++;
        AlarmDetails.AddRange(newAlarmDetails);
        RecentTime = DateTime.Now;
    }
}