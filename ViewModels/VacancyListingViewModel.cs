using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class VacancyListingViewModel : ViewModelBase
{
    
    private readonly IDataService<Vacancy> _vacancyDataService;
    public ObservableCollection<Vacancy> Vacancies { get; private set; }
    
    public ICommand AddVacancyCommand { get; }

    public VacancyListingViewModel(INavigationService addVacancyNavigationService, IDataService<Vacancy> vacancyDataService
        )
    {
       
        _vacancyDataService = vacancyDataService;
       Vacancies = new ObservableCollection<Vacancy>();

        AddVacancyCommand = new NavigateCommand(addVacancyNavigationService);
        

        LoadVacancies();

    }
    private async void LoadVacancies()
    {
        var vacancies = await _vacancyDataService.GetAll();
        foreach (var vacancy in vacancies)
        {
            Vacancies.Add(vacancy);
        }
    }

    private void OnVacancyAdded(Vacancy vacancy)
    {
        Vacancies.Add(new Vacancy());
    }
}