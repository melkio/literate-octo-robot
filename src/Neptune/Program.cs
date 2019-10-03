using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Neptune
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
