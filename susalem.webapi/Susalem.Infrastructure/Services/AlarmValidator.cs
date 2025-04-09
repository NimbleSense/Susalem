using System;
using System.Collections.Generic;
using System.Linq;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Record;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Alarm;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Services;

public class AlarmValidator : IAlarmValidator
{
    private readonly ILogger<AlarmValidator> _logger;

    /// <summary>
    /// 告警规则下的报警, key: 报警规则Id， Value: 报警详细内容
    /// </summary>
    private readonly Dictionary<int, CachedAlarmDetail> _cachedAlarmRuleDetails;

    public AlarmValidator(ILogger<AlarmValidator> logger)
    {
        _logger = logger;
        _cachedAlarmRuleDetails = new Dictionary<int, CachedAlarmDetail>();
    }

    /// <summary>
    /// 验证报警规则
    /// </summary>
    /// <param name="alarmRule">指定报警规则</param>
    /// <param name="records">单个点位单次产生的不同点位功能的记录</param>
    public AlarmRequestDTO Validate(AlarmRuleQueryModel alarmRule, IList<RecordRequestDTO> records)
    {
        _logger.LogInformation($"Validate alarm rule: {alarmRule.Name}, TriggerCount: {alarmRule.Settings.TriggerCount}, TriggerInterval: {alarmRule.Settings.TriggerInterval}");
        var alarmDetails = new List<AlarmDetailDTO>();

        bool? ruleSuccess = null;

        foreach (var positionFunctionRecord in records)
        {
            foreach (var recordContent in positionFunctionRecord.Contents)
            {
                //根据记录找到对应的规则
                var triggerRules = alarmRule.Rules.Where(t => t.Key.Equals(recordContent.Key));
                //将所有的规则做计算校验
                foreach (var triggerRule in triggerRules)
                {
                    _logger.LogInformation($"{triggerRule}");
                    var meetCondition = false;
                    ruleSuccess = triggerRule.Execute(recordContent.Value, ruleSuccess, ref meetCondition);
                    if (meetCondition) //如果报警规则成功，表明是报警，暂存报警内容
                    {
                        var alarmDetail = new AlarmDetailDTO
                        {
                            AlarmProperty = recordContent.Key,
                            AlarmValue = recordContent.Value,
                            Function = positionFunctionRecord.PositionFunction
                        };
                        _logger.LogInformation($"{alarmDetail}");
                        alarmDetails.Add(alarmDetail);
                    }
                    else
                    {
                        _logger.LogInformation($"{positionFunctionRecord.PositionFunction} {recordContent.Key}: {recordContent.Value} not match the rule");
                    }
                }
            }
        }

        if (ruleSuccess != null && !(bool)ruleSuccess)//如果规则失败，证明没有报警
        {
            alarmDetails.Clear();
        }

        if (alarmDetails.Count <= 0) return null;

        //判断报警条件次数为1次，则直接报警
        if (alarmRule.Settings.TriggerCount <= 1)
        {
            return new AlarmRequestDTO
            {
                AlarmDetails = alarmDetails,
                PositionId = alarmRule.PositionId,
                Level = alarmRule.AlarmLevel,
                ReportTime = DateTime.Now
            };
        }

        //配合缓存报警进行计算
        if (!_cachedAlarmRuleDetails.ContainsKey(alarmRule.Id) )
        {
            _logger.LogInformation($"Cached times 1, Count:{alarmDetails.Count}");
            _cachedAlarmRuleDetails.Add(alarmRule.Id, new CachedAlarmDetail(alarmDetails));
        }
        else
        {
            var lastReportTime = _cachedAlarmRuleDetails[alarmRule.Id].RecentTime;
            if (DateTime.Now.Subtract(lastReportTime).TotalSeconds >= alarmRule.Settings.TriggerInterval * 60)
            {
                //超过指定时间了，则清除上一条缓存记录
                _cachedAlarmRuleDetails[alarmRule.Id].Clear();
            }

            _cachedAlarmRuleDetails[alarmRule.Id].Add(alarmDetails); 

            _logger.LogInformation($"Cached times {_cachedAlarmRuleDetails[alarmRule.Id].Times}, Count:{_cachedAlarmRuleDetails[alarmRule.Id].AlarmDetails.Count}");

            if (_cachedAlarmRuleDetails[alarmRule.Id].Times >= alarmRule.Settings.TriggerCount)
            {
                var alarmRequest = new AlarmRequestDTO
                {
                    AlarmDetails = _cachedAlarmRuleDetails[alarmRule.Id].AlarmDetails,
                    PositionId = alarmRule.PositionId,
                    Level = alarmRule.AlarmLevel,
                    ReportTime = DateTime.Now
                };

                _cachedAlarmRuleDetails[alarmRule.Id].Clear();

                return alarmRequest;
            }
        }

        return null;
    }

    public void ClearCachedAlarms()
    {
        _logger.LogInformation("Clear all cached alarms");
        _cachedAlarmRuleDetails.Clear();
    }

    /// <summary>
    /// 验证具体点位功能的内容
    /// </summary>
    /// <param name="alarmRule">报警规则</param>
    /// <param name="record">内容</param>
    /// <param name="alarmDetails">报警列表</param>
    private void ValidatePositionFunctionRecord(AlarmRuleQueryModel alarmRule, RecordRequestDTO record, IList<AlarmDetailDTO> alarmDetails)
    {
        bool? ruleSuccess = null;
        foreach (var recordContent in record.Contents)
        {
            //根据记录找到对应的规则
            var triggerRules = alarmRule.Rules.Where(t => t.Key.Equals(recordContent.Key));
            //将所有的规则做计算校验
            foreach (var triggerRule in triggerRules)
            {
                var meetCondition = false;
                ruleSuccess = triggerRule.Execute(recordContent.Value, ruleSuccess, ref meetCondition);
                if (meetCondition) //如果报警规则成功，表明是报警，暂存报警内容
                {
                    var alarmDetail = new AlarmDetailDTO
                    {
                        AlarmProperty = recordContent.Key,
                        AlarmValue = recordContent.Value,
                        Function = record.PositionFunction
                    };
                    _logger.LogInformation($"{alarmDetail}");
                    alarmDetails.Add(alarmDetail);
                }
            }
        }
        if (ruleSuccess != null && !(bool)ruleSuccess)//如果规则失败，证明没有报警
        {
            _logger.LogInformation($"Valid: {record.PositionFunction}");
            alarmDetails.Clear();
        }
    }
}