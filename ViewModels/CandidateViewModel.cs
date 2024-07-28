using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class CandidateViewModel : INotifyPropertyChanged
{
    private readonly ICandidateService _candidateService;
    private ObservableCollection<Candidate> _candidates;
    private Candidate _selectedCandidate;

    public CandidateViewModel(ICandidateService candidateService)
    {
        _candidateService = candidateService;
        LoadCandidates();
        SaveCommand = new RelayCommand(SaveCandidate);
        DeleteCommand = new RelayCommand(DeleteCandidate);
    }

    public ObservableCollection<Candidate> Candidates
    {
        get => _candidates;
        set
        {
            _candidates = value;
            OnPropertyChanged();
        }
    }

    public Candidate SelectedCandidate
    {
        get => _selectedCandidate;
        set
        {
            _selectedCandidate = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public void LoadCandidates()
    {
        Candidates = new ObservableCollection<Candidate>(_candidateService.GetAllCandidates());
    }

    public void SaveCandidate()
    {
        if (SelectedCandidate.CandidateId == 0)
        {
            _candidateService.CreateCandidate(SelectedCandidate);
        }
        else
        {
            _candidateService.UpdateCandidate(SelectedCandidate);
        }
        LoadCandidates();
    }

    public void DeleteCandidate()
    {
        _candidateService.DeleteCandidate(SelectedCandidate.CandidateId);
        LoadCandidates();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}