using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.Models;
using RTS.Services.Interfaces;
using RTS.ViewModels;
using RTS.ViewModels.Navigators;

namespace RTS.HostBuilders
{
    public static class AddViewModelsHostBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<HomeViewModel>();
                services.AddTransient<VacancyListViewModel>();
                services.AddTransient<ApplicationListViewModel>();
                services.AddTransient<CandidateListViewModel>();
                
                services.AddSingleton<CreateViewModel<HomeViewModel>>(s =>
                    s.GetRequiredService<HomeViewModel>);
                services.AddSingleton<CreateViewModel<VacancyListViewModel>>(s =>
                    s.GetRequiredService<VacancyListViewModel>);
                services.AddSingleton<CreateViewModel<ApplicationListViewModel>>(s =>
                    s.GetRequiredService<ApplicationListViewModel>);
                services.AddSingleton<CreateViewModel<CandidateListViewModel>>(s =>
                    s.GetRequiredService<CandidateListViewModel>);
                
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<MainViewModel>(s => new MainViewModel(
                    s.GetRequiredService<INavigator>(),
                    s.GetRequiredService<IViewModelFactory>()));
            });

            return host;
        }
        
    }
}