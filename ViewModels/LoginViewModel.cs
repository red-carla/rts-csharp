using System.Windows.Input;
using RTS.Commands;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _email = null!;

    private string _password = null!;

    public LoginViewModel(AccountStore accountStore, INavigationService loginNavigationService)
    {
        LoginCommand = new LoginCommand(this, accountStore, loginNavigationService);
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand { get; }
}