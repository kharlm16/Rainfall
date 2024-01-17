using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rainfall.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id/{stationId}/readings")]
        public IActionResult GetReadingByStationId(string stationId)
        {


            return Ok(new { Test = stationId });
        }
    }
}
