using Susalem.Core.Application.Interfaces;
using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Susalem.Common.Extensions;
using System.Linq;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.Commands.Events;

/// <summary>
/// 点位记录保存命令
/// </summary>
/// <param name="positionId">点位ID</param>
/// <param name="Records">点位记录</param>
public record SavePositionRecordCommand(int positionId,IList<RecordRequestDTO> Records) : IRequest<Result>;

/// <summary>
/// 保存点位记录
/// </summary>
public class SavePositionRecordsHandler : BaseMessageHandler<SavePositionRecordCommand, Result>
{
    private readonly IRecordService _recordService;

    public SavePositionRecordsHandler(IRecordService recordService)
    {
        _recordService = recordService;
    }

    public override async Task<Result> HandleAsync(SavePositionRecordCommand request)
    {
        await _recordService.CreateRecordsAsync(request.Records);
        return new Result().Successful();
    }
}

/// <summary>
/// 点位记录数据创建产生
/// </summary>
/// <param name="PositionId"></param>
/// <param name="Records"></param>
public record CreatePositionRecordCommand(int PositionId, IList<RecordRequestDTO> Records, bool Door1, bool Door2, bool Door3, bool Door4) : IRequest<Result>;

/// <summary>
/// 验证点位数据记录是否产生告警
/// </summary>
public class RecordsAlarmValidationHandler : BaseMessageHandler<CreatePositionRecordCommand, Result>
{
    private readonly ILogger<RecordsAlarmValidationHandler> _logger;
    private readonly IServiceBus _serviceBus;
    private readonly IAlarmRuleService _alarmRuleService;
    private readonly IApplicationAlarmService _alarmService;
    private readonly IAlarmValidator _alarmValidator;
    private readonly IPositionFactory _positionFactory;
    private readonly IEngineFactory _engineFactory;

    public RecordsAlarmValidationHandler(ILogger<RecordsAlarmValidationHandler> logger,
        IServiceBus serviceBus,
        IAlarmRuleService alarmRuleService, 
        IApplicationAlarmService alarmService,
        IAlarmValidator alarmValidator,
        IPositionFactory positionFactory,
        IEngineFactory engineFactory)
    {
        _logger = logger;
        _serviceBus = serviceBus;
        _alarmRuleService = alarmRuleService;
        _alarmService = alarmService;
        _alarmValidator = alarmValidator;
        _positionFactory = positionFactory;
        _engineFactory = engineFactory;
    }

    public override async Task<Result> HandleAsync(CreatePositionRecordCommand request)
    {
        var alarmRulesResult = await _alarmRuleService.GetAlarmRulesByPositionIdAsync(request.PositionId);
        if (alarmRulesResult.Failed)
        {
            return alarmRulesResult;
        }
        var alarmCount = 0;
        var warningCount = 0;
        foreach (var alarmRule in alarmRulesResult.Data)
        {
            var alarmRequest = _alarmValidator.Validate(alarmRule, request.Records);
            if (alarmRequest == null) continue;

            //根据报警结果，调整设备状态
            foreach(var alarmDetail in alarmRequest.AlarmDetails)
            {
                var recordRequest = request.Records.FirstOrDefault(t => t.PositionFunction == alarmDetail.Function);
                if (recordRequest == null) continue;

                var recordContent = recordRequest.Contents.FirstOrDefault(t => t.Key == alarmDetail.AlarmProperty);
                if (recordContent == null) continue;

                recordContent.DeviceStatus = alarmRule.AlarmLevel == AlarmLevelEnum.Alarm ? DeviceStatus.Alarm : DeviceStatus.Warning;
            }

            var result = await _alarmService.CreateAlarmAsync(alarmRequest);
            if (result.Failed)
            {
                _logger.LogError("Save alarm failed");
                return result;
            }
            if (alarmRequest.Level == AlarmLevelEnum.Alarm) alarmCount++;

            if(alarmRequest.Level == AlarmLevelEnum.Warning) warningCount++;
            
            await _serviceBus.Publish(new AlarmGenerated(alarmRule, result.Data));
        }

        //找到点位下面的报警灯，根据告警等级，设置等级
        var alerts = _positionFactory.GetBoundAlerter(request.PositionId);
        foreach (var alert in alerts)
        {
            if (warningCount == 0 && alarmCount == 0) _engineFactory.UpdateAlerterStatus(alert, request.PositionId, null);

            if (alarmCount > 0) _engineFactory.UpdateAlerterStatus(alert, request.PositionId, AlarmLevelEnum.Alarm);

            if (warningCount > 0) _engineFactory.UpdateAlerterStatus(alert, request.PositionId, AlarmLevelEnum.Warning);
        }
        //发送点位记录
        await _serviceBus.Publish(new PositionRecordStatusUpdated(request.PositionId, request.Records, request.Door1, request.Door2, request.Door3, request.Door4));

        return new Result().Successful();
    }
}
