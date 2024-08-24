using RTS.Commands;
using RTS.Services;
using RTS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Models;
using RTS.Views;

namespace RTS.ViewModels
{
    public class CandidateListingViewModel : ViewModelBase
    {
        public ICommand OpenDetailCommand { get; private set; }
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

        public ICommand AddCandidateCommand { get; }


        public CandidateListingViewModel(INavigationService addCandidateNavigationService,
            IDataService<Candidate> candidateDataService)
        {
            _candidateDataService = candidateDataService;
            Candidates = new ObservableCollection<Candidate>();
            OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
            AddCandidateCommand = new NavigateCommand(addCandidateNavigationService);
            //   _candidateDataService.EntityCreated += OnCandidateAdded; this throws an Explicit id error so figure out another way to refresh the list after create

            LoadCandidates();
        }
        private bool OpenDetailCanExecute()
        {
            return SelectedCandidate != null;
        }

        private async void  OpenDetailExecute()
        {
            var detailViewModel = new CandidateDetailViewModel(_candidateDataService);
            await detailViewModel.LoadCandidateDetails(SelectedCandidate.Id);

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

        private async void OnCandidateAdded(Candidate candidate)
        {
            await _candidateDataService.Create(candidate);
            Candidates.Add(candidate);
        }
    }
}