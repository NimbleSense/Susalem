using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Susalem.Core.Application;
using Susalem.Core.Application.ReadModels.Position;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Related with position management.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ILogger<PositionController> _logger;
        private readonly IApplicationPositionService _applicationPositionService;

        public PositionController(ILogger<PositionController> logger, 
            IApplicationPositionService applicationPositionService)
        {
            _logger = logger;
            _applicationPositionService = applicationPositionService;
        }

        /// <summary>
        /// Get position functions
        /// </summary>
        /// <returns></returns>
        [HttpGet("functions")]
        public IActionResult GetPositionFunctions()
        {
            return Ok(Enum.GetNames(typeof(PositionFunctionEnum)).ToList());
        }

        /// <summary>
        /// Get all positions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var serviceResult = await _applicationPositionService.GetPositionsAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get all positions with areas cascade
        /// </summary>
        /// <returns></returns>
        [HttpGet("cascade")]
        public async Task<IActionResult> GetCascadeWithAreas()
        {
            var serviceResult = await _applicationPositionService.GetPositionsAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            
            return Ok(AreaCascadeQueryModel.ConvertToCascade(serviceResult.Data));
        }

        /// <summary>
        /// Get specific position
        /// </summary>
        /// <param name="positionId">position Id</param>
        /// <returns></returns>
        [HttpGet("{positionId}")]
        public async Task<IActionResult> GetPosition(int positionId)
        {
            var serviceResult = await _applicationPositionService.GetPositionAsync(positionId);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Create a position
        /// </summary>
        /// <param name="requestDto">Position request</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Roles.RootManagement)]
        public async Task<IActionResult> Post([FromBody] PositionRequestDTO requestDto)
        {
            var serviceResult = await _applicationPositionService.CreatePositionAsync(requestDto);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            var positionModel = serviceResult.Data;
            return CreatedAtAction(nameof(GetPosition), new { positionId = positionModel.Id }, positionModel);
        }

        /// <summary>
        /// Update a position
        /// </summary>
        /// <param name="positionId">position id</param>
        /// <param name="requestDto">Position request</param>
        [HttpPut("{positionId}")]
        [Authorize(Policy = Roles.RootManagement)]
        public async Task<IActionResult> Put(int positionId, [FromBody] PositionRequestDTO requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            var serviceResult = await _applicationPositionService.EditPositionAsync(positionId, requestDto);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete position.
        /// </summary>
        /// <param name="id">position id</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = Roles.RootManagement)]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _applicationPositionService.DeletePositionAsync(id);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Update position functions
        /// </summary>
        /// <param name="positionId">position id</param>
        /// <param name="positionFunctions">position functions and related devices</param>
        [HttpPut("{positionId}/function")]
        [Authorize(Policy = Roles.RootManagement)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePositionFunction(int positionId, [FromBody]IList<PositionFunctionProperty> positionFunctions)
        {
            var serviceResult = await _applicationPositionService.UpdateFunctionAsync(positionId, positionFunctions);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete position function
        /// </summary>
        /// <param name="positionId">position id</param>
        /// <param name="positionFunction">position function name</param>
        [HttpDelete("{positionId}/function/{positionFunction}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePositionFunction(int positionId, PositionFunctionEnum positionFunction)
        {
            var serviceResult = await _applicationPositionService.DeleteFunctionAsync(positionId, positionFunction);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }
    }
}
