using System.Collections.ObjectModel;
using RTS.Models;
using RTS.Services.Interfaces;

namespace RTS.ViewModels;

public class VacancyDetailViewModel : ViewModelBase

{
    private readonly IDataService<Vacancy> _dataService;
    private Vacancy _vacancy;

    public Vacancy Vacancy
    {
        get => _vacancy;
        set
        {
            _vacancy = value;
            OnPropertyChanged(nameof(Vacancy));
        }
    }

    public VacancyDetailViewModel(IDataService<Vacancy> dataService)
    {
        _dataService = dataService;
    }

    public async Task LoadVacancyDetails(int vacancyId)
    {
        Vacancy = await _dataService.GetById(vacancyId);
        OnPropertyChanged(nameof(Vacancy));
    }
}