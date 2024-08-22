using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.State.Navigators;

namespace RTS.HostBuilders;

public static class AddStoresHostBuilder
{
    public static IHostBuilder AddStores(this IHostBuilder host)
    {
        host.ConfigureServices(services => { services.AddSingleton<INavigator, Navigator>(); });

        return host;
    }
}