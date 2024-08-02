using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using RecruitmentApp.Views;

namespace RecruitmentApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private readonly IUserService _userService;
        private bool _loginSuccessful;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(Login);
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public bool LoginSuccessful
        {
            get => _loginSuccessful;
            set
            {
                _loginSuccessful = value;
                OnPropertyChanged();
            }
        }

        private void Login()
        {
            var user = _userService.Authenticate(Email, Password);
            LoginSuccessful = user != null;

            if (LoginSuccessful)
            {
                // Successful login
                // Close the login window
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginWindow)
                        {
                            window.DialogResult = true;
                            window.Close();
                        }
                    }
                });
            }
            else
            {
                // Handle failed login
                // Show error message or clear fields
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
