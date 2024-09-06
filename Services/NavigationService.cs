using RTS.Stores;
using RTS.ViewModels;

namespace RTS.Services;

public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly Func<TViewModel> _createViewModel;
    private readonly NavigationStore _navigationStore;
    private readonly AccountStore _accountStore;
    private readonly Func<ViewModelBase> _createLoginViewModel;
    

    public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel, AccountStore accountStore,
        Func<ViewModelBase> createLoginViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
        _accountStore = accountStore;
        _createLoginViewModel = createLoginViewModel;
    }

    public void Navigate()
    {
        if (_accountStore.IsLoggedIn)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
        else
        {
            _navigationStore.CurrentViewModel = _createLoginViewModel();
        }
    }
}