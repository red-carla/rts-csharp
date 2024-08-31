using RTS.Stores;

namespace RTS.Commands;

public class LogoutCommand : CommandBase
{
    private readonly AccountStore _accountStore;

    public LogoutCommand(AccountStore accountStore)
    {
        _accountStore = accountStore;
    }

    public override void Execute(object? parameter)
    {
        _accountStore.Logout();
    }
}