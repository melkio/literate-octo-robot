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
            var baseAddress = Environment.GetEnvironmentVariable("NEPTUNE_API_HOST_URL");
            var secondsTimeout = int.Parse(Environment.GetEnvironmentVariable("NEPTUNE_API_DEFAULT_SECONDS_TIMEOUT") ?? "100");

            client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(secondsTimeout)
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