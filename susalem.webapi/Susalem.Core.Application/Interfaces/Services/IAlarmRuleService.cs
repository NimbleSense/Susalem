using Susalem.Common.Results;
using Susalem.Core.Application.DTOs.Alarm;
using Susalem.Core.Application.ReadModels.Alarm;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IAlarmRuleService
    {
        Task<Result<AlarmRuleQueryModel>> CreateAlarmRuleAsync(AlarmRuleRequestDTO alarmRuleRequest);

        Task<Result<AlarmRuleQueryModel>> GetAlarmRuleAsync(int alarmRuleId);

        Task<Result> EditAlarmRuleAsync(int alarmRuleId, AlarmRuleRequestDTO alarmRuleRequest);

        Task<Result> DeleteAlarmRuleAsync(int alarmRuleId);

        Task<Result<IEnumerable<AlarmRuleQueryModel>>> GetAlarmRulesAsync();

        /// <summary>
        /// 获取指定点位可用的报警规则
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<AlarmRuleQueryModel>>> GetAlarmRulesByPositionIdAsync(int positionId);

        Task<Result> SetAlarmRuleNotificationAsync(int alarmRuleId, NotificationSetting settings);

    }
}
