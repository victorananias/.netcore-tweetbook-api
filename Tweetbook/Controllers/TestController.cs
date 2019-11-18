using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tweetbook.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("user")]
        public IActionResult Get()
        {
            return Ok(new { name = "Victor" });
        }
    }
}