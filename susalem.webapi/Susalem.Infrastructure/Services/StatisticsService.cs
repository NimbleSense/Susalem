using Susalem.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Infrastructure.Models.Application;
using Microsoft.Extensions.Logging;
using Susalem.Core.Application;
using Susalem.Core.Application.ReadModels.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Localize;
using Nito.AsyncEx;

namespace Susalem.Infrastructure.Services;

internal class StatisticsService: IStatisticsService
{
    private readonly ILogger<StatisticsService> _logger;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IEntityRepository<AreaEntity, int> _areaRepository;
    private readonly IEntityRepository<DeviceEntity, int> _deviceRepository;
    private readonly IEntityRepository<PositionEntity, int> _positionRepository;
    private readonly IEntityRepository<AlarmEntity, int> _alarmRepository;
    private readonly IRecordService _recordService;

    public StatisticsService(ILogger<StatisticsService> logger, 
        IStringLocalizer<Resource> localizer,
        IEntityRepository<AreaEntity,int> areaRepository,
        IEntityRepository<DeviceEntity,int> deviceRepository,
        IEntityRepository<PositionEntity,int> positionRepository,
        IEntityRepository<AlarmEntity,int> alarmRepository, 
        IRecordService recordService)
    {
        _logger = logger;
        _localizer = localizer;
        _areaRepository = areaRepository;
        _deviceRepository = deviceRepository;
        _positionRepository = positionRepository;
        _alarmRepository = alarmRepository;
        _recordService = recordService;
    }

    public Result<AlarmCountDistributionQueryModel> GetAlarmCountDistributionByDay()
    {
        var serviceResult = new Result<AlarmCountDistributionQueryModel>();
        try
        {
            var alarmCountDistribution = new AlarmCountDistributionQueryModel();
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, now.Day);

            var hour = now.Hour + 1;
            for (var i = 0; i < hour; i++)
            {
                var startTime = startDate.AddHours(i);
                var endTime = startDate.AddHours(i + 1).AddSeconds(-1);
                var warningCount = _alarmRepository.GetBy(t => t.ReportTime >= startTime && 
                                       t.ReportTime <= endTime && 
                                       t.Level == Core.Application.Enumerations.AlarmLevelEnum.Warning).Count();
                var alarmCount = _alarmRepository.GetBy(t => t.ReportTime >= startTime &&
                                       t.ReportTime <= endTime &&
                                       t.Level == Core.Application.Enumerations.AlarmLevelEnum.Alarm).Count();

                alarmCountDistribution.Hours.Add($"{i}:00");
                alarmCountDistribution.Alarms.Add(alarmCount);
                alarmCountDistribution.Warnings.Add(warningCount);
            }
            serviceResult.WithData(alarmCountDistribution);
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get alarm count distribution statistics.");
        }
        return serviceResult;
    }

    public Result<BasicInfoQueryModel> GetBasicInfo()
    {
        var serviceResult = new Result<BasicInfoQueryModel>();
        try
        {
            var basicInfo = new BasicInfoQueryModel
            {
                AreaCount = _areaRepository.GetAll().Count(),
                DeviceCount = _deviceRepository.GetAll().Select(t=>t.Address).Distinct().Count(),
                PositionCount = _positionRepository.GetAll().Count(),
                UnHandledAlarmCount = _alarmRepository.GetBy(t => !t.IsConfirmed).Count()
            };
            return serviceResult.WithData(basicInfo).Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get basic info statistics.");
        }
        return serviceResult;
    }

    public  Result<PositionCountDistributionQueryModel> GetPositionCountDistributionByDay()
    {
        var serviceResult = new Result<PositionCountDistributionQueryModel>();
        try
        {
            var positionCountDistribution = new PositionCountDistributionQueryModel();

            var positions = _positionRepository.GetAll().ToList();

            var now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, now.Day);
            var endTime = startTime.AddDays(1).AddSeconds(-1);

            foreach (var position in positions)
            {
                var count = _recordService.GetRecordCountWithinTime(position.Id, startTime, endTime);
                positionCountDistribution.PositionNames.Add(position.Name);
                positionCountDistribution.RecordCounts.Add(count);
            }
            serviceResult.WithData(positionCountDistribution);
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get position record count distribution statistics.");
        }
        return serviceResult;
    }

    public async Task<Result<IList<PositionDistributionQueryModel>>> GetPositionDistributionAsync()
    {
        var serviceResult = new Result<IList<PositionDistributionQueryModel>>();
        try
        {
            var distributionPositions = new List<PositionDistributionQueryModel>();
            var positions = await _positionRepository.GetAll().ToListAsync();
            var positionFunctions = positions.SelectMany(t => t.Functions).ToList();

            distributionPositions.AddRange(positionFunctions.GroupBy(t => t.Key).Select(t =>
                new PositionDistributionQueryModel()
                {
                    Name = _localizer[t.Key.ToString()],
                    Value = t.Count()
                }));

            return serviceResult.WithData(distributionPositions).Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get position distributions.");
        }
        return serviceResult;
    }
}