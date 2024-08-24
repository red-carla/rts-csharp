using Microsoft.Extensions.DependencyInjection;
using RTS.Services;
using RTS.Stores;
using RTS.ViewModels;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RTS.EntityFramework;
using RTS.Models;
using RTS.HostBuilders;
using Application = System.Windows.Application;

namespace RTS
{
    public partial class App : Application
    {
        private readonly IHost _host;
        
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .AddDbContext()
                .AddDatabaseSeeder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<DatabaseSeeder>();
                    services.AddHostedService<DatabaseSeederService>();
                    services.AddSingleton<AccountStore>();
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<IDataService<Candidate>, DataService<Candidate>>();
                    services.AddSingleton<IDataService<Vacancy>, DataService<Vacancy>>();
                    services.AddSingleton<RecruitmentDbContextFactory>(s => new RecruitmentDbContextFactory(
                        options => options.UseSqlServer(context.Configuration.GetConnectionString("SqlServer")!)
                    ));

                    services.AddSingleton<INavigationService>(CreateHomeNavigationService);
                    services.AddSingleton<CloseModalNavigationService>();

                    services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateLoginNavigationService(s)));

                    services.AddTransient<AccountViewModel>(s => new AccountViewModel(
                        s.GetRequiredService<AccountStore>(),
                        CreateHomeNavigationService(s)));

                    services.AddTransient<LoginViewModel>(CreateLoginViewModel);

                    services.AddTransient<CandidateListingViewModel>(s => new CandidateListingViewModel(
                        CreateAddCandidateNavigationService(s), s.GetRequiredService<IDataService<Candidate>>()));

                    services.AddTransient<AddCandidateViewModel>(s => new AddCandidateViewModel(
                        s.GetRequiredService<IDataService<Candidate>>(),
                        s.GetRequiredService<CloseModalNavigationService>()
                    ));

                    services.AddTransient<VacancyListingViewModel>(s => new VacancyListingViewModel(
                        CreateAddVacancyNavigationService(s), s.GetRequiredService<IDataService<Vacancy>>()));

                    services.AddTransient<AddVacancyViewModel>(s => new AddVacancyViewModel(
                        s.GetRequiredService<IDataService<Vacancy>>(),
                        s.GetRequiredService<CloseModalNavigationService>()
                        
                    ));

                    services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<HomeViewModel>,
                serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<LoginViewModel>);
        }

        private INavigationService CreateAddCandidateNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<AddCandidateViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<AddCandidateViewModel>);
        }

        private INavigationService CreateAddVacancyNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<AddVacancyViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<AddVacancyViewModel>);
        }

        private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<AccountViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<AccountViewModel>,
                serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private INavigationService CreateCandidateListingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<CandidateListingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<CandidateListingViewModel>,
                serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private INavigationService CreateVacancyListingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<VacancyListingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<VacancyListingViewModel>,
                serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
        {
            CompositeNavigationService navigationService = new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseModalNavigationService>(),
                CreateAccountNavigationService(serviceProvider));

            return new LoginViewModel(
                serviceProvider.GetRequiredService<AccountStore>(),
                navigationService);
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<AccountStore>(),
                CreateHomeNavigationService(serviceProvider),
                CreateAccountNavigationService(serviceProvider),
                CreateLoginNavigationService(serviceProvider),
                CreateCandidateListingNavigationService(serviceProvider),
                CreateVacancyListingNavigationService(serviceProvider));
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}