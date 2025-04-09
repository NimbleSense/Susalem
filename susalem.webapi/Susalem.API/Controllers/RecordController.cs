using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Susalem.Api.Interfaces;
using Susalem.Core.Application;
using Susalem.Core.Application.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Record;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Susalem.Infrastructure.Device.Constants;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Localize;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Record management
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/records")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IRecordService _recordService;
        private readonly IApplicationConfigurationService _configurationService;
        private readonly IStringLocalizer<Resource> _localizer;

        public RecordController(IReportService reportService,
            IRecordService recordService,
            IApplicationConfigurationService configurationService,
            IStringLocalizer<Resource> localizer)
        {
            _reportService = reportService;
            _recordService = recordService;
            _configurationService = configurationService;
            _localizer = localizer;
        }


        /// <summary>
        /// Get all records by page parameter
        /// </summary>
        /// <param name="parameters">Page parameter</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRecords([FromQuery] RecordParameters parameters)
        {
            var records = _recordService.GetRecords(parameters);
            var metadata = new
            {
                records.TotalCount,
                records.PageSize,
                records.CurrentPage,
                records.TotalPages,
                records.HasNext,
                records.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(records);
        }

        /// <summary>
        /// Get pdf report
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="positionFunction"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [Route("report/{positionId}/{positionFunction}")]
        [HttpGet]
        public async Task<string> GetReport(int positionId, PositionFunctionEnum positionFunction, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            var result = await _recordService.GetRecordsAsync(positionId, positionFunction, startTime, endTime);
            if (result.Failed)
            {
                //return BadRequest();
                return string.Empty;
            }

            var headers = new List<string>
            {
                _localizer["Position"].Value,
                _localizer["Time"].Value
            };
            //if (positionFunction == PositionFunctionEnum.Humiture)
            //{
            //    headers.Add(_localizer["Temperature"].Value);
            //    headers.Add(_localizer["Humidity"].Value);
            //}

            var headerName = "智能物联控制系统";
            var records = result.Data.ToList();
            if (records.Count > 0)
            {
                headerName = records[0].PositionName;
            }

            var companyName = _localizer["Report"].Value;
            var companyNameResult = _configurationService.GetValue(Configuration.CustomerNameKey);
            if (companyNameResult.Succeeded)
            {
                companyName = companyNameResult.Data;
            }
            var fileName = _reportService.GeneratePdfReport(companyName, $"{headerName}", headers, result.Data);

            return fileName;
        }

        /// <summary>
        /// Get records for chart
        /// </summary>
        /// <param name="recordRequest"></param>
        /// <returns></returns>
        [Route("chart")]
        [HttpGet]
        public async Task<IActionResult> GetRecords([FromQuery] RecordChartRequestDTO recordRequest)
        {
            var result = await _recordService.GetChartRecordsAsync(recordRequest);
            if (result.Failed)
            {
                return BadRequest(result.MessageWithErrors);
            }

            return Ok(result.Data);
        }
    }
}
