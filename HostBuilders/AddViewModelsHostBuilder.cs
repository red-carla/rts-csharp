using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.State.Navigators;
using RTS.ViewModels;

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
                services.AddTransient<ApplicationDetailViewModel>();
                services.AddTransient<CandidateDetailViewModel>();
                services.AddTransient<VacancyDetailViewModel>();
                
                services.AddSingleton<CreateViewModel<HomeViewModel>>(s =>
                    s.GetRequiredService<HomeViewModel>);
                services.AddSingleton<CreateViewModel<VacancyListViewModel>>(s =>
                    s.GetRequiredService<VacancyListViewModel>);
                services.AddSingleton<CreateViewModel<ApplicationListViewModel>>(s =>
                    s.GetRequiredService<ApplicationListViewModel>);
                services.AddSingleton<CreateViewModel<ApplicationDetailViewModel>>(s =>
                    s.GetRequiredService<ApplicationDetailViewModel>);
                services.AddSingleton<CreateViewModel<CandidateListViewModel>>(s =>
                    s.GetRequiredService<CandidateListViewModel>);
                services.AddSingleton<CreateViewModel<CandidateDetailViewModel>>(s =>
                    s.GetRequiredService<CandidateDetailViewModel>);
                services.AddSingleton<CreateViewModel<VacancyDetailViewModel>>(s =>
                    s.GetRequiredService<VacancyDetailViewModel>);
                
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<MainViewModel>(s => new MainViewModel(
                    s.GetRequiredService<INavigator>(),
                    s.GetRequiredService<IViewModelFactory>()));
            });

            return host;
        }
     
    }
}