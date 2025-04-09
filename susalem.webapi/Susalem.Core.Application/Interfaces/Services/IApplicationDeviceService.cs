using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Alerter;
using Susalem.Core.Application.ReadModels.Device;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IApplicationDeviceService
    {
        #region DeviceType

        Task<Result<IEnumerable<DeviceTypeQueryModel>>> GetDeviceTypesAsync();

        #endregion

        #region Device

        Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesAsync();

        Task<Result<IEnumerable<DeviceOnlineStatusQueryModel>>> GetDevicesWithStatusAsync();

        Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesAsync(IEnumerable<int> deviceIds);

        Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesByTypeIdAsync(int deviceTypeId);

        Task<Result<IEnumerable<DeviceQueryModel>>> GetDevicesByTypeNameAsync(DeviceTypeEnum deviceType);

        Task<Result<DeviceQueryModel>> CreateDeviceAsync(DeviceRequestDTO dto, int channleId = 0);

        Task<Result<DeviceQueryModel>> CopyDeviceAsync(int deviceId, int channleId);

        Task<Result> EditDeviceAsync(int deviceId, DeviceRequestDTO dto);

        Task<Result> DeleteDeviceAsync(int deviceId);
         
        Task<Result<DeviceQueryModel>> GetDeviceAsync(int deviceId);

        #endregion

        //#region Alerter

        //Task<Result<IEnumerable<AlerterQueryModel>>> GetAllAlerterAsync();

        //Task<Result<IEnumerable<AlerterMonitorQueryModel>>> GetAllMonitorAlerterAsync();

        //Task<Result> EditAlerterAsync(int alerterId, AlerterRequestDTO dto);

        //Task<Result> DeleteAlerterAsync(int alerterId);

        //Task<Result<AlerterInfoQueryModel>> GetAlerterByDeviceAsync(int deviceId);

        //Task<Result<AlerterInfoQueryModel>> CreateAlerterAsync(int deviceId, AlerterRequestDTO dto);

        //#endregion
    }
}
