using RTS.Models;
using RTS.Services;
using RTS.Stores;
using RTS.ViewModels;

namespace RTS.Commands;

public class AddVacancyCommand : CommandBase
{
    private readonly AddVacancyViewModel _addVacancyViewModel;
    private readonly VacancyStore _vacancyStore;
    private readonly INavigationService _navigationService;

    public AddVacancyCommand(AddVacancyViewModel addVacancyViewModel, VacancyStore vacancyStore,
        INavigationService navigationService)
    {
        _addVacancyViewModel = addVacancyViewModel;
        _vacancyStore = vacancyStore;
        _navigationService = navigationService;
    }

    public override void Execute(object parameter)
    {
       Vacancy vacancy = new Vacancy()
       {
              JobTitle = _addVacancyViewModel.JobTitle,
              Description = _addVacancyViewModel.Description,
              Status = _addVacancyViewModel.Status,
         };
    
          _vacancyStore.AddVacancy(vacancy);
        
          _navigationService.Navigate();
        
    }
}