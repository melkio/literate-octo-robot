using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Saturn.Values
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private readonly NeptuneClient client;

        public ValuesController(NeptuneClient client)
        {
            this.client = client;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var seed = random.Next(1, 10);

            return Ok(new { value = seed });
        }

        [HttpGet("detailed")]
        public async Task<ActionResult> GetDetailed()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var seed = random.Next(1, 10);

            var response = await client.Get();

            var model = new { saturn = seed, neptune = response.Value };
            return Ok(model);
        }
    }
}