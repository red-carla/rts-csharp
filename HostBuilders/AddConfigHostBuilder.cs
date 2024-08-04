using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace RTS.HostBuilders;

public static class AddConfigHostBuilder
{
    public static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration(c =>
        {
            c.AddJsonFile("appsettings.json");
            c.AddEnvironmentVariables();
        });

        return host;
    }
}