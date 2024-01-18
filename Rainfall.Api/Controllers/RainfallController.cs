using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Models.Response;
using Rainfall.Api.Services;

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
            RainfallPublicService rainfallService = new RainfallPublicService();

            var result = await rainfallService.GetReadingsByStation(stationId, count);

            return Ok(result);
        }
    }
}
