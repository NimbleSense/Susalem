using System;
using System.Collections.Generic;
using AutoMapper;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Infrastructure.Models.Application;

namespace Susalem.Infrastructure.Mappings.Resolvers
{
    internal class DeviceResolver : IValueResolver<PositionRequestDTO,PositionEntity, ICollection<DeviceEntity>>
    {
        private readonly IEntityRepository<DeviceEntity, int> _deviceRepository;

        public DeviceResolver(IEntityRepository<DeviceEntity, int> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public ICollection<DeviceEntity> Resolve(PositionRequestDTO source, PositionEntity destination, ICollection<DeviceEntity> destMember,
            ResolutionContext context)
        {

            //var deviceEntities = new List<DeviceEntity>();
            //var ids = source.Devices;
            //foreach (var id in ids)
            //{
            //    var deviceEntity = _deviceRepository.Find(id);
            //    if (deviceEntity != null)
            //    {
            //        deviceEntities.Add(deviceEntity);
            //    }
            //}

            //return deviceEntities;
            return null;
        }
    }
}
