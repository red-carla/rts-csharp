using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;
using System.Windows.Input;

namespace RTS.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email = null!;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password = null!;
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

        public LoginViewModel(AccountStore accountStore, INavigationService loginNavigationService)
        {
            LoginCommand = new LoginCommand(this, accountStore, loginNavigationService);
        }
    }
}
