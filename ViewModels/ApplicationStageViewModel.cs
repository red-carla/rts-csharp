using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class ApplicationStageViewModel : INotifyPropertyChanged
{
    private readonly IApplicationStageService _applicationStageService;
    private ObservableCollection<ApplicationStage> _applicationStages;
    private ApplicationStage _selectedApplicationStage;

    public ApplicationStageViewModel(IApplicationStageService applicationStageService)
    {
        _applicationStageService = applicationStageService;
        LoadApplicationStages();
        SaveCommand = new RelayCommand(SaveApplicationStage);
        DeleteCommand = new RelayCommand(DeleteApplicationStage);
    }

    public ObservableCollection<ApplicationStage> ApplicationStages
    {
        get => _applicationStages;
        set
        {
            _applicationStages = value;
            OnPropertyChanged();
        }
    }

    public ApplicationStage SelectedApplicationStage
    {
        get => _selectedApplicationStage;
        set
        {
            _selectedApplicationStage = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public void LoadApplicationStages()
    {
        ApplicationStages = new ObservableCollection<ApplicationStage>(_applicationStageService.GetAllApplicationStages());
    }

    public void SaveApplicationStage()
    {
        if (SelectedApplicationStage.ApplicationStageId == 0)
        {
            _applicationStageService.CreateApplicationStage(SelectedApplicationStage);
        }
        else
        {
            _applicationStageService.UpdateApplicationStage(SelectedApplicationStage);
        }
        LoadApplicationStages();
    }

    public void DeleteApplicationStage()
    {
        _applicationStageService.DeleteApplicationStage(SelectedApplicationStage.ApplicationStageId);
        LoadApplicationStages();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
