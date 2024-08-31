using System.Windows.Input;
using RTS.Commands;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class NavigationBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public NavigationBarViewModel(AccountStore accountStore,
        INavigationService homeNavigationService,
        INavigationService accountNavigationService,
        INavigationService loginNavigationService,
        INavigationService candidateListingNavigationService,
        INavigationService vacancyListingNavigationService,
        INavigationService jobApplicationListingNavigationService)
    {
        _accountStore = accountStore;
        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
        NavigateAccountCommand = new NavigateCommand(accountNavigationService);
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        NavigateCandidateListingCommand = new NavigateCommand(candidateListingNavigationService);
        NavigateVacancyListingCommand = new NavigateCommand(vacancyListingNavigationService);
        NavigateJobApplicationListingCommand = new NavigateCommand(jobApplicationListingNavigationService);
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