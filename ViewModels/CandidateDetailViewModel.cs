using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels;

public class CandidateDetailViewModel : ViewModelBase
{
    private readonly IDataService<Candidate> _candidateDataService;
     private Candidate _candidate;
        private bool _isEditing;
        
     /*  private readonly IDataService<JobApplication> _jobApplicationDataService;
    public ObservableCollection<JobApplication> JobApplications { get; private set; }*/

    public CandidateDetailViewModel(IDataService<Candidate> candidateDataService)
    {
        _candidateDataService = candidateDataService;
        /*_jobApplicationDataService = jobApplicationDataService;
        JobApplications = new ObservableCollection<JobApplication>();*/
        EditCommand = new RelayCommand(ToggleEdit, () => true);
        SaveCommand = new RelayCommand(Save, () => IsEditing);
        DeleteCommand = new RelayCommand(Delete, () => true);
    }

    public Candidate Candidate
    {
        get => _candidate;
        set
        {
            _candidate = value;
            OnPropertyChanged(nameof(Candidate));
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

    public async Task LoadCandidateDetails(int candidateId)
    {
        Candidate = await _candidateDataService.GetById(candidateId);
        
          OnPropertyChanged(nameof(Candidate));
    }
    
    /*public async Task LoadJobApplications(int candidateId)
    {
        var jobApplications = await _jobApplicationDataService.GetByCondition(j => j.CandidateId == candidateId);
        foreach (var jobApplication in jobApplications) 
            JobApplications.Add(jobApplication);
    }*/
    

    private void ToggleEdit()
    {
        IsEditing = !IsEditing;
        CommandManager.InvalidateRequerySuggested();
    }

    private async void Save()
    {
        await _candidateDataService.Update(Candidate.Id, Candidate);
        IsEditing = false;
    }

    private async void Delete()
    {
        await _candidateDataService.Delete(Candidate.Id);
    }
}