using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class EmployerViewModel : INotifyPropertyChanged
{
    private readonly IEmployerService _employerService;
    private ObservableCollection<Employer> _employers;
    private Employer _selectedEmployer;

    public EmployerViewModel(IEmployerService employerService)
    {
        _employerService = employerService;
        LoadEmployers();
        SaveCommand = new RelayCommand(SaveEmployer);
        DeleteCommand = new RelayCommand(DeleteEmployer);
    }

    public ObservableCollection<Employer> Employers
    {
        get => _employers;
        set
        {
            _employers = value;
            OnPropertyChanged();
        }
    }

    public Employer SelectedEmployer
    {
        get => _selectedEmployer;
        set
        {
            _selectedEmployer = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public void LoadEmployers()
    {
        Employers = new ObservableCollection<Employer>(_employerService.GetAllEmployers());
    }

    public void SaveEmployer()
    {
        if (SelectedEmployer.EmployerId == 0)
        {
            _employerService.CreateEmployer(SelectedEmployer);
        }
        else
        {
            _employerService.UpdateEmployer(SelectedEmployer);
        }
        LoadEmployers();
    }

    public void DeleteEmployer()
    {
        _employerService.DeleteEmployer(SelectedEmployer.EmployerId);
        LoadEmployers();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}