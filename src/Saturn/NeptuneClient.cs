using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Saturn
{
    public class NeptuneClient
    {
        private readonly HttpClient client;

        public NeptuneClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("NEPTUNE_API_HOST_URL"))
            };
        }

        public async Task<NeptuneResponse> Get()
        {
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NeptuneResponse>(json);
        }
    }

    public class NeptuneResponse
    {
        public int Value { get; set; }
    }
}