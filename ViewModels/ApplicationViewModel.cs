using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private readonly IApplicationService _applicationService;
    private ObservableCollection<Application> _applications;
    private Application _selectedApplication;

    public ApplicationViewModel(IApplicationService applicationService)
    {
        _applicationService = applicationService;
        LoadApplications();
        SaveCommand = new RelayCommand(SaveApplication);
        DeleteCommand = new RelayCommand(DeleteApplication);
    }

    public ObservableCollection<Application> Applications
    {
        get => _applications;
        set
        {
            _applications = value;
            OnPropertyChanged();
        }
    }

    public Application SelectedApplication
    {
        get => _selectedApplication;
        set
        {
            _selectedApplication = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public void LoadApplications()
    {
        Applications = new ObservableCollection<Application>(_applicationService.GetAllApplications());
    }

    public void SaveApplication()
    {
        if (SelectedApplication.ApplicationId == 0)
        {
            _applicationService.CreateApplication(SelectedApplication);
        }
        else
        {
            _applicationService.UpdateApplication(SelectedApplication);
        }
        LoadApplications();
    }

    public void DeleteApplication()
    {
        _applicationService.DeleteApplication(SelectedApplication.ApplicationId);
        LoadApplications();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
