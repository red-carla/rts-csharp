using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels;

public class VacancyDetailViewModel : ViewModelBase
{
    private readonly IDataService<Vacancy> _vacancyDataService;
    private bool _isEditing;
    private Vacancy _vacancy;

    public VacancyDetailViewModel(IDataService<Vacancy> dataService)
    {
        _vacancyDataService = dataService;
        EditCommand = new RelayCommand(ToggleEdit, () => true);
        SaveCommand = new RelayCommand(Save, () => IsEditing);
        DeleteCommand = new RelayCommand(Delete, () => true);
    }

    public Vacancy Vacancy
    {
        get => _vacancy;
        set
        {
            _vacancy = value;
            OnPropertyChanged(nameof(Vacancy));
        }
    }

    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged(nameof(IsEditing));
        }
    }

    public ICommand EditCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public async Task LoadVacancyDetails(int VacancyId)
    {
        Vacancy = await _vacancyDataService.GetById(VacancyId);
        OnPropertyChanged(nameof(Vacancy));
    }

    private void ToggleEdit()
    {
        IsEditing = !IsEditing;
        CommandManager.InvalidateRequerySuggested();
    }

    private async void Save()
    {
        await _vacancyDataService.Update(Vacancy.Id, Vacancy);
        IsEditing = false;
    }

    private async void Delete()
    {
        await _vacancyDataService.Delete(Vacancy.Id);
    }
}