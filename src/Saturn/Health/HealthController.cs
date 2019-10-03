using Microsoft.AspNetCore.Mvc;

namespace Saturn.Health
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