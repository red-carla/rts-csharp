using System.Windows.Input;
using RTS.Commands;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels;

public class AddVacancyViewModel : ViewModelBase
{
    private string _jobTitle;

    public string JobTitle
    {
        get { return _jobTitle; }
        set
        {
            _jobTitle = value;
            OnPropertyChanged(nameof(JobTitle));
        }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    private string _status;

    public string Status
    {
        get { return _status; }
        set
        {
            _status = value;
            OnPropertyChanged(nameof(Status));
        }
    }
    private string _eduReq;

    public string EduReq
    {
        get { return _eduReq; }
        set
        {
            _eduReq = value;
            OnPropertyChanged(nameof(EduReq));
        }
    }
    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public AddVacancyViewModel(VacancyStore vacancyStore, INavigationService closeNavigationService)
    {
        SubmitCommand = new AddVacancyCommand(this, vacancyStore, closeNavigationService);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }
}
