using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Neptune.Values
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            int delay = 0;
            int.TryParse(Environment.GetEnvironmentVariable("VALUES_API_DELAY"), out delay);

            var random = new Random(DateTime.Now.Millisecond);
            var seed = random.Next(1, 10);

            await Task.Delay(seed * delay);

            return Ok(new { value = seed });
        }
    }
}