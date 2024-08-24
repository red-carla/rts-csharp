using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;
using System.Windows.Input;

namespace RTS.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        
        public string? Email => _accountStore.CurrentAccount?.Email;

        public ICommand NavigateHomeCommand { get; }

        public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
        {
            _accountStore = accountStore;

            NavigateHomeCommand = new NavigateCommand(homeNavigationService);

            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Email));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

            base.Dispose();
        }
    }
}
