using RTS.Commands;
using RTS.Services;
using RTS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Models;

namespace RTS.ViewModels
{
    public class CandidateListingViewModel : ViewModelBase
    {
       
        private readonly IDataService<Candidate> _candidateDataService;
        public ObservableCollection<Candidate> Candidates{ get; private set; }

        public ICommand AddCandidateCommand { get; }
        

        public CandidateListingViewModel(INavigationService addCandidateNavigationService, IDataService<Candidate> candidateDataService)
        {
            
            _candidateDataService = candidateDataService;
            Candidates = new ObservableCollection<Candidate>();
            
            AddCandidateCommand = new NavigateCommand(addCandidateNavigationService);
         //   _candidateDataService.EntityCreated += OnCandidateAdded; this throws an Explicit id error so figure out another way to refresh the list after create
            
            LoadCandidates();
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
