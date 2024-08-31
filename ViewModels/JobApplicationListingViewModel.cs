using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Views;

namespace RTS.ViewModels;

public class JobApplicationListingViewModel : ViewModelBase
{
    private readonly IDataService<ApplicationStage> _applicationStageDataService;
    private readonly IDataService<Candidate> _candidateDataService;

    private readonly IDataService<JobApplication> _jobApplicationDataService;
    private readonly INavigationService _navigationService;
    private readonly IDataService<Vacancy> _vacancyDataService;
    private JobApplication _selectedJobApplication;


    public JobApplicationListingViewModel(IDataService<JobApplication> jobApplicationDataService,
        IDataService<Candidate> candidateDataService,
        IDataService<Vacancy> vacancyDataService,
        IDataService<ApplicationStage> applicationStageDataService,
        INavigationService addJobApplicationNavigationService)
    {
        _jobApplicationDataService = jobApplicationDataService;
        _candidateDataService = candidateDataService;
        _vacancyDataService = vacancyDataService;
        _applicationStageDataService = applicationStageDataService;
        _navigationService = addJobApplicationNavigationService;
        JobApplications = new ObservableCollection<JobApplication>();

        OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
        AddJobApplicationCommand = new NavigateCommand(addJobApplicationNavigationService);

        LoadJobApplications();
    }

    public ICommand OpenDetailCommand { get; private set; }

    public JobApplication SelectedJobApplication
    {
        get => _selectedJobApplication;
        set
        {
            _selectedJobApplication = value;
            OnPropertyChanged(nameof(SelectedJobApplication));
        }
    }

    public ObservableCollection<JobApplication> JobApplications { get; }

    public ICommand AddJobApplicationCommand { get; }

    private bool OpenDetailCanExecute()
    {
        return true;
    }

    private async void OpenDetailExecute()
    {
        var detailViewModel = new JobApplicationDetailViewModel(_jobApplicationDataService, _candidateDataService,
            _vacancyDataService, _applicationStageDataService, _navigationService, SelectedJobApplication);
        await detailViewModel.Initialize();

        var detailView = new JobApplicationDetailView
        {
            DataContext = detailViewModel
        };
        detailView.Show();
    }

    private async void LoadJobApplications()
    {
        var jobApplications =
            await _jobApplicationDataService.GetAll("Vacancy, Candidate, ApplicationStage");
        foreach (var jobApplication in jobApplications) JobApplications.Add(jobApplication);
    }

    private async void OnJobApplicationAdded(JobApplication jobApplication)
    {
        await _jobApplicationDataService.Create(jobApplication);
        JobApplications.Add(jobApplication);
    }
}