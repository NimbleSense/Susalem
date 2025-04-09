using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Paging;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Alarm;

namespace Susalem.Core.Application.Interfaces.Services
{
    /// <summary>
    /// Alarm service,include alarm and warning
    /// </summary>
    public interface IApplicationAlarmService
    {
        /// <summary>
        /// Report alarm or warning
        /// </summary>
        /// <param name="alarmRequestDto"></param>
        /// <returns></returns>
        Task<Result<AlarmQueryModel>> CreateAlarmAsync(AlarmRequestDTO alarmRequestDto);

        /// <summary>
        /// Report alarms or warnings-
        /// </summary>
        /// <param name="alarmRequestDto"></param>
        /// <returns></returns>
        Task<Result<IEnumerable<AlarmQueryModel>>> CreateAlarmsAsync(IEnumerable<AlarmRequestDTO> alarmRequestDto);

        /// <summary>
        /// Get all unconfirmed alarms and warnings
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<AlarmQueryModel>>> GetUnConfirmedAsync();

        /// <summary>
        /// Get all unconfirmed alarms and warnings count
        /// </summary>
        /// <returns></returns>
        Task<Result<int>> GetUnConfirmedAlarmsCount();

        Task<Result<IEnumerable<AlarmQueryModel>>> GetUnConfirmedAlarmsAsync(int count);

        Task<Result> ConfirmAlarmsAsync(ICollection<int> alarmIds, string confirmContent);

        Task<Result> ConfirmAlarmsAsync(int count, string confirmContent);

        PagedList<AlarmDetailQueryModel> GetAllAlarms(AlarmParameters parameters);

        Task<Result<IEnumerable<AlarmDetailQueryModel>>> GetAllAlarms(AlarmLevelEnum alarmLevel, bool confirmStatus,
            DateTime startTime, DateTime endTime);

        Task<bool> IsExistsUnConfirmedAlarmsAsync(int positionId);
    }
}
