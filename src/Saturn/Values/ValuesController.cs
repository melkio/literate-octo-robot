using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;

namespace Saturn.Values
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory factory;
        private readonly CircuitBreakerPolicy policy;

        public ValuesController(IHttpClientFactory factory, CircuitBreakerPolicy policy)
        {
            this.factory = factory;
            this.policy = policy;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var seed = random.Next(1, 10);

            return Ok(new { value = seed });
        }

        [HttpGet("detailed")]
        public Task<ActionResult> GetDetailed()
            => policy.ExecuteAsync(() => GetDetailedInner());

        private async Task<ActionResult> GetDetailedInner()
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