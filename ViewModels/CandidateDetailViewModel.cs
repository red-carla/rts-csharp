using System.Collections.ObjectModel;
using RTS.Models;
using RTS.Services.Interfaces;

namespace RTS.ViewModels;
public class CandidateDetailViewModel : ViewModelBase

    {
        private readonly IDataService<Candidate> _candidateDataService;
        private Candidate _candidate;
        public Candidate Candidate
        {
            get => _candidate;
            set
            {
                _candidate = value;
                OnPropertyChanged(nameof(Candidate));
            }
        }
        
        
        public CandidateDetailViewModel(IDataService<Candidate> dataService)
        {
            _candidateDataService = dataService;
           
        }

        public async Task LoadCandidateDetails(int candidateId)
        {
            Candidate = await _candidateDataService.GetById(candidateId);
            OnPropertyChanged(nameof(Candidate));
        }
    }


