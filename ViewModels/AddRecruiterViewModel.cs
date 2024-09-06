using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class AddRecruiterViewModel : ViewModelBase
{
    private readonly IDataService<Recruiter> _recruiterDataService;

    private string _name = null!;
    private string _email = null!;
    private string _password = null!;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
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
    

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public AddRecruiterViewModel(IDataService<Recruiter> recruiterDataService, INavigationService closeNavigationService)
    {
        _recruiterDataService = recruiterDataService;
        SubmitCommand = new AddRecruiterCommand(this, closeNavigationService);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }

    public async Task AddRecruiter(Recruiter recruiter)
    {
        await _recruiterDataService.Create(recruiter);
      

    }

}
