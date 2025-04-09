using AutoMapper;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs.Alarm;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;

namespace Susalem.Infrastructure.Services
{
    public class AlarmRuleService : IAlarmRuleService
    {
        private readonly ILogger<AlarmRuleService> _logger;
        private readonly IEntityRepository<AlarmRuleEntity, int> _alarmRuleRepository;
        private readonly IMapper _mapper;

        public AlarmRuleService(ILogger<AlarmRuleService> logger,
            IEntityRepository<AlarmRuleEntity, int> alarmRuleRepository,
            IMapper mapper) 
        {
            _logger = logger;
            _alarmRuleRepository = alarmRuleRepository;
            _mapper = mapper;
        }

        public async Task<Result<AlarmRuleQueryModel>> CreateAlarmRuleAsync(AlarmRuleRequestDTO alarmRuleRequest)
        {
            var serviceResult = new Result<AlarmRuleQueryModel>();
            try
            {
                var entity = _mapper.Map<AlarmRuleEntity>(alarmRuleRequest);

                await _alarmRuleRepository.AddAsync(entity);
                await _alarmRuleRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful().WithData(_mapper.Map<AlarmRuleQueryModel>(entity));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create alarm rule.");
            }
            return serviceResult;
        }

        public async Task<Result> DeleteAlarmRuleAsync(int alarmRuleId)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _alarmRuleRepository.GetSingleAsync(a => a.Id == alarmRuleId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("alarm rule not found");
                }
                _alarmRuleRepository.Delete(entity);
                await _alarmRuleRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete alarm rule");
                return serviceResult;
            }
        }

        public async Task<Result> EditAlarmRuleAsync(int alarmRuleId, AlarmRuleRequestDTO alarmRuleRequest)
        {

            var serviceResult = new Result();
            try
            {
                var entity = await _alarmRuleRepository.GetSingleAsync(a => a.Id == alarmRuleId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("alarm rule not found");
                }

                var updatedEntity = _mapper.Map(alarmRuleRequest, entity);
                _alarmRuleRepository.Update(updatedEntity);
                await _alarmRuleRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit alarm rule");
                return serviceResult;
            }
        }

        public async Task<Result<AlarmRuleQueryModel>> GetAlarmRuleAsync(int alarmRuleId)
        {
            var serviceResult = new Result<AlarmRuleQueryModel>();
            try
            {
                var entity = await _alarmRuleRepository.GetBy(t => t.Id == alarmRuleId)
                                                    .FirstOrDefaultAsync();
                if (entity == null)
                {
                    serviceResult.Failed().WithError("alarm rule not found");
                }
                var queryModel = _mapper.Map<AlarmRuleQueryModel>(entity);
                serviceResult.Successful().WithData(queryModel);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get alarm rule by id");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<AlarmRuleQueryModel>>> GetAlarmRulesAsync()
        {
            var serviceResult = new Result<IEnumerable<AlarmRuleQueryModel>>();
            try
            {
                var datas = await _alarmRuleRepository.GetAll().ToListAsync();
                var queryModels = _mapper.Map<IEnumerable<AlarmRuleQueryModel>>(datas);
                serviceResult.Successful().WithData(queryModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get alarm rules");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<AlarmRuleQueryModel>>> GetAlarmRulesByPositionIdAsync(int positionId)
        {
            var serviceResult = new Result<IEnumerable<AlarmRuleQueryModel>>();
            try
            {
                var datas = await _alarmRuleRepository.GetBy(t=>t.PositionId == positionId && t.Active).ToListAsync();
                var queryModels = _mapper.Map<IEnumerable<AlarmRuleQueryModel>>(datas);
                serviceResult.Successful().WithData(queryModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get active alarm rules by position id");
            }

            return serviceResult;
        }

        public async Task<Result> SetAlarmRuleNotificationAsync(int alarmRuleId, NotificationSetting settings)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _alarmRuleRepository.GetSingleAsync(a => a.Id == alarmRuleId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("alarm rule not found");
                }
                entity.Notification= settings;
                _alarmRuleRepository.Update(entity);
                await _alarmRuleRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to set alarm rule notification users");
                return serviceResult;
            }
        }
    }
}
