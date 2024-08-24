using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels
{
    public class CandidateDetailViewModel : ViewModelBase
    {
        private readonly IDataService<Candidate> _candidateDataService;
        private Candidate _candidate;
        private bool _isEditing;
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
        public CandidateDetailViewModel(IDataService<Candidate> dataService)
        {
           
            _candidateDataService = dataService;
            EditCommand = new RelayCommand(() => IsEditing = true);
            SaveCommand = new RelayCommand(Save);


        }
        public async Task LoadCandidateDetails(int candidateId)
        {
            Candidate = await _candidateDataService.GetById(candidateId);
            OnPropertyChanged(nameof(Candidate));
        }
        private async void Save()
        {
            await _candidateDataService.Update(Candidate.Id, Candidate);
            IsEditing = false;
        }
    
    }
}
