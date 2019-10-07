using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;

namespace Saturn
{
    public static class HttpClientFactoryExtensions
    {
        public static void AddNeptuneClient(this IServiceCollection services)
        {
            var baseAddress = Environment.GetEnvironmentVariable("NEPTUNE_API_HOST_URL");
            var secondsTimeout = int.Parse(Environment.GetEnvironmentVariable("NEPTUNE_API_DEFAULT_SECONDS_TIMEOUT") ?? "100");

            var policy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(15)
                );
            services.AddSingleton<CircuitBreakerPolicy>(policy);

            services.AddHttpClient("Neptune", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(secondsTimeout);
            });
        }
    }
}