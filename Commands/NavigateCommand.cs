using RTS.Services;

namespace RTS.Commands;

public class NavigateCommand : CommandBase
{
    private readonly INavigationService _navigationService;
    private readonly Action _postNavigationAction;
    
    public NavigateCommand(INavigationService navigationService, Action postNavigationAction = null)
    {
        _navigationService = navigationService;
        _postNavigationAction = postNavigationAction;
    }
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;
    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
        _postNavigationAction?.Invoke();
    }
}