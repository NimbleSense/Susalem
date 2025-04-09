using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Related with area and position management
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly ILogger<AreaController> _logger;
        private readonly IApplicationPositionService _applicationPositionService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AreaController(ILogger<AreaController> logger, 
            IApplicationPositionService applicationPositionService)
        {
            _logger = logger;
            _applicationPositionService = applicationPositionService;
        }

        /// <summary>
        /// Get All Areas
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetAreas()
        {
            var serviceResult = await _applicationPositionService.GetAreasAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get area by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetArea(int id)
        {
            var serviceResult = await _applicationPositionService.GetAreaAsync(id);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Create a new area
        /// </summary>
        /// <param name="areaDto">area</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AreaDTO areaDto)
        {
            var serviceResult = await _applicationPositionService.CreateAreaAsync(areaDto);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            var newAreaDto = serviceResult.Data;
            return CreatedAtAction(nameof(GetArea), new {Id = newAreaDto.Id}, newAreaDto);
        }

        /// <summary>
        /// Update a area
        /// </summary>
        /// <param name="id"></param>
        /// <param name="areaDto"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AreaDTO areaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            if (id != areaDto.Id)
            {
                return BadRequest();
            }

            var serviceResult = await _applicationPositionService.EditAreaAsync(id, areaDto);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete area
        /// </summary>
        /// <param name="id">Area id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _applicationPositionService.DeleteAreaAsync(id);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }


        /// <summary>
        /// Get all position by area id
        /// </summary>
        /// <param name="areaId">area id</param>
        /// <returns></returns>
        [HttpGet("{areaId}/positions")]
        public async Task<IActionResult> GetPositions(int areaId)
        {
            var serviceResult = await _applicationPositionService.GetPositionsByAreaIdAsync(areaId);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get all monitor areas include position and functions
        /// </summary>
        /// <returns></returns>
        [HttpGet("monitor")]
        public async Task<IActionResult> GetAllMonitorAreas()
        {
            var serviceResult = await _applicationPositionService.GetMonitorAreasAsync();
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }
    }
}