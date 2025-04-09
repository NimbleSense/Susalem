using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces;
using Susalem.Infrastructure.Device.Engine;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Related with device and device types management.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = Roles.RootManagement)]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IEngineFactory _engineFactory;
        private readonly IApplicationDeviceService _applicationDeviceService;

        public DeviceController(ILogger<DeviceController> logger, 
            IEngineFactory engineFactory,
            IApplicationDeviceService applicationDeviceService)
        {
            _logger = logger;
            _engineFactory = engineFactory;
            _applicationDeviceService = applicationDeviceService;
        }

        /// <summary>
        /// Get devices online status.
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        public async Task<IActionResult> GetDevicesStatus()
        {
            var serviceResult = await _applicationDeviceService.GetDevicesWithStatusAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get all device types.
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        public async Task<IActionResult> GetDeviceTypes()
        {
            var serviceResult = await _applicationDeviceService.GetDeviceTypesAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get all devices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var serviceResult = await _applicationDeviceService.GetDevicesAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get all devices in device type.
        /// </summary>
        /// <param name="deviceTypeId">Device type id</param>
        /// <returns></returns>
        [HttpGet("type/{deviceTypeId}")]
        public async Task<IActionResult> GetDevicesByType(int deviceTypeId)
        {
            var serviceResult = await _applicationDeviceService.GetDevicesByTypeIdAsync(deviceTypeId);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get device.
        /// </summary>
        /// <param name="id">device id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var serviceResult = await _applicationDeviceService.GetDeviceAsync(id);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Create a new device
        /// </summary>
        /// <param name="deviceRequestDto">device request</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeviceRequestDTO deviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            var serviceResult = await _applicationDeviceService.CreateDeviceAsync(deviceRequestDto);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            var createdDeviceDto = serviceResult.Data;
            return CreatedAtAction(nameof(GetDevice), new {createdDeviceDto.Id }, createdDeviceDto);
        }

        /// <summary>
        /// Create a new device for channel
        /// </summary>
        /// <param name="channelId">通道id</param>
        /// <param name="deviceRequestDto">device request</param>
        [HttpPost("channel/{channelId}")]
        public async Task<IActionResult> Post(int channelId,[FromBody] DeviceRequestDTO deviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            var serviceResult = await _applicationDeviceService.CreateDeviceAsync(deviceRequestDto, channelId);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            var createdDeviceDto = serviceResult.Data;
            return CreatedAtAction(nameof(GetDevice), new { createdDeviceDto.Id }, createdDeviceDto);
        }

        /// <summary>

        /// <summary>
        /// Update a device
        /// </summary>
        /// <param name="id">device id</param>
        /// <param name="deviceRequestDto">device request</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DeviceRequestDTO deviceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            var serviceResult = await _applicationDeviceService.EditDeviceAsync(id, deviceRequestDto);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete device.
        /// </summary>
        /// <param name="id">device id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _applicationDeviceService.DeleteDeviceAsync(id);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        #region 设备调试

        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="id">device id</param>                  
        /// <returns></returns>
        [HttpPost("{id}/synctime")]
        public async Task<IActionResult> SyncTime(int id)
        {
            var engine = _engineFactory.GetEngineWithDeviceId(id);
            if (engine == null) return BadRequest();

            ((Cabinet)engine).SyncTime();
            return Ok();
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="id">device id</param>                  
        /// <returns></returns>
        [HttpPost("{id}/write")]
        public async Task<IActionResult> WriteData(int id, [FromBody]DebugData debugData)
        {
            var engine = _engineFactory.GetEngineWithDeviceId(id);
            if (engine == null) return BadRequest();

            ((Cabinet)engine).WriteDebugData(debugData);
            return Ok();
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="id">device id</param>                  
        /// <returns></returns>
        [HttpGet("{id}/debugdata")]
        public async Task<IActionResult> LoadDebugData(int id)
        {
            var engine = _engineFactory.GetEngineWithDeviceId(id);
            if (engine == null) return BadRequest();

            var cabinet = (Cabinet)engine;
            var result = cabinet.LoadDebugData();
            if(!result) return BadRequest();

            return Ok(cabinet.DebugDatas);
        }

        #endregion
    }
}
