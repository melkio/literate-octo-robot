using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Saturn.Values
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory factory;

        public ValuesController(IHttpClientFactory factory)
        {
            this.factory = factory;
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

            var client = factory.CreateClient("Neptune");
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeAnonymousType(content, new { value = "" });

            var model = new { saturn = seed, neptune = json.value };
            return Ok(model);
        }
    }
}