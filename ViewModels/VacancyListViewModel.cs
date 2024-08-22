using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services.Interfaces;
using RTS.Views;

namespace RTS.ViewModels;

public class VacancyListViewModel : ViewModelBase
{
    public ICommand OpenDetailCommand { get; private set; }
    public ICommand ApplyFilterCommand { get; private set; }
    private Vacancy _selectedVacancy;
    public Vacancy SelectedVacancy
    {
        get => _selectedVacancy;
        set
        {
            _selectedVacancy = value;
            OnPropertyChanged(nameof(SelectedVacancy));
        }
    }
    
    private readonly IDataService<Vacancy> _vacancyDataService;
    public ObservableCollection<Vacancy> Vacancies { get; private set; }
    public ICollectionView VacanciesView { get; }

    private string _filter1 = string.Empty;
    public string Filter1
    {
        get => _filter1;
        set
        {
            _filter1 = value;
            OnPropertyChanged(nameof(Filter1));
            VacanciesView.Refresh();
        }
    }

    public VacancyListViewModel(IDataService<Vacancy> vacancyDataService)
    {
        _vacancyDataService = vacancyDataService;
        Vacancies = new ObservableCollection<Vacancy>();
        VacanciesView = CollectionViewSource.GetDefaultView(Vacancies);
        VacanciesView.Filter = FilterVacancies;
        OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
        LoadVacancies();
    }
    private bool OpenDetailCanExecute()
    {
        return SelectedVacancy != null;
    }

    private void OpenDetailExecute()
    {
        var detailViewModel = new VacancyDetailViewModel(_vacancyDataService);
        detailViewModel.LoadVacancyDetails(SelectedVacancy.Id);

        VacancyDetailView detailView = new VacancyDetailView
        {
            DataContext = detailViewModel
        };
        detailView.Show();
    }
    private async void LoadVacancies()
    {
        var vacancies = await _vacancyDataService.GetAll();
        foreach (var vacancy in vacancies)
        {
            Vacancies.Add(vacancy);
        }
    }

    private bool FilterVacancies(object item)
    {
        if (item is Vacancy vacancy)
        {
            return vacancy.JobTitle.Contains(Filter1, StringComparison.InvariantCultureIgnoreCase);
    
        }
        return false;
    }

}