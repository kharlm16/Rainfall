using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Models.Response;
using Rainfall.Api.Services;
using System.Net;

namespace Rainfall.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <response code="200">A list of rainfall readings successfully retrieved</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">No readings found for the specified stationId</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("id/{stationId}/readings")]
        [ProducesResponseType(typeof(RainfallReadingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> GetReadingByStationId(string stationId, int count = 10)
        {
            Error paramError = new Error
            {
                Detail = new List<ErrorDetail>()
            };
            if (stationId == null)
            {
                paramError.Detail.Add(new ErrorDetail
                {
                    PropertyName = "stationId",
                    Message = "Station ID is required."
                });
            }

            if (count < 1 || count > 100)
            {
                paramError.Detail.Add(new ErrorDetail
                {
                    PropertyName = "count",
                    Message = "The parameter 'count' must have a minimum value of 1 and a maximum of 100."
                });
            }

            if (paramError.Detail.Count > 0)
            {
                return BadRequest(paramError);
            }

            RainfallPublicService rainfallService = new RainfallPublicService();

            try
            {
                var result = await rainfallService.GetReadingsByStation(stationId, count);

                if (result.Readings.Count == 0)
                {
                    Error error = new Error
                    {
                        Message = "No readings found for the specified station id."
                    };

                    return NotFound(error);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Error error = new Error
                {
                    Message = ex.Message
                };

                return StatusCode(500, error);
            }
        }
    }
}
