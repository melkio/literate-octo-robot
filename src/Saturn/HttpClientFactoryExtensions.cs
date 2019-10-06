using System;
using Microsoft.Extensions.DependencyInjection;

namespace Saturn
{
    public static class HttpClientFactoryExtensions
    {
        public static void AddNeptuneClient(this IServiceCollection services)
        {
            var baseAddress = Environment.GetEnvironmentVariable("NEPTUNE_API_HOST_URL");
            var secondsTimeout = int.Parse(Environment.GetEnvironmentVariable("NEPTUNE_API_DEFAULT_SECONDS_TIMEOUT") ?? "100");

            services.AddHttpClient("Neptune", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(secondsTimeout);
            });
        }
    }
}