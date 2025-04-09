using System.Collections.Generic;
using AutoMapper;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.ReadModels.Position;
using Susalem.Infrastructure.Device.Constants;

namespace Susalem.Infrastructure.Mappings.Resolvers
{
    internal class DeviceFunctionValueUnitResolver : IValueResolver<PositionDeviceProperty, MonitorDeviceFunctionQueryModel, string>
    {
        private static readonly Dictionary<string, string> DeviceFunctionsUnit = new()
        {
            {DeviceConstants.Temperature, "℃"},
            {DeviceConstants.Humidity, "%RH"}
        };

        public DeviceFunctionValueUnitResolver()
        {

        }

        public string Resolve(PositionDeviceProperty source, MonitorDeviceFunctionQueryModel destination, string destMember,
            ResolutionContext context)
        {
            if (DeviceFunctionsUnit.ContainsKey(source.Key))
            {
                return DeviceFunctionsUnit[source.Key];
            }

            return string.Empty;
        }

        public static string GetUnit(string key)
        {
            if (DeviceFunctionsUnit.ContainsKey(key))
            {
                return DeviceFunctionsUnit[key];
            }

            return key;
        }
    }
}
