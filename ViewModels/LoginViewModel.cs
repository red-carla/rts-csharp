using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _email = null!;

    private string _password = null!;
    private readonly IDataService<Recruiter> _recruiterDataService;

    public LoginViewModel(AccountStore accountStore, 
        INavigationService loginNavigationService, IDataService<Recruiter> recruiterDataService, INavigationService addRecruiterNavigationService)
    {
        _recruiterDataService = recruiterDataService;
        LoginCommand = new LoginCommand(this, accountStore, loginNavigationService, recruiterDataService);
        AddRecruiterCommand = new NavigateCommand(addRecruiterNavigationService);
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
    public ICommand AddRecruiterCommand { get; }

    public async void OnRecruiterAdded(Recruiter recruiter)
    {
        await _recruiterDataService.Create(recruiter);
        
    }


}