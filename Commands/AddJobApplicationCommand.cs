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
            var newJobApplication = new JobApplication
            {
                CandidateId = _addJobApplicationViewModel.SelectedCandidate.Id,
                VacancyId = _addJobApplicationViewModel.SelectedVacancy.Id,
                ApplicationStageId =
                    _addJobApplicationViewModel.SelectedApplicationStage.Id,
                CreatedAt = _addJobApplicationViewModel.CreatedAt ?? DateTime.Now,
                UpdatedAt = DateTime.Now
            };


            await _addJobApplicationViewModel.AddJobApplication(newJobApplication);


            _navigationService.Navigate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding job application: {ex.Message}");
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return _addJobApplicationViewModel.SelectedCandidate != null &&
               _addJobApplicationViewModel.SelectedVacancy != null &&
               _addJobApplicationViewModel.SelectedApplicationStage != null;
    }

    private void AddJobApplicationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnCanExecuteChanged();
    }
}