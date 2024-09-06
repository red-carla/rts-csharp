using System.ComponentModel;
using RTS.Models;
using RTS.Services;
using RTS.ViewModels;

namespace RTS.Commands;

public class AddRecruiterCommand : CommandBase
{
    private readonly AddRecruiterViewModel _addRecruiterViewModel;
    private readonly INavigationService _navigationService;

    public AddRecruiterCommand(AddRecruiterViewModel addRecruiterViewModel, INavigationService navigationService)
    {
        _addRecruiterViewModel = addRecruiterViewModel;
        _navigationService = navigationService;

        _addRecruiterViewModel.PropertyChanged += AddRecruiterViewModel_PropertyChanged;
    }

    public override async void Execute(object? parameter)
    {
        try
        {
            string recruiterName = _addRecruiterViewModel.Name;
            string recruiterEmail = _addRecruiterViewModel.Email;
            string recruiterPassword = _addRecruiterViewModel.Password;
            string hashedPassword = PasswordHasher.GenerateAndReturnHash(recruiterPassword);


            var recruiter = new Recruiter
            {
                Name = recruiterName,
                Email = recruiterEmail,
                PasswordHash = hashedPassword,
            };


            await _addRecruiterViewModel.AddRecruiter(recruiter);

            _navigationService.Navigate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void AddRecruiterViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnCanExecuteChanged();
    }
}