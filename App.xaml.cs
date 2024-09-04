using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTS.EntityFramework;
using RTS.HostBuilders;
using RTS.Models;
using RTS.Services;
using RTS.Stores;
using RTS.ViewModels;
using Wpf.Ui.Controls;
using Application = System.Windows.Application;

namespace RTS;

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
                services.AddSingleton<IDataService<JobApplication>, DataService<JobApplication>>();
                services.AddSingleton<IDataService<ApplicationStage>, DataService<ApplicationStage>>();

                services.AddSingleton<RecruitmentDbContextFactory>(s => new RecruitmentDbContextFactory(
                    options => options.UseSqlServer(context.Configuration.GetConnectionString("SqlServer")!)
                ));

                services.AddSingleton(CreateHomeNavigationService);
                services.AddSingleton<CloseModalNavigationService>();

                services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateLoginNavigationService(s)));

                services.AddTransient(CreateLoginViewModel);

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
                services.AddTransient<JobApplicationListingViewModel>(s => new JobApplicationListingViewModel(
                    s.GetRequiredService<IDataService<JobApplication>>(),
                    s.GetRequiredService<IDataService<Candidate>>(),
                    s.GetRequiredService<IDataService<Vacancy>>(),
                    s.GetRequiredService<IDataService<ApplicationStage>>(),
                    CreateAddJobApplicationNavigationService(s)
                ));
                services.AddTransient<AccountViewModel>(s => new AccountViewModel(
                    s.GetRequiredService<AccountStore>(),
                    CreateHomeNavigationService(s),
                    s.GetRequiredService<IDataService<Vacancy>>(),
                    s.GetRequiredService<IDataService<Candidate>>()
                ));


                services.AddTransient<AddJobApplicationViewModel>(s => new AddJobApplicationViewModel(
                    s.GetRequiredService<IDataService<JobApplication>>(),
                    s.GetRequiredService<IDataService<Candidate>>(),
                    s.GetRequiredService<IDataService<Vacancy>>(),
                    s.GetRequiredService<IDataService<ApplicationStage>>(),
                    s.GetRequiredService<CloseModalNavigationService>()
                ));

                services.AddTransient(CreateNavigationBarViewModel);
                services.AddSingleton<MainViewModel>();

                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();
        var initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
        initialNavigationService.Navigate();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<LoginViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<LoginViewModel>,
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

    private INavigationService CreateAddJobApplicationNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<AddJobApplicationViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<AddJobApplicationViewModel>);
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

    private INavigationService CreateJobApplicationListingNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<JobApplicationListingViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<JobApplicationListingViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>);
    }

    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        var navigationService = new CompositeNavigationService(
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
            CreateVacancyListingNavigationService(serviceProvider),
            CreateJobApplicationListingNavigationService(serviceProvider));
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}