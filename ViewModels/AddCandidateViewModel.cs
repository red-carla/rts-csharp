﻿using RTS.Commands;
using RTS.Services;
using RTS.Stores;
using System.Windows.Input;
using RTS.Models;

namespace RTS.ViewModels
{
    public class AddCandidateViewModel : ViewModelBase
    {
        
        private readonly IDataService<Candidate> _candidateDataService;
        
        private string _name = null!;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _title = null!;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _avatar = null!;

        public string Avatar
        {
            get { return _avatar; }
            set
            {
                _avatar = value;
                OnPropertyChanged(nameof(Avatar));
            }
        }

        private string _resumeLink = null!;

        public string ResumeLink
        {
            get { return _resumeLink; }
            set
            {
                _resumeLink = value;
                OnPropertyChanged(nameof(ResumeLink));
            }
        }

        private string _email = null!;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        private string _phone = null!;

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _address = null!;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _status = null!;

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCandidateViewModel(IDataService<Candidate> candidateDataService, INavigationService closeNavigationService)
        {
            _candidateDataService = candidateDataService;
            SubmitCommand = new AddCandidateCommand(this, closeNavigationService);
            CancelCommand = new NavigateCommand(closeNavigationService);
        }
        
        public async Task AddCandidate(Candidate candidate)
        {
            await _candidateDataService.Create(candidate);
            
        }
       
    }
}