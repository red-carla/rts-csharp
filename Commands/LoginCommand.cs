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
    
    private readonly IDataService<Recruiter> _recruiterDataService;

    public LoginCommand(LoginViewModel loginViewModel, AccountStore accountStore, INavigationService navigationService, IDataService<Recruiter> recruiterDataService)
    {
        _loginViewModel = loginViewModel;
        _accountStore = accountStore;
        _navigationService = navigationService;
        _recruiterDataService = recruiterDataService;
    }

    public override async void Execute(object? parameter)
    {
        var recruiter = await _recruiterDataService.GetByEmail(_loginViewModel.Email);

        if (recruiter == null || !PasswordHasher.VerifyHash(_loginViewModel.Password, recruiter.PasswordHash))
        {
           //Invalid login, show error
            return;
        }

        // Successful login, store recruiter information
        _accountStore.CurrentAccount = recruiter;
        _navigationService.Navigate();
    }
    /*public override void Execute(object? parameter)
    {
        var account = new Recruiter
        {
            Email = _loginViewModel.Email
        };

        _accountStore.CurrentAccount = account;

        _navigationService.Navigate();
    }*/
    private bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Here, use a hashing algorithm like SHA256 to compute the hash of the entered password
        // This is a simplified example. Replace ComputeHash with actual hash checking logic.
        var enteredHash = ComputeHash(enteredPassword);  // Placeholder function for actual hashing logic
        return enteredHash == storedHash;
    }
    private string ComputeHash(string input)
    {
        // Implement a hashing function here to hash the password (e.g., SHA256)
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}