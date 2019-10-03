using Microsoft.AspNetCore.Mvc;

namespace Neptune.Health
{
    [Route("health")]
    public class HealthController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok(new { status = "healthy" });
        }
    }
}