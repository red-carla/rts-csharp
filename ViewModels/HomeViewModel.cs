using System.Windows.Input;
using RTS.Commands;
using RTS.Services;

namespace RTS.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public HomeViewModel(INavigationService loginNavigationService)
    {
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
    }
    public ICommand NavigateLoginCommand { get; }
}