using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;
namespace RTS.ViewModels;

public class AccountViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public string WelcomeMessage => $"Welcome, {_accountStore.CurrentAccount?.Name ?? "User"}";

    private readonly IDataService<Vacancy> _vacancyDataService;
    public ICollectionView DashboardVacanciesView { get; }

    public ObservableCollection<Vacancy> DashboardVacancies { get; }

    private readonly IDataService<Candidate> _candidateDataService;
    public ICollectionView DashboardCandidatesView { get; }

    public ObservableCollection<Candidate> DashboardCandidates { get; }

    private readonly IDataService<JobApplication> _jobApplicationDataService;
    public ICollectionView DashboardApplicationsView { get; }

    public ObservableCollection<JobApplication> DashboardApplications { get; }
    
    

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService,
        IDataService<Vacancy> vacancyDataService, IDataService<Candidate> candidateDataService,
        IDataService<JobApplication> jobApplicationDataService)
    {
       
        _accountStore = accountStore;

        NavigateHomeCommand = new NavigateCommand(homeNavigationService);

        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

        _vacancyDataService = vacancyDataService;

        DashboardVacancies = new ObservableCollection<Vacancy>();
        DashboardVacanciesView = CollectionViewSource.GetDefaultView(DashboardVacancies);

        _candidateDataService = candidateDataService;

        DashboardCandidates = new ObservableCollection<Candidate>();
        DashboardCandidatesView = CollectionViewSource.GetDefaultView(DashboardCandidates);

        _jobApplicationDataService = jobApplicationDataService;

        DashboardApplications = new ObservableCollection<JobApplication>();
        DashboardApplicationsView = CollectionViewSource.GetDefaultView(DashboardApplications);

        LoadDashboardVacancies();
        LoadDashboardCandidates();
        LoadDashboardApplications();
    }

    public string? Email => _accountStore.CurrentAccount?.Email;
    public string? Name => _accountStore.CurrentAccount?.Name;
    public string? Avatar => _accountStore.CurrentAccount?.Avatar;
    
    public ICommand NavigateHomeCommand { get; }

    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Avatar));
    }

    private async void LoadDashboardVacancies()
    {
        var vacancies = await _vacancyDataService.GetAll();


        foreach (var vacancy in vacancies.Take(6))
        {
            DashboardVacancies.Add(vacancy);
        }
    }

    private async void LoadDashboardCandidates()
    {
        var candidates = (await _candidateDataService.GetAll())
            .Where(c => c.Status == "open" || c.Status == "seeking")
            .Take(6);

        foreach (var candidate in candidates)
        {
            DashboardCandidates.Add(candidate);
        }
    }

    private async void LoadDashboardApplications()
    {
        var jobApplications =
            await _jobApplicationDataService.GetAll("Vacancy, Candidate, ApplicationStage");

        foreach (var jobApplication in jobApplications.Take(6))
        {
            DashboardApplications.Add(jobApplication);
        }
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}