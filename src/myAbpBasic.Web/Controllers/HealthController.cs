using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace myAbpBasic.Web.Controllers
{
    [DontWrapResult]
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : myAbpBasicControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}