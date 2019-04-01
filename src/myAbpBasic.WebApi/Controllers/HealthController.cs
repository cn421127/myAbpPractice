using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace myAbpBasic.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}