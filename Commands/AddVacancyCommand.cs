using System.ComponentModel;
using RTS.Models;
using RTS.Services;
using RTS.ViewModels;

namespace RTS.Commands;

public class AddVacancyCommand : CommandBase
{
    private readonly AddVacancyViewModel _addVacancyViewModel;
    private readonly INavigationService _navigationService;

    public AddVacancyCommand(AddVacancyViewModel addVacancyViewModel,
        INavigationService navigationService)
    {
        _addVacancyViewModel = addVacancyViewModel;
        _navigationService = navigationService;

        _addVacancyViewModel.PropertyChanged += AddVacancyViewModel_PropertyChanged;
    }

    public override async void Execute(object? parameter)
    {
        try
        {
            var vacancy = new Vacancy
            {
                JobTitle = _addVacancyViewModel.JobTitle,
                Description = _addVacancyViewModel.Description,
                Status = _addVacancyViewModel.Status,
                EducationReq = _addVacancyViewModel.EduReq,
                ExperienceReq = _addVacancyViewModel.ExperienceReq,
                DatePosted = _addVacancyViewModel.DatePosted,
                Location = _addVacancyViewModel.Location,

                EmploymentType = _addVacancyViewModel.EmploymentType
            };
            await _addVacancyViewModel.AddVacancy(vacancy);

            _navigationService.Navigate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);
        }
    }


    private void AddVacancyViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnCanExecuteChanged();
    }
}