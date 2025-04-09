using AutoMapper;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Susalem.Common.Paging;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Record;
using Susalem.Infrastructure.Device.Constants;
using Susalem.Infrastructure.Mappings.Resolvers;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Infrastructure.Models.Application;
using PositionRecordEntity = Susalem.Infrastructure.Models.Record.PositionRecordEntity;
using Susalem.Core.Application.ReadModels.Alarm;
using Microsoft.Extensions.Configuration;

namespace Susalem.Infrastructure.Services;

public class RecordService:IRecordService
{
    private readonly RecordDbContext _dbContext;
    private readonly IEntityRepository<PositionEntity, int> _positionRepository;
    private readonly IEntityRepository<DeviceEntity, int> _deviceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RecordService> _logger;
    private readonly IAlarmRuleService _alarmRuleService;
    private readonly IConfiguration _configuration;

    public RecordService(RecordDbContext dbContext, 
        IEntityRepository<PositionEntity, int> positionRepository,
        IEntityRepository<DeviceEntity, int> deviceRepository,
        IAlarmRuleService alarmRuleService,
        IConfiguration configuration,
        IMapper mapper, 
        ILogger<RecordService> logger)
    {
        _dbContext = dbContext;
        _positionRepository = positionRepository;
        _deviceRepository = deviceRepository;
        _alarmRuleService = alarmRuleService;
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;

        try
        {
            _dbContext.Database.Migrate();
            if (_configuration["Db:Provider"].ToLower().Equals("sqlite"))
            {
                _dbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL");
            }
        }
        catch (Exception ex)
        {

        }
    }

    public async Task<Result> CreateRecordsAsync(IEnumerable<RecordRequestDTO> recordRequests)
    {
        var serviceResult = new Result();
        try
        {
            var recordEntities = _mapper.Map<IEnumerable<PositionRecordEntity>>(recordRequests);

            await _dbContext.PositionRecords.AddRangeAsync(recordEntities);
            await _dbContext.SaveChangesAsync();

            return serviceResult.Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create records.");
        }
        return serviceResult;
    }

    public PagedList<RecordReadModel> GetRecords(RecordParameters parameters)
    {

        var configuration = new MapperConfiguration(cfg =>
            cfg.CreateProjection<PositionRecordEntity, RecordReadModel>());

        var deviceRecordEntities = _dbContext.PositionRecords.Where(t => t.PositionId == parameters.PositionId &&
                                                                t.PositionFunction == parameters.PositionFunction &&
                                                                t.CreateTime >= parameters.StartTime &&
                                                                t.CreateTime <= parameters.EndTime)
            .OrderBy(t => t.CreateTime)
            .ProjectTo<RecordReadModel>(configuration);

        if (deviceRecordEntities == null)
        {
            return new PagedList<RecordReadModel>(new List<RecordReadModel>(),0,parameters.PageNumber,parameters.PageSize);
        }

        return PagedList<RecordReadModel>.ToPagedList(deviceRecordEntities, parameters.PageNumber,
            parameters.PageSize);
    }

    public async Task<Result<IEnumerable<RecordReadModel>>> GetRecordsAsync(int positionId, PositionFunctionEnum positionFunction, DateTime startTime, DateTime endTime)
    {
        var serviceResult = new Result<IEnumerable<RecordReadModel>>();
        try
        {
            var deviceRecordEntities = await _dbContext.PositionRecords.Where(t => t.PositionId == positionId &&
                                                                          t.PositionFunction == positionFunction &&
                                                                          t.CreateTime >= startTime &&
                                                                          t.CreateTime <= endTime)
                .OrderBy(t => t.CreateTime).ToListAsync();
            var records = _mapper.Map<IEnumerable<RecordReadModel>>(deviceRecordEntities);
            serviceResult.Successful().WithData(records);
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get all records");
        }

        return serviceResult;
    }

    public async Task<Result<RecordChartReadModel>> GetChartRecordsAsync(RecordChartRequestDTO request)
    {
        var serviceResult = new Result<RecordChartReadModel>();
        try
        {
            var positionEntity = _positionRepository.GetFirst(t => t.Id == request.PositionId);
            var positionFunction = positionEntity.Functions.FirstOrDefault(t => t.Key == request.PositionFunction);

            if (positionFunction == null) throw new Exception("Position function not found");

            ICollection<DeviceFunctionProperty> deviceProperties = new List<DeviceFunctionProperty>();
            foreach (var positionDeviceProperty in positionFunction.Devices)
            {
                var deviceEntity = _deviceRepository.GetFirst(t => t.Id == positionDeviceProperty.Id);
                if (deviceEntity != null)
                {
                    deviceProperties = deviceEntity.Properties;
                }
            }

            var recordChartReadModel = new RecordChartReadModel();
            var records = await _dbContext.PositionRecords.Where(t => t.PositionId == request.PositionId &&
                                               t.PositionFunction == request.PositionFunction &&
                                               t.CreateTime >= request.StartTime &&
                                               t.CreateTime <= request.EndTime).OrderBy(t => t.CreateTime).ToListAsync();
            // X轴数据时记录发生的时间
            recordChartReadModel.XData = records.Select(t => t.CreateTime).ToList();

            if (records.Count > 0)
            {
                var units = records[0].Contents.Select(t => DeviceFunctionValueUnitResolver.GetUnit(t.Key)).ToList().Distinct();

                foreach (var unit in units)
                {
                    if (string.IsNullOrEmpty(recordChartReadModel.YLeftName))
                    {
                        recordChartReadModel.YLeftName = unit;
                    }
                    else
                    {
                        recordChartReadModel.YRightName = unit;
                    }
                }
            }

            var dic = new Dictionary<string, List<double>>();
            foreach (var record in records)
            {
                foreach (var content in record.Contents)
                {
                    if (!dic.ContainsKey(content.Key))
                    {
                        dic.Add(content.Key, new List<double>());
                    }
                    dic[content.Key].Add(content.Value);
                }
            }
            AlarmRuleQueryModel alarmRule = null;

            foreach (var itemKey in dic.Keys)
            {
                var markLineData = new List<double>();
                
                    var deviceProperty = deviceProperties.FirstOrDefault(t => t.Key == itemKey);

                    if (alarmRule != null)
                    {
                        var alarmRuleSettings = alarmRule.Rules.Where(t => t.Key.Equals(itemKey));

                        if (alarmRuleSettings != null)
                        {
                            foreach (var setting in alarmRuleSettings)
                            {
                                markLineData.Add(setting.RuleValue);
                            }
                        }
                    }

                    

                var itemUnit = DeviceFunctionValueUnitResolver.GetUnit(itemKey);
                if (itemUnit.Equals(recordChartReadModel.YLeftName))
                {
                    recordChartReadModel.YLeft.Add(new YAxis(itemKey, dic[itemKey], markLineData));
                }
                else
                {
                    recordChartReadModel.YRight.Add(new YAxis(itemKey, dic[itemKey], markLineData));
                }
            }

            return serviceResult.Successful().WithData(recordChartReadModel);
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get chart record.");
        }
        return serviceResult;
    }

    public int GetRecordCountWithinTime(int positionId, DateTime startTime, DateTime endTime)
    {
        var count = _dbContext.PositionRecords.Where(t => t.PositionId == positionId &&
                                  t.CreateTime >= startTime &&
                                  t.CreateTime <= endTime).Count();
        return count;
    }
}
