
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RTS.Models;
using RTS.Services;

namespace RTS.HostBuilders;

public static class AddServicesHostBuilder
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IDataService<Vacancy>, DataService<Vacancy>>();
            services.AddSingleton<IDataService<JobApplication>, DataService<JobApplication>>();
            services.AddSingleton<IDataService<Candidate>, DataService<Candidate>>();
            services.AddSingleton<IDataService<Recruiter>, DataService<Recruiter>>();
            
        });
        
        return host;
    }
}