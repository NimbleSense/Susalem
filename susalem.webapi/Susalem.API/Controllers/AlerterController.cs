using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Susalem.Core.Application;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Alerter management.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = Roles.RootManagement)]
    [Authorize(Policy = Roles.DeviceControl)]
    [Route("api/[controller]")]
    [ApiController]
    public class AlerterController : ControllerBase
    {
        private readonly IApplicationDeviceService _applicationDeviceService;

        public AlerterController(IApplicationDeviceService applicationDeviceService)
        {
            _applicationDeviceService = applicationDeviceService;
        }

        /// <summary>
        /// Get all alerter
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var serviceResult = await _applicationDeviceService.GetDevicesByTypeNameAsync(DeviceTypeEnum.Alerter);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            return Ok(serviceResult.Data);
        }

        ///// <summary>
        ///// Get all alerter used for monitor
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("monitor")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetMonitorAlerter()
        //{
        //    var serviceResult = await _applicationDeviceService.GetAllMonitorAlerterAsync();
        //    if (serviceResult.Failed)
        //    {
        //        return BadRequest(serviceResult.MessageWithErrors);
        //    }
        //    return Ok(serviceResult.Data);
        //}

        ///// <summary>
        ///// Update alerter
        ///// </summary>
        ///// <param name="id">alerter id</param>
        ///// <param name="requestDto">alerter request</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> Put(int id, [FromBody] AlerterRequestDTO requestDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid payload");
        //    }
            
        //    var serviceResult = await _applicationDeviceService.EditAlerterAsync(id, requestDto);
        //    if (serviceResult.Failed)
        //    {
        //        return BadRequest(serviceResult.MessageWithErrors);
        //    }

        //    return NoContent();
        //}

        ///// <summary>
        ///// Delete alerter
        ///// </summary>
        ///// <param name="id">alerter id</param>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var serviceResult = await _applicationDeviceService.DeleteAlerterAsync(id);
        //    if (serviceResult.Failed)
        //    {
        //        return NotFound(serviceResult.MessageWithErrors);
        //    }

        //    return NoContent();
        //}
    }
}
