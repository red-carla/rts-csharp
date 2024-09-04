using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Views;
using Wpf.Ui.Input;

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
    private string _locationFilter = string.Empty;
    public string LocationFilter
    {
        get => _locationFilter;
        set
        {
            _locationFilter = value;
            OnPropertyChanged(nameof(LocationFilter));
            VacanciesView.Refresh();
        }
    }
    private string _contractFilter = string.Empty;
    public string ContractFilter
    {
        get => _contractFilter;
        set
        {
            _contractFilter = value;
            OnPropertyChanged(nameof(ContractFilter));
            VacanciesView.Refresh();
        }
    }

    public ICommand SortCommand { get; private set; }
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
        SortCommand = new RelayCommand<string>(SortVacancies);
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
    
    
    private bool FilterVacancies(object item)
    {
        if (item is not Vacancy vacancy) return false;

        bool matchesName = string.IsNullOrEmpty(NameFilter) || vacancy.JobTitle.Contains(NameFilter, StringComparison.OrdinalIgnoreCase);
        bool matchesLocation = string.IsNullOrEmpty(LocationFilter) || vacancy.Location.Contains(LocationFilter, StringComparison.OrdinalIgnoreCase);
        bool matchesContract = string.IsNullOrEmpty(ContractFilter) || vacancy.EmploymentType.Contains(ContractFilter, StringComparison.OrdinalIgnoreCase);
        return matchesName && matchesLocation && matchesContract;
    }
    private void ApplyFilter()
    {
        VacanciesView.Refresh();
    }

    // Method to sort vacancies
    private void SortVacancies(string sortBy)
    {
        VacanciesView.SortDescriptions.Clear();
        VacanciesView.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Ascending));
    }
        /*if (item is Vacancy vacancy)
        {
            return vacancy.JobTitle.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase);

        }
        return false;
    }*/
}

