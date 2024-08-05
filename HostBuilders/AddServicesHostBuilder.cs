using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RTS.Models;
using RTS.Services;
using RTS.Services.Interfaces;

namespace RTS.HostBuilders;

public static class AddServicesHostBuilder
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IDataService<Vacancy>, DataService<Vacancy>>();
            
        });
        
        return host;
    }
}