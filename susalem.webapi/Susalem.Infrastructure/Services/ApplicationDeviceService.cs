using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Alerter;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Infrastructure.Device;
using Susalem.Infrastructure.Models.Application;
using Susalem.Persistence.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Services
{
    public class ApplicationDeviceService : IApplicationDeviceService
    {
        private readonly ILogger<ApplicationDeviceService> _logger;
        private readonly IMapper _mapper;
        private readonly IEngineFactory _engineFactory;
        private readonly IEntityRepository<AlerterEntity, int> _alerterRepository;
        private readonly IEntityRepository<DeviceTypeEntity, int> _deviceTypeRepository;
        private readonly IEntityRepository<DeviceEntity, int> _deviceRepository;
        private readonly IEntityRepository<ChannelDevicesEntity, int> _channelDevicesRepository;

        public ApplicationDeviceService(ILogger<ApplicationDeviceService> logger,
            IMapper mapper,
            IEngineFactory engineFactory,
            IEntityRepository<DeviceTypeEntity, int> deviceTypeRepository,
            IEntityRepository<DeviceEntity, int> deviceRepository,
            IEntityRepository<ChannelDevicesEntity, int> channelDevicesRepository,
            IEntityRepository<AlerterEntity, int> alerterRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _engineFactory = engineFactory;
            _deviceTypeRepository = deviceTypeRepository;
            _deviceRepository = deviceRepository;
            _alerterRepository = alerterRepository;
            _channelDevicesRepository = channelDevicesRepository;
        }


        #region Device

        public async Task<Result<IEnumerable<DeviceTypeQueryModel>>> GetDeviceTypesAsync()
        {
            var serviceResult = new Result<IEnumerable<DeviceTypeQueryModel>>();
            try
            {
                var deviceTypes = await _deviceTypeRepository.GetBy(d=> d.IsPublish).ToListAsync();
                var deviceTypeModels = _mapper.Map<IEnumerable<DeviceTypeQueryModel>>(deviceTypes);
                serviceResult.Successful().WithData(deviceTypeModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get device types");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesAsync()
        {
            var serviceResult = new Result<IEnumerable<DeviceQueryModel>>();
            try
            {
                var devices = await _deviceRepository.GetAll().Include(t=>t.DeviceType).ToListAsync();
                var deviceModels = _mapper.Map<IEnumerable<DeviceQueryModel>>(devices);
                serviceResult.Successful().WithData(deviceModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get devices");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<DeviceOnlineStatusQueryModel>>> GetDevicesWithStatusAsync()
        {
            var serviceResult = new Result<IEnumerable<DeviceOnlineStatusQueryModel>>();
            try
            {
                var devices = await _deviceRepository.GetAll().Include(t=>t.DeviceType).ToListAsync();
                var deviceModels = _mapper.Map<IEnumerable<DeviceOnlineStatusQueryModel>>(devices).ToList();
                foreach (var deviceModel in deviceModels)
                {
                    var engine = _engineFactory.GetEngineWithDeviceId(deviceModel.Id);
                    if (engine != null)
                    {
                        deviceModel.Online = engine.Property.Online;
                    }
                }
                serviceResult.Successful().WithData(deviceModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get devices with status");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesAsync(IEnumerable<int> deviceIds)
        {
            var serviceResult = new Result<IEnumerable<DeviceQueryModel>>();
            try
            {
                var devices = await _deviceRepository.GetBy(t=>deviceIds.Contains(t.Id)).ToListAsync();
                var deviceModels = _mapper.Map<IEnumerable<DeviceQueryModel>>(devices);
                serviceResult.Successful().WithData(deviceModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get devices");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesByTypeIdAsync(int deviceTypeId)
        {
            var serviceResult = new Result<IEnumerable<DeviceQueryModel>>();
            try
            {
                var devices = await _deviceRepository.GetBy(d=>d.DeviceTypeId==deviceTypeId).ToListAsync();
                var deviceModels = _mapper.Map<IEnumerable<DeviceQueryModel>>(devices);
                serviceResult.Successful().WithData(deviceModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get devices by deviceType id");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesByTypeNameAsync(DeviceTypeEnum deviceType)
        {
            var serviceResult = new Result<IEnumerable<DeviceQueryModel>>();
            try
            {
                var deviceTypeEntity = await _deviceTypeRepository.GetBy(t => t.Name == deviceType).Include(t=>t.Devices).ToListAsync();
                if (deviceTypeEntity == null)
                {
                    throw new ArgumentNullException(nameof(deviceType));
                }
                var deviceModels = _mapper.Map<IEnumerable<DeviceQueryModel>>(deviceTypeEntity.SelectMany(t=>t.Devices));
                return serviceResult.Successful().WithData(deviceModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get devices by deviceType name");
            }

            return serviceResult;
        }

        public async Task<Result<DeviceQueryModel>> CreateDeviceAsync(DeviceRequestDTO dto, int channelId =0)
        {
            var serviceResult = new Result<DeviceQueryModel>();
            try
            {
                var deviceEntity = _mapper.Map<DeviceEntity>(dto);

                await _deviceRepository.AddAsync(deviceEntity);

                if (channelId > 0)
                {
                    _channelDevicesRepository.Add(new ChannelDevicesEntity()
                    {
                        ChannelId = channelId,
                        Device = deviceEntity
                    });
                }

                await _deviceRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful().WithData(_mapper.Map<DeviceQueryModel>(deviceEntity));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create device.");
            }
            return serviceResult;
        }

        public async Task<Result<DeviceQueryModel>> CopyDeviceAsync(int deviceId, int channelId)
        {
            var serviceResult = new Result<DeviceQueryModel>();
            try
            {
                var entity = await _deviceRepository.FindAsync(deviceId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("device not found");
                }
                entity.Id = 0;
                await _deviceRepository.AddAsync(entity);

                if (channelId > 0)
                {
                    _channelDevicesRepository.Add(new ChannelDevicesEntity()
                    {
                        ChannelId = channelId,
                        Device = entity
                    });
                }

                await _deviceRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful().WithData(_mapper.Map<DeviceQueryModel>(entity));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create device.");
            }
            return serviceResult;
        }

        public async Task<Result> EditDeviceAsync(int deviceId, DeviceRequestDTO dto)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _deviceRepository.FindAsync(deviceId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("device not found");
                }

                _mapper.Map(dto, entity);
                _deviceRepository.Update(entity);
                await _deviceRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit area");
                return serviceResult;
            }
        }

        public async Task<Result> DeleteDeviceAsync(int deviceId)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _deviceRepository.FindAsync(deviceId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("device not found");
                }
                _deviceRepository.Delete(entity);
                await _deviceRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete device");
                return serviceResult;
            }
        }

        public async Task<Result<DeviceQueryModel>> GetDeviceAsync(int deviceId)
        {
            var serviceResult = new Result<DeviceQueryModel>();
            try
            {
                var device = await _deviceRepository.GetBy(t=>t.Id == deviceId)
                                                    .Include(t=>t.DeviceType)
                                                    .FirstOrDefaultAsync();
                if (device == null)
                {
                    serviceResult.Failed().WithError("device not found");
                }
                var deviceModel = _mapper.Map<DeviceQueryModel>(device);
                serviceResult.Successful().WithData(deviceModel);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get device by id");
            }

            return serviceResult;
        }

        #endregion

    }
}
