using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Sample.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Extensive.Pipeline.CacheControl.Sample.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/teams")]
    public class TeamsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Enumerable.Empty<TeamResourceV1>());
        }

        [HttpGet("id")]
        public IActionResult GetOne()
        {
            return NotFound();
        }
    }
}
