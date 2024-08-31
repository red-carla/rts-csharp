using System.ComponentModel;
using RTS.Models;
using RTS.Services;
using RTS.ViewModels;

namespace RTS.Commands;

public class AddJobApplicationCommand : CommandBase
{
    private readonly AddJobApplicationViewModel _addJobApplicationViewModel;
    private readonly INavigationService _navigationService;

    public AddJobApplicationCommand(AddJobApplicationViewModel addJobApplicationViewModel,
        INavigationService navigationService)
    {
        _addJobApplicationViewModel = addJobApplicationViewModel;
        _navigationService = navigationService;

        _addJobApplicationViewModel.PropertyChanged += AddJobApplicationViewModel_PropertyChanged;
    }

    public override async void Execute(object? parameter)
    {
        try
        {
            // Create a new JobApplication instance using the selected IDs
            var newJobApplication = new JobApplication
            {
                CandidateId = _addJobApplicationViewModel.SelectedCandidate.Id, // Use existing Candidate ID
                VacancyId = _addJobApplicationViewModel.SelectedVacancy.Id, // Use existing Vacancy ID
                ApplicationStageId =
                    _addJobApplicationViewModel.SelectedApplicationStage.Id, // Use existing ApplicationStage ID
                CreatedAt = _addJobApplicationViewModel.CreatedAt ?? DateTime.Now,
                UpdatedAt = DateTime.Now // Automatically set the current date and time
            };

            // Call the AddJobApplication method to save the new job application
            await _addJobApplicationViewModel.AddJobApplication(newJobApplication);

            // Navigate back to the previous view
            _navigationService.Navigate();
        }
        catch (Exception ex)
        {
            // Handle exceptions such as database errors, etc.
            // Example: Log the error, show a message to the user, etc.
            Console.WriteLine($"Error adding job application: {ex.Message}");
        }
    }

    public override bool CanExecute(object? parameter)
    {
        // Ensure the command is only enabled when all necessary fields are filled
        return _addJobApplicationViewModel.SelectedCandidate != null &&
               _addJobApplicationViewModel.SelectedVacancy != null &&
               _addJobApplicationViewModel.SelectedApplicationStage != null;
    }

    private void AddJobApplicationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnCanExecuteChanged();
    }
}