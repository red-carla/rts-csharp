using RTS.Services;
using RTS.Stores;

namespace RTS.Commands;

public class LogoutCommand : CommandBase
{
    private readonly AccountStore _accountStore;
    
    private readonly INavigationService _navigationService; 

    public LogoutCommand(AccountStore accountStore, INavigationService navigationService)
    {
        _accountStore = accountStore;
        _navigationService = navigationService;
    }
    public override void Execute(object? parameter)
    {
        _accountStore.CurrentAccount = null;
        _navigationService.Navigate();
    }
}