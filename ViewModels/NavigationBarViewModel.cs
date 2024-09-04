using System.Windows.Input;
using RTS.Commands;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class NavigationBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    private string _currentPage;

    public NavigationBarViewModel(AccountStore accountStore,
        INavigationService homeNavigationService,
        INavigationService accountNavigationService,
        INavigationService loginNavigationService,
        INavigationService candidateListingNavigationService,
        INavigationService vacancyListingNavigationService,
        INavigationService jobApplicationListingNavigationService)
    {
        _accountStore = accountStore;
        NavigateHomeCommand = new NavigateCommand(homeNavigationService, () => CurrentPage = "Home");
        NavigateAccountCommand = new NavigateCommand(accountNavigationService, () => CurrentPage = "Account");
        NavigateLoginCommand = new NavigateCommand(loginNavigationService, () => CurrentPage = "Login");
        NavigateCandidateListingCommand = new NavigateCommand(candidateListingNavigationService, () => CurrentPage = "Candidates");
        NavigateVacancyListingCommand = new NavigateCommand(vacancyListingNavigationService, () => CurrentPage = "Vacancies");
        NavigateJobApplicationListingCommand = new NavigateCommand(jobApplicationListingNavigationService, () => CurrentPage = "Job Applications");
        LogoutCommand = new LogoutCommand(_accountStore);

        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
    }

    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateAccountCommand { get; }
    public ICommand NavigateLoginCommand { get; }
    public ICommand NavigateCandidateListingCommand { get; }
    public ICommand NavigateVacancyListingCommand { get; }
    public ICommand NavigateJobApplicationListingCommand { get; }
    public ICommand LogoutCommand { get; }

    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    
    public string CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}