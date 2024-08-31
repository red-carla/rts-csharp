using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Views;

namespace RTS.ViewModels;

public class VacancyListingViewModel : ViewModelBase
{
    private readonly IDataService<Vacancy> _vacancyDataService;
    private Vacancy _selectedVacancy;
    public ICollectionView VacanciesView { get; }

    private string _nameFilter = string.Empty;
    public string NameFilter
    {
        get => _nameFilter;
        set
        {
            _nameFilter = value;
            OnPropertyChanged(nameof(NameFilter));
            VacanciesView.Refresh();
        }
    }
    public ICommand ApplyFilterCommand { get; private set; }
    public VacancyListingViewModel(INavigationService addVacancyNavigationService,
        IDataService<Vacancy> vacancyDataService)
    {
        _vacancyDataService = vacancyDataService;
        Vacancies = new ObservableCollection<Vacancy>();
        VacanciesView = CollectionViewSource.GetDefaultView(Vacancies);
        VacanciesView.Filter = FilterVacancies;
        OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
        AddVacancyCommand = new NavigateCommand(addVacancyNavigationService);
        ApplyFilterCommand = new RelayCommand(() => VacanciesView.Refresh());
      
        LoadVacancies();
    }

    public ICommand OpenDetailCommand { get; private set; }

    public Vacancy SelectedVacancy
    {
        get => _selectedVacancy;
        set
        {
            _selectedVacancy = value;
            OnPropertyChanged(nameof(SelectedVacancy));
        }
    }

    public ObservableCollection<Vacancy> Vacancies { get; }

    public ICommand AddVacancyCommand { get; }

    private bool OpenDetailCanExecute()
    {
        return SelectedVacancy != null;
    }

    private async void OpenDetailExecute()
    {
        var detailViewModel = new VacancyDetailViewModel(_vacancyDataService);
        await detailViewModel.LoadVacancyDetails(SelectedVacancy.Id);

        var detailView = new VacancyDetailView
        {
            DataContext = detailViewModel
        };
        detailView.Show();
    }

    private async void LoadVacancies()
    {
        var vacancies = await _vacancyDataService.GetAll();
        foreach (var vacancy in vacancies) Vacancies.Add(vacancy);
    }

    private async void OnVacancyAdded(Vacancy vacancy)
    {
        await _vacancyDataService.Create(vacancy);
        Vacancies.Add(vacancy);
    }
    private bool FilterVacancies(object item)
    {
        if (item is Vacancy vacancy)
        {
            return vacancy.JobTitle.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase);

        }
        return false;
    }
}