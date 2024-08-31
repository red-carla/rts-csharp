using System.Windows.Input;
using RTS.Commands;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class AccountViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
    {
        _accountStore = accountStore;

        NavigateHomeCommand = new NavigateCommand(homeNavigationService);

        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
    }

    public string? Email => _accountStore.CurrentAccount?.Email;

    public ICommand NavigateHomeCommand { get; }

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