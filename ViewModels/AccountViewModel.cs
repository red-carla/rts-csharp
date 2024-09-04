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
    private readonly IDataService<Vacancy> _vacancyDataService;
    public ICollectionView DashboardVacanciesView { get; }

    public ObservableCollection<Vacancy> DashboardVacancies { get; }

    private readonly IDataService<Candidate> _candidateDataService;
    public ICollectionView DashboardCandidatesView { get; }

    public ObservableCollection<Candidate> DashboardCandidates { get; }

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService,
        IDataService<Vacancy> vacancyDataService, IDataService<Candidate> candidateDataService)
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

        LoadDashboardVacancies();
        LoadDashboardCandidates();
    }


    public string? Email => _accountStore.CurrentAccount?.Email;
    public string? Name => _accountStore.CurrentAccount?.Name;

    public ICommand NavigateHomeCommand { get; }

    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Name));
    }

    private async void LoadDashboardVacancies()
    {
        var vacancies = await _vacancyDataService.GetAll();

        // Example: Show only the top 5 most recent vacancies or apply your own logic
        foreach (var vacancy in vacancies.Take(5))
        {
            DashboardVacancies.Add(vacancy);
        }
    }

    private async void LoadDashboardCandidates()
    {
        var candidates = await _candidateDataService.GetAll();

        // Example: Show only the top 5 most recent candidates or apply your own logic
        foreach (var candidate in candidates.Take(5))
        {
            DashboardCandidates.Add(candidate);
        }
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}