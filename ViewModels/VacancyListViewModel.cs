using System.Collections.ObjectModel;
using RTS.Models;
using RTS.Services.Interfaces;

namespace RTS.ViewModels;

public class VacancyListViewModel : ViewModelBase
{
    private readonly IDataService<Vacancy> _vacancyDataService;
    public ObservableCollection<Vacancy> Vacancies { get; private set; }
    public VacancyListViewModel(IDataService<Vacancy> vacancyDataService)
    {
        _vacancyDataService = vacancyDataService;
        Vacancies = new ObservableCollection<Vacancy>();
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
}