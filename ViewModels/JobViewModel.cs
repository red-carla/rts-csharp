using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public class JobViewModel : INotifyPropertyChanged
{
    private readonly IJobService _jobService;
    private ObservableCollection<Job> _jobs;
    private Job _selectedJob;

    public JobViewModel(IJobService jobService)
    {
        _jobService = jobService;
        LoadJobs();
        SaveCommand = new RelayCommand(SaveJob);
        DeleteCommand = new RelayCommand(DeleteJob);
    }

    public ObservableCollection<Job> Jobs
    {
        get => _jobs;
        set
        {
            _jobs = value;
            OnPropertyChanged();
        }
    }

    public Job SelectedJob
    {
        get => _selectedJob;
        set
        {
            _selectedJob = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public void LoadJobs()
    {
        Jobs = new ObservableCollection<Job>(_jobService.GetAllJobs());
    }

    public void SaveJob()
    {
        if (SelectedJob.JobId == 0)
        {
            _jobService.CreateJob(SelectedJob);
        }
        else
        {
            _jobService.UpdateJob(SelectedJob);
        }
        LoadJobs();
    }

    public void DeleteJob()
    {
        _jobService.DeleteJob(SelectedJob.JobId);
        LoadJobs();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}