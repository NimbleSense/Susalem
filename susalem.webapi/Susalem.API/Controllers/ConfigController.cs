using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Susalem.Core.Application;
using Susalem.Core.Application.Commands.Config;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Queries.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Susalem.Core.Application.Models;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Related with system configuration settings.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IServiceBus _serviceBus;
        private readonly IApplicationConfigurationService _configurationService;
        private readonly IPlatformService _platformService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceBus"></param>
        /// <param name="configurationService"></param>
        /// <param name="platformService"></param>
        /// <param name="licenseService"></param>
        /// <param name="copyright"></param>
        public ConfigController(IServiceBus serviceBus, 
            IApplicationConfigurationService configurationService, 
            IPlatformService platformService)
        {
            _serviceBus = serviceBus;
            _configurationService = configurationService;
            _platformService = platformService;
        }

        /// <summary>
        /// Get system application config items.
        /// </summary>
        /// <returns></returns>
        [HttpGet("system")]
        [Authorize(Policy = Roles.RootManagement)]
        public async Task<IActionResult> GetSystem()
        {
            var systemConfigDto = new SystemConfigurationDTO();
            var properties = systemConfigDto.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var serviceResult = await _serviceBus.Send(new GetConfigQuery(propertyInfo.Name));
                if (serviceResult.Succeeded)
                {
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(systemConfigDto, bool.Parse(serviceResult.Data.Value));
                    }
                    else
                    {
                        propertyInfo.SetValue(systemConfigDto, serviceResult.Data.Value);
                    }
                }
            }

            return Ok(systemConfigDto);
        }

        /// <summary>
        /// Update system application config items.
        /// </summary>
        /// <returns></returns>
        [HttpPost("system")]
        [Authorize(Policy = Roles.RootManagement)]
        public async Task<IActionResult> UpdateSystem(SystemConfigurationDTO systemConfigDto)
        {
            var properties = systemConfigDto.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var key = propertyInfo.Name;
                var configDto = new ApplicationConfigurationDTO
                {
                    Id = key,
                    Value = propertyInfo.GetValue(systemConfigDto)?.ToString()
                };

                await _serviceBus.Send(new UpdateApplicationConfigurationCommand(key, configDto));
            }

            return NoContent();
        }

        /// <summary>
        /// Get database backup configuration.
        /// </summary>
        /// <returns></returns>
        [HttpGet("db/backup")]
        public IActionResult GetDbBackup()
        {
            var result = _configurationService.GetValue(Configuration.DbBackUpKey);
            if (result.Failed)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(result.Data))
            {
                return Ok(new DbConfigurationDTO());
            }

            return Ok(JsonConvert.DeserializeObject<DbConfigurationDTO>(result.Data));
        }

        /// <summary>
        /// Get usable serial ports from computer.
        /// </summary>
        /// <returns></returns>
        [HttpGet("serialports")]
        [Authorize(Policy = Roles.RootManagement)]
        public IActionResult GetSerialPorts()
        {
            return Ok(_platformService.AvailablePorts());
        }

        /// <summary>
        /// Get application config item detail.
        /// Id:CustomerName (used to set customer company name)
        /// Id:DefaultPassword (used to set default password for new created user)
        /// </summary>
        /// <param name="id">Config item id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var serviceResult = await _serviceBus.Send(new GetConfigQuery(id));
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Update application config item.
        /// </summary>
        /// <param name="id">Config item key</param>
        /// <param name="applicationConfigurationDto"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ApplicationConfigurationDTO applicationConfigurationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            if (!id.Equals(applicationConfigurationDto.Id))
            {
                return BadRequest();
            }

            var result = await _serviceBus.Send(new UpdateApplicationConfigurationCommand(id, applicationConfigurationDto));
            if (result.Failed)
            {
                return BadRequest(result.MessageWithErrors);
            }

            return Ok();
        }

        #region 监控参数

        /// <summary>
        /// Get monitor setting.
        /// </summary>
        /// <returns></returns>
        [HttpGet("monitor/setting")]
        public IActionResult GetMonitorSetting()
        {
            var result = _configurationService.GetValue(Configuration.MonitorSettingKey);
            if (result.Failed)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(result.Data))
            {
                return Ok(new MonitorSetting());
            }

            return Ok(JsonConvert.DeserializeObject<MonitorSetting>(result.Data));
        }

        /// <summary>
        /// Update monitor setting.
        /// </summary>
        /// <returns></returns>
        [HttpPost("monitor/setting")]
        public async Task<IActionResult> UpdateMonitorSetting([FromBody]MonitorSetting monitorSetting)
        {
            var configDto = new ApplicationConfigurationDTO
            {
                Id = Configuration.MonitorSettingKey,
                Value = JsonConvert.SerializeObject(monitorSetting)
            };

            var result = await _serviceBus.Send(new UpdateApplicationConfigurationCommand(configDto.Id, configDto));
            if (result.Failed)
            {
                return BadRequest();
            }
            return NoContent();
        }

        #endregion
    }
}
