using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.Services;
using RTS.ViewModels;

namespace RTS.HostBuilders;

public static class AddViewsHostBuilder
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
        });

        return host;
    }
}