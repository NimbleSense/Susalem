using AutoMapper;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Alarm;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Core.Application.ReadModels.Alerter;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Core.Application.ReadModels.Position;
using Susalem.Infrastructure.Models.Application;
using Susalem.Messages.Features.Channel;
using Susalem.Persistence.Models.Application;

namespace Susalem.Infrastructure.Mappings
{
    internal class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ApplicationConfigurationDTO, ApplicationConfigurationEntity>().ReverseMap();

            CreateMap<AreaDTO, AreaEntity>().ReverseMap();

            CreateMap<DeviceRequestDTO, DeviceEntity>().ReverseMap();
            CreateMap<DeviceEntity, DeviceQueryModel>()
                .ForMember(target=>target.DeviceTypeName, opt=>opt.MapFrom(source=>source.DeviceType.Name))
                .ForMember(target=>target.ChannelName, opt=>opt.MapFrom(source=>source.ChannelDevices.Channel.Name));

            CreateMap<DeviceEntity, DeviceOnlineStatusQueryModel>()
                .ForMember(target => target.DeviceTypeName, opt => opt.MapFrom(source => source.DeviceType.Name));


            CreateMap<PositionDeviceProperty, MonitorDeviceFunctionQueryModel>();

            CreateMap<DeviceTypeEntity, DeviceTypeQueryModel>();
            CreateMap<DeviceTypeProperty, DeviceTypePropertyQueryModel>();

            CreateMap<DeviceTypeProperty, DeviceFunctionProperty>();

            CreateMap<PositionEntity, PositionRequestDTO>().ReverseMap();

            CreateMap<PositionEntity, PositionQueryModel>()
                .ForMember(target=>target.AreaName, opt=> opt.MapFrom(source=>source.Area.Name));

            CreateMap<AreaEntity, MonitorAreaQueryModel>()
                .ForMember(target => target.Positions, opt => opt.MapFrom(source => source.Positions));

            CreateMap<PositionEntity, MonitorPositionQueryModel>()
                .ForMember(target => target.PositionFunctions, opt => opt.MapFrom(source => source.Functions));

            CreateMap<AlarmRequestDTO, AlarmEntity>().ReverseMap();

            CreateMap<PositionFunctionProperty, MonitorPositionFunctionQueryModel>()
                .ForMember(target=>target.DeviceFunctions, opt=>opt.MapFrom(source=>source.Devices));

            CreateMap<AlarmEntity, AlarmQueryModel>()
                .ForMember(target => target.PositionName,
                    opt => opt.MapFrom(source => source.Position.Name));

            CreateMap<AlerterEntity, AlerterMonitorQueryModel>();

            CreateMap<AlerterEntity, AlerterQueryModel>()
                .ForMember(target => target.Address, 
                    opt => opt.MapFrom(source => source.Device.Address))
                .ForMember(target=>target.Functions,opt=>opt.MapFrom(source=>source.Properties));

            CreateMap<AlerterEntity, AlerterInfoQueryModel>();

            CreateMap<AlerterRequestDTO, AlerterEntity>()
                .ForMember(target=>target.AffectedDevices, opt=>opt.MapFrom(source=>source.AffectedDevices));

            CreateMap<AlarmEntity, AlarmDetailQueryModel> ()
                .ForMember(target => target.PositionName, opt => opt.MapFrom(source => source.Position.Name));

            CreateMap<AlarmRuleEntity, AlarmRuleRequestDTO>().ReverseMap();
            CreateMap<AlarmRuleEntity, AlarmRuleQueryModel>().ReverseMap();

            CreateMap<ChannelEnitity, ChannelQueryModel>()
                .ForMember(target=>target.Devices, opt=>opt.MapFrom(souce=>souce.ChannelDevices.Count));
            CreateMap<ChannelEnitity, ChannelSettingDTO>().ReverseMap();

        }
    }
}
