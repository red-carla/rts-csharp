using RTS.Stores;
using RTS.ViewModels;

namespace RTS.Services;

public class ParameterNavigationService<TParameter, TViewModel>
    where TViewModel : ViewModelBase
{
    private readonly Func<TParameter, TViewModel> _createViewModel;
    private readonly NavigationStore _navigationStore;

    public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public void Navigate(TParameter parameter)
    {
        _navigationStore.CurrentViewModel = _createViewModel(parameter);
    }
}