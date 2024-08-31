using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels;

public class AddVacancyViewModel : ViewModelBase
{
    private readonly IDataService<Vacancy> _vacancyDataService;
    private DateTime _datePosted = DateTime.Now;

    private string _description = null!;

    private string _eduReq = null!;
    private string _employmentType = null!;
    private string _experienceReq = null!;

    private string _jobTitle = null!;
    private string _location = null!;

    private string _status = null!;

    public AddVacancyViewModel(IDataService<Vacancy> vacancyDataService, INavigationService closeNavigationService)
    {
        _vacancyDataService = vacancyDataService;
        SubmitCommand = new AddVacancyCommand(this, closeNavigationService);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }

    public string JobTitle
    {
        get => _jobTitle;
        set
        {
            _jobTitle = value;
            OnPropertyChanged(nameof(JobTitle));
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public string EduReq
    {
        get => _eduReq;
        set
        {
            _eduReq = value;
            OnPropertyChanged(nameof(EduReq));
        }
    }

    public DateTime DatePosted
    {
        get => _datePosted;
        set
        {
            _datePosted = value;
            OnPropertyChanged(nameof(DatePosted));
        }
    }

    public string ExperienceReq
    {
        get => _experienceReq;
        set
        {
            _experienceReq = value;
            OnPropertyChanged(nameof(ExperienceReq));
        }
    }

    public string Location
    {
        get => _location;
        set
        {
            _location = value;
            OnPropertyChanged(nameof(Location));
        }
    }

    public string EmploymentType
    {
        get => _employmentType;
        set
        {
            _employmentType = value;
            OnPropertyChanged(nameof(EmploymentType));
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public async Task AddVacancy(Vacancy vacancy)
    {
        await _vacancyDataService.Create(vacancy);
    }
}