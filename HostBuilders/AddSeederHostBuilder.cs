using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RTS.EntityFramework;
using RTS.Services;

namespace RTS.HostBuilders
{

    public static class AddSeederHostBuilder
    {
        public static IHostBuilder AddDatabaseSeeder(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddHostedService<DatabaseSeederService>();
            });

            return host;
        }
    }
}