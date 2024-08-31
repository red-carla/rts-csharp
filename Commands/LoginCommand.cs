using RTS.Models;
using RTS.Services;
using RTS.Stores;
using RTS.ViewModels;

namespace RTS.Commands;

public class LoginCommand : CommandBase
{
    private readonly AccountStore _accountStore;
    private readonly LoginViewModel _loginViewModel;
    private readonly INavigationService _navigationService;

    public LoginCommand(LoginViewModel loginViewModel, AccountStore accountStore, INavigationService navigationService)
    {
        _loginViewModel = loginViewModel;
        _accountStore = accountStore;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        var account = new Recruiter
        {
            Email = _loginViewModel.Email
        };

        _accountStore.CurrentAccount = account;

        _navigationService.Navigate();
    }
}