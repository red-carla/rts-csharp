using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels;

public class AddJobApplicationViewModel : ViewModelBase
{
    private readonly IDataService<ApplicationStage> _applicationStageDataService;
    private readonly IDataService<Candidate> _candidateDataService;

    private readonly IDataService<JobApplication> _jobApplicationDataService;
    private readonly IDataService<Vacancy> _vacancyDataService;

    private ObservableCollection<ApplicationStage> _applicationStages = new();

    private ObservableCollection<Candidate> _candidates = new();



    private ApplicationStage _selectedApplicationStage = null!;

    private Candidate _selectedCandidate = null!;

    private Vacancy _selectedVacancy = null!;

    private DateTime? _updatedAt;

    private ObservableCollection<Vacancy> _vacancies = new();

    public AddJobApplicationViewModel(IDataService<JobApplication> jobApplicationDataService,
        IDataService<Candidate> candidateDataService,
        IDataService<Vacancy> vacancyDataService,
        IDataService<ApplicationStage> applicationStageDataService,
        INavigationService closeNavigationService)
    {
        _jobApplicationDataService = jobApplicationDataService;
        _candidateDataService = candidateDataService;
        _vacancyDataService = vacancyDataService;
        _applicationStageDataService = applicationStageDataService;
        SubmitCommand = new AddJobApplicationCommand(this, closeNavigationService);
        CancelCommand = new NavigateCommand(closeNavigationService);

        LoadData(); //for comboboxes
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

    public ObservableCollection<Vacancy> Vacancies
    {
        get => _vacancies;
        set
        {
            _vacancies = value;
            OnPropertyChanged(nameof(Vacancies));
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

    public Candidate SelectedCandidate
    {
        get => _selectedCandidate;
        set
        {
            _selectedCandidate = value;
            OnPropertyChanged(nameof(SelectedCandidate));
        }
    }

    public Vacancy SelectedVacancy
    {
        get => _selectedVacancy;
        set
        {
            _selectedVacancy = value;
            OnPropertyChanged(nameof(SelectedVacancy));
        }
    }

    public ApplicationStage SelectedApplicationStage
    {
        get => _selectedApplicationStage;
        set
        {
            _selectedApplicationStage = value;
            OnPropertyChanged(nameof(SelectedApplicationStage));
        }
    }

    public DateTime? CreatedAt { get; }

    public DateTime? UpdatedAt
    {
        get => _updatedAt;
        set
        {
            _updatedAt = DateTime.Now;
            OnPropertyChanged(nameof(UpdatedAt));
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    private async void LoadData()
    {
        Candidates = new ObservableCollection<Candidate>(await _candidateDataService.GetAll());
        Vacancies = new ObservableCollection<Vacancy>(await _vacancyDataService.GetAll());
        ApplicationStages = new ObservableCollection<ApplicationStage>(await _applicationStageDataService.GetAll());
    }


    public async Task AddJobApplication(JobApplication jobApplication)
    {
        await _jobApplicationDataService.Create(jobApplication);
    }
}