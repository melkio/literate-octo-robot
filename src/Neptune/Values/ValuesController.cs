using System;
using Microsoft.AspNetCore.Mvc;

namespace Neptune.Values
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var threshold = int.Parse(Environment.GetEnvironmentVariable("NEPTUNE_API_DELAY_THRESHOLD") ?? "200");

            if (DateTime.Now.Millisecond < threshold)
            {
                var secondsDelay = int.Parse(Environment.GetEnvironmentVariable("NEPTUNE_VALUES_API_SECONDS_DELAY") ?? "0");
                Run.For(TimeSpan.FromSeconds(secondsDelay));
            }

            var random = new Random(DateTime.Now.Millisecond);
            var value = random.Next(1, 10);

            return Ok(new { value = value });
        }
    }
}