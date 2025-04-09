using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Susalem.Common.Extensions;
using Susalem.Common.Paging;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Services
{
    internal class ApplicationAlarmService : IApplicationAlarmService
    {
        private readonly ILogger<ApplicationAlarmService> _logger;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IEntityRepository<AlarmEntity, int> _alarmRepository;
        private readonly IMapper _mapper;

        public ApplicationAlarmService(ILogger<ApplicationAlarmService> logger, 
            IAuthenticatedUserService authenticatedUserService,
            IEntityRepository<AlarmEntity,int> alarmRepository,
            IMapper mapper)
        {
            _logger = logger;
            _authenticatedUserService = authenticatedUserService;
            _alarmRepository = alarmRepository;
            _mapper = mapper;
        }

        public async Task<Result<AlarmQueryModel>> CreateAlarmAsync(AlarmRequestDTO alarmRequestDto)
        {
            var serviceResult = new Result<AlarmQueryModel>();
            try
            {
                var alarmEntity = _mapper.Map<AlarmEntity>(alarmRequestDto);

                await _alarmRepository.AddAsync(alarmEntity);
                await _alarmRepository.UnitOfWork.SaveChangesAsync();

                var newAlarmEntity = _alarmRepository.GetBy(t => t.Id == alarmEntity.Id).Include(t => t.Position).FirstOrDefault();
                return serviceResult.Successful().WithData(_mapper.Map<AlarmQueryModel>(newAlarmEntity));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create alarm.");
            }
            return serviceResult;
        }

        public async Task<Result<IEnumerable<AlarmQueryModel>>> CreateAlarmsAsync(IEnumerable<AlarmRequestDTO> alarmRequestDto)
        {
            var serviceResult = new Result<IEnumerable<AlarmQueryModel>>();
            try
            {
                var alarmEntities = _mapper.Map<IEnumerable<AlarmEntity>>(alarmRequestDto).ToList();

                await _alarmRepository.AddRangeAsync(alarmEntities);
                await _alarmRepository.UnitOfWork.SaveChangesAsync();

                var alarmIds = alarmEntities.Select(t => t.Id);
                var newAlarmEntities = _alarmRepository.GetBy(t => alarmIds.Contains(t.Id)).Include(t => t.Position);
                return serviceResult.Successful().WithData(_mapper.Map<IEnumerable<AlarmQueryModel>>(newAlarmEntities));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create alarm.");
            }
            return serviceResult;
        }

        public async Task<Result<IEnumerable<AlarmQueryModel>>> GetUnConfirmedAsync()
        {
            var serviceResult = new Result<IEnumerable<AlarmQueryModel>>();
            try
            {
                var alarmEntities = await _alarmRepository.GetBy(t=>t.IsConfirmed==true).OrderByDescending(t=>t.ReportTime).Take(100)
                    .Include(t=>t.Position)
                    .ToListAsync();

                return serviceResult.Successful().WithData(_mapper.Map<IEnumerable<AlarmQueryModel>>(alarmEntities));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get unconfirmed alarms.");
            }
            return serviceResult;
        }

        public async Task<Result<IEnumerable<AlarmQueryModel>>> GetUnConfirmedAlarmsAsync(int count)
        {
            var serviceResult = new Result<IEnumerable<AlarmQueryModel>>();
            try
            {
                var alarmEntities = await _alarmRepository.GetBy(t => !t.IsConfirmed).OrderByDescending(t => t.ReportTime).Take(count)
                    .Include(t=>t.Position)
                    .ToListAsync();

                return serviceResult.Successful().WithData(_mapper.Map<IEnumerable<AlarmQueryModel>>(alarmEntities));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get unconfirmed alarms.");
            }
            return serviceResult;
        }

        public async Task<Result> ConfirmAlarmsAsync(ICollection<int> alarmIds, string confirmContent)
        {
            var serviceResult = new Result<IEnumerable<AlarmQueryModel>>();
            try
            {
                var alarmEntities = await _alarmRepository.GetBy(t => alarmIds.Contains(t.Id)).ToListAsync();

                foreach (var alarmEntity in alarmEntities)
                {
                    alarmEntity.ConfirmContent = confirmContent;
                    alarmEntity.IsConfirmed = true;
                    alarmEntity.ConfirmTime = DateTime.Now;
                    alarmEntity.ConfirmUser = _authenticatedUserService?.UserName;
                }
                _alarmRepository.UpdateRange(alarmEntities);
                await _alarmRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to confirm specific alarms.");
            }
            return serviceResult;
        }

        public async Task<Result<int>> GetUnConfirmedAlarmsCount()
        {
            var serviceResult = new Result<int>();
            try
            {
                var alarmsCount = await _alarmRepository.GetBy(t=>!t.IsConfirmed).CountAsync();

                return serviceResult.Successful().WithData(alarmsCount);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get unconfirmed alarms count.");
            }
            return serviceResult;
        }

        public async Task<Result> ConfirmAlarmsAsync(int count, string confirmContent)
        {
            var serviceResult = new Result<IEnumerable<AlarmQueryModel>>();
            try
            {
                var alarmEntities = await _alarmRepository.GetBy(t => !t.IsConfirmed).OrderByDescending(t => t.ReportTime).Take(count).ToListAsync();

                foreach (var alarmEntity in alarmEntities)
                {
                    alarmEntity.ConfirmContent = confirmContent;
                    alarmEntity.IsConfirmed = true;
                    alarmEntity.ConfirmTime = DateTime.Now;
                    alarmEntity.ConfirmUser = _authenticatedUserService?.UserName;
                }
                _alarmRepository.UpdateRange(alarmEntities);
                await _alarmRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to confirm alarms.");
            }
            return serviceResult;
        }

        public PagedList<AlarmDetailQueryModel> GetAllAlarms(AlarmParameters parameters)
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateProjection<AlarmEntity, AlarmDetailQueryModel>()
                    .ForMember(target => target.PositionName, opt => opt.MapFrom(source => source.Position.Name)));

            var alarmEntities = _alarmRepository
                .GetBy(t => t.IsConfirmed == parameters.ConfirmStatus &&
                            t.Level == parameters.AlarmLevel
                            && t.ReportTime >= parameters.StartTime
                            && t.ReportTime <= parameters.EndTime)
                .Include(t => t.Position)
                .OrderBy(t => t.ReportTime)
                .ProjectTo<AlarmDetailQueryModel>(configuration);

            return PagedList<AlarmDetailQueryModel>.ToPagedList(alarmEntities, parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<Result<IEnumerable<AlarmDetailQueryModel>>> GetAllAlarms(AlarmLevelEnum alarmLevel, bool confirmStatus, DateTime startTime, DateTime endTime)
        {
            var serviceResult = new Result<IEnumerable<AlarmDetailQueryModel>>();
            try
            {
                var alarmEntities = await _alarmRepository.GetBy(t => t.IsConfirmed == confirmStatus &&
                                                                       t.Level == alarmLevel &&
                                                                       t.ReportTime >= startTime && 
                                                                       t.ReportTime <= endTime)
                    .Include(t => t.Position)
                    .OrderBy(t => t.ReportTime).ToListAsync();
                var records = _mapper.Map<IEnumerable<AlarmDetailQueryModel>>(alarmEntities);
                serviceResult.Successful().WithData(records);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get all alarms");
            }

            return serviceResult;
        }

        public async Task<bool> IsExistsUnConfirmedAlarmsAsync( int positionId)
        {
            var serviceResult = new Result<IEnumerable<AlarmDetailQueryModel>>();
            try
            {
                var alarmEntitity = await _alarmRepository.GetFirstAsync(t => t.IsConfirmed == false &&
                                                                       t.PositionId == positionId);
                if(alarmEntitity == null )
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get position is exists unconfirmed alarms");
            }

            return false;
        }
    }
}
