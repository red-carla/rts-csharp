using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services.Interfaces;
using RTS.Views;

namespace RTS.ViewModels;

public class CandidateListViewModel : ViewModelBase
{ public ICommand OpenDetailCommand { get; private set; }
    private Candidate _selectedCandidate;
    public Candidate SelectedCandidate
    {
        get => _selectedCandidate;
        set
        {
            _selectedCandidate = value;
            OnPropertyChanged(nameof(SelectedCandidate));
        }
    }
    
    private readonly IDataService<Candidate> _candidateDataService;
    public ObservableCollection<Candidate> Candidates { get; private set; }

    public CandidateListViewModel(IDataService<Candidate> candidateDataService)
    {
        _candidateDataService = candidateDataService;
        Candidates = new ObservableCollection<Candidate>();
        OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
        LoadCandidates();
    }
    private bool OpenDetailCanExecute()
    {
        return SelectedCandidate != null;
    }

    private void OpenDetailExecute()
    {
        var detailViewModel = new CandidateDetailViewModel(_candidateDataService);
        detailViewModel.LoadCandidateDetails(SelectedCandidate.Id);

        CandidateDetailView detailView = new CandidateDetailView
        {
            DataContext = detailViewModel
        };
        detailView.Show();
    }
    private async void LoadCandidates()
    {
        var candidates = await _candidateDataService.GetAll();
        foreach (var candidate in candidates)
        {
            Candidates.Add(candidate);
        }
    }
}