using AutoMapper;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Messages.Features.Channel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Susalem.Persistence.Models.Application;
using Susalem.Common.Extensions;
using Susalem.Core.Application;
using Microsoft.EntityFrameworkCore;
using Susalem.Infrastructure.Models.Application;

namespace Susalem.Infrastructure.Services;

public class ChannelService : IChannelService
{
    private readonly ILogger<ChannelService> _logger;
    private readonly IMapper _mapper;
    private readonly IEntityRepository<DeviceEntity, int> _deviceRepository;
    private readonly IEntityRepository<ChannelEnitity, int> _channelRepository;
    private readonly IEntityRepository<ChannelDevicesEntity, int> _channelDevicesRepository;

    public ChannelService(ILogger<ChannelService> logger,
            IMapper mapper,
            IEntityRepository<DeviceEntity, int> deviceRepository,
            IEntityRepository<ChannelEnitity, int> channelRepository,
            IEntityRepository<ChannelDevicesEntity, int> channelDevicesRepository) 
    {
        _logger = logger;
        _mapper = mapper;
        _deviceRepository = deviceRepository;
        _channelRepository = channelRepository;
        _channelDevicesRepository = channelDevicesRepository;
    }

    public async Task<Result> AddDevicesToChannelAsync(int id)
    {
        var serviceResult = new Result();
        try
        {
            var devices = _deviceRepository.GetAll().Select(t=>t.Id).ToList();
            foreach(var deviceId in devices)
            {
                var channelDeviceEntity = new ChannelDevicesEntity()
                {
                    ChannelId = id,
                    DeviceId = deviceId
                };
                _channelDevicesRepository.Add(channelDeviceEntity);
            }
            await _channelDevicesRepository.UnitOfWork.SaveChangesAsync();

            return serviceResult.Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to add devices to channel.");
        }
        return serviceResult;
    }

    public async Task<Result<ChannelQueryModel>> CreateChannelAsync(ChannelSettingDTO channelDto)
    {
        var serviceResult = new Result<ChannelQueryModel>();
        try
        {
            var channelEntity = _mapper.Map<ChannelEnitity>(channelDto);
            channelEntity.CreateTime= DateTime.Now;

            await _channelRepository.AddAsync(channelEntity);
            await _channelRepository.UnitOfWork.SaveChangesAsync();

            return serviceResult.Successful().WithData(_mapper.Map<ChannelQueryModel>(channelEntity));
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create channel.");
        }
        return serviceResult;
    }

    public async Task<Result> DeleteChannelAsync(int id)
    {
        var serviceResult = new Result();
        try
        {
            var entity = await _channelRepository.FindAsync(id);
            if (entity == null)
            {
                return serviceResult.Failed().WithError("channel not found");
            }
            _channelRepository.Delete(entity);
            await _channelRepository.UnitOfWork.SaveChangesAsync();

            return serviceResult.Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete channel");
            return serviceResult;
        }
    }

    public async Task<Result> EditChannelAsync(int channelId, ChannelSettingDTO dto)
    {
        var serviceResult = new Result();
        try
        {
            var entity = await _channelRepository.GetSingleAsync(a => a.Id == channelId);
            if (entity == null)
            {
                return serviceResult.Failed().WithError("channel not found");
            }

            _mapper.Map(dto, entity);
            _channelRepository.Update(entity);
            await _channelRepository.UnitOfWork.SaveChangesAsync();

            return serviceResult.Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit channel");
            return serviceResult;
        }
    }

    public async Task<Result<ChannelQueryModel>> GetChannelAsync(int id)
    {
        var serviceResult = new Result<ChannelQueryModel>();
        try
        {
            var channel = await _channelRepository.GetBy(t => t.Id == id)
                                                .Include(t => t.ChannelDevices)
                                                .FirstOrDefaultAsync();
            if (channel == null)
            {
                serviceResult.Failed().WithError("channel not found");
            }
            var channelModel = _mapper.Map<ChannelQueryModel>(channel);
            serviceResult.Successful().WithData(channelModel);
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get channel by id");
        }

        return serviceResult;
    }

    public async Task<Dictionary<int, List<int>>> GetChannelDevicesAsync()
    {
        var mapping = new Dictionary<int, List<int>>();
        try
        {
            var channelDevices = await _channelDevicesRepository.GetAll().ToListAsync();
            foreach(var channelDevice in channelDevices)
            {
                if(mapping.ContainsKey(channelDevice.ChannelId))
                {
                    mapping[channelDevice.ChannelId].Add(channelDevice.DeviceId);
                }
                else
                {
                    mapping.Add(channelDevice.ChannelId, new List<int>() { channelDevice.DeviceId });
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while trying to get channel devices, {Exception}", ex);
        }

        return mapping;
    }

    public async Task<Result<List<ChannelQueryModel>>> GetChannelsAsync()
    {
        var serviceResult = new Result<List<ChannelQueryModel>>();
        try
        {
            var channels = await _channelRepository.GetAll().Include(t=>t.ChannelDevices).ToListAsync();
            var channelModels = _mapper.Map<IEnumerable<ChannelQueryModel>>(channels);
            serviceResult.Successful().WithData(channelModels.ToList());
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get channels");
        }

        return serviceResult;
    }

    public async Task<Result<IList<DeviceOnlineStatusQueryModel>>> GetDevicesAsync(int id)
    {
        var serviceResult = new Result<IList<DeviceOnlineStatusQueryModel>>();
        try
        {
            var devices = await _channelDevicesRepository.GetBy(t=>t.ChannelId ==id)
                                                            .Include(t=>t.Device)
                                                            .Select(t=>t.Device)
                                                            .ToListAsync();
            var deviceModels = _mapper.Map<IEnumerable<DeviceOnlineStatusQueryModel>>(devices);
            serviceResult.Successful().WithData(deviceModels.ToList());
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get devices from channel");
        }

        return serviceResult;
    }

    public async Task<Result> RemoveDevicesFromChannelAsync(int id)
    {
        var serviceResult = new Result();
        try
        {
            var channelDevices = _channelDevicesRepository.GetBy(t=>t.ChannelId == id).ToList();
            _channelDevicesRepository.DeleteRange(channelDevices);

            await _channelDevicesRepository.UnitOfWork.SaveChangesAsync();
            return serviceResult.Successful();
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to remove devices from channel.");
        }
        return serviceResult;
    }
}
