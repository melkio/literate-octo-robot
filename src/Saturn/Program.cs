using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Saturn
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
