using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Views;

namespace RTS.ViewModels;

public class CandidateListingViewModel : ViewModelBase
{
    private readonly IDataService<Candidate> _candidateDataService;
    private Candidate _selectedCandidate;


    public CandidateListingViewModel(INavigationService addCandidateNavigationService,
        IDataService<Candidate> candidateDataService)
    {
        _candidateDataService = candidateDataService;
        Candidates = new ObservableCollection<Candidate>();
        OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
        AddCandidateCommand = new NavigateCommand(addCandidateNavigationService);
      
        LoadCandidates();
    }

    public ICommand OpenDetailCommand { get; private set; }

    public Candidate SelectedCandidate
    {
        get => _selectedCandidate;
        set
        {
            _selectedCandidate = value;
            OnPropertyChanged(nameof(SelectedCandidate));
        }
    }

    public ObservableCollection<Candidate> Candidates { get; }

    public ICommand AddCandidateCommand { get; }

    private bool OpenDetailCanExecute()
    {
        return SelectedCandidate != null;
    }

    private async void OpenDetailExecute()
    {
        var detailViewModel = new CandidateDetailViewModel(_candidateDataService);
        await detailViewModel.LoadCandidateDetails(SelectedCandidate.Id);
        /*await detailViewModel.LoadJobApplications(SelectedCandidate.Id);*/

        var detailView = new CandidateDetailView
        {
            DataContext = detailViewModel
        };
        detailView.Show();
    }

    private async void LoadCandidates()
    {
        var candidates = await _candidateDataService.GetAll();
        foreach (var candidate in candidates) Candidates.Add(candidate);
    }

    private async void OnCandidateAdded(Candidate candidate)
    {
        await _candidateDataService.Create(candidate);
        Candidates.Add(candidate);
    }
}