using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels;

public class JobApplicationDetailViewModel : ViewModelBase
{
    private readonly IDataService<ApplicationStage> _applicationStageDataService;
    private readonly IDataService<Candidate> _candidateDataService;
    private readonly IDataService<JobApplication> _jobApplicationDataService;
    private readonly INavigationService _navigationService;
    private readonly IDataService<Vacancy> _vacancyDataService;

    private ObservableCollection<ApplicationStage> _applicationStages;

    private ObservableCollection<Candidate> _candidates;

    private bool _isEditing;

    private JobApplication _jobApplication;

    private ApplicationStage _selectedApplicationStage;

    private Candidate _selectedCandidate;

    private Vacancy _selectedVacancy;

    private ObservableCollection<Vacancy> _vacancies;

    public JobApplicationDetailViewModel(
        IDataService<JobApplication> jobApplicationDataService,
        IDataService<Candidate> candidateDataService,
        IDataService<Vacancy> vacancyDataService,
        IDataService<ApplicationStage> applicationStageDataService,
        INavigationService navigationService,
        JobApplication selectedJobApplication)
    {
        _applicationStageDataService = applicationStageDataService;
        _candidateDataService = candidateDataService;
        _jobApplicationDataService = jobApplicationDataService;
        _navigationService = navigationService;
        _vacancyDataService = vacancyDataService;

        JobApplication = selectedJobApplication ?? throw new ArgumentNullException(nameof(selectedJobApplication));

        CloseCommand = new RelayCommand(OnClose);
        DeleteCommand = new RelayCommand(Delete, () => true);
        EditCommand = new RelayCommand(ToggleEdit, () => true);
        SaveCommand = new RelayCommand(Save, () => IsEditing);
    }

    public JobApplication JobApplication
    {
        get => _jobApplication;
        set
        {
            _jobApplication = value;
            OnPropertyChanged(nameof(JobApplication));
            SelectedCandidate = _jobApplication.Candidate;
            SelectedVacancy = _jobApplication.Vacancy;
            SelectedApplicationStage = _jobApplication.ApplicationStage;
        }
    }

    public ObservableCollection<Candidate> Candidates
    {
        get => _candidates;
        set
        {
            _candidates = value;
            OnPropertyChanged(nameof(Candidates));
        }
    }

    public Candidate SelectedCandidate
    {
        get => _selectedCandidate;
        set
        {
            _selectedCandidate = value;
            JobApplication.Candidate = value;
            OnPropertyChanged(nameof(SelectedCandidate));
        }
    }

    public ObservableCollection<Vacancy> Vacancies
    {
        get => _vacancies;
        set
        {
            _vacancies = value;
            OnPropertyChanged(nameof(Vacancies));
        }
    }

    public Vacancy SelectedVacancy
    {
        get => _selectedVacancy;
        set
        {
            _selectedVacancy = value;
            JobApplication.Vacancy = value;
            OnPropertyChanged(nameof(SelectedVacancy));
        }
    }

    public ObservableCollection<ApplicationStage> ApplicationStages
    {
        get => _applicationStages;
        set
        {
            _applicationStages = value;
            OnPropertyChanged(nameof(ApplicationStages));
        }
    }

    public ApplicationStage SelectedApplicationStage
    {
        get => _selectedApplicationStage;
        set
        {
            _selectedApplicationStage = value;
            JobApplication.ApplicationStage = value;
            OnPropertyChanged(nameof(SelectedApplicationStage));
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

    public ICommand CloseCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand SaveCommand { get; }

    private async Task LoadJobApplicationDetails()
    {
        JobApplication = await _jobApplicationDataService.GetByIdWProps(JobApplication.Id,
            "Vacancy, Candidate, ApplicationStage");
    }

    private async Task LoadRelatedData()
    {
        ApplicationStages = new ObservableCollection<ApplicationStage>(await _applicationStageDataService.GetAll());
        Candidates = new ObservableCollection<Candidate>(await _candidateDataService.GetAll());
        Vacancies = new ObservableCollection<Vacancy>(await _vacancyDataService.GetAll());
    }

    public async Task Initialize()
    {
        await LoadJobApplicationDetails();
        await LoadRelatedData();
    }

    private void ToggleEdit()
    {
        IsEditing = !IsEditing;
        CommandManager.InvalidateRequerySuggested();
    }

    private async void Save()
    {
        await _jobApplicationDataService.Update(JobApplication.Id, JobApplication);
        IsEditing = false;
    }

    private async void Delete()
    {
        await _jobApplicationDataService.Delete(JobApplication.Id);
    }

    private void OnClose()
    {
        _navigationService.Navigate();
    }
}