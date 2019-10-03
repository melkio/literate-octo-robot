using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Saturn.Values
{
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var seed = random.Next(1, 10);

            return Ok(new { value = seed });
        }
    }
}