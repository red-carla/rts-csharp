using RTS.Services;
using RTS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using RTS.Models;

namespace RTS.Commands
{
    public class AddCandidateCommand : CommandBase
    {
        private readonly AddCandidateViewModel _addCandidateViewModel;
        private readonly INavigationService _navigationService;

        public AddCandidateCommand(AddCandidateViewModel addCandidateViewModel,  INavigationService navigationService)
        {
            _addCandidateViewModel = addCandidateViewModel;
            _navigationService = navigationService;
            
            _addCandidateViewModel.PropertyChanged += AddCandidateViewModel_PropertyChanged;
        }
        public override async void Execute(object? parameter)
        {
             Candidate candidate = new Candidate()
                       {
                           Name = _addCandidateViewModel.Name,
                           Title = _addCandidateViewModel.Title,
                           Avatar = _addCandidateViewModel.Avatar,
                           ResumeLink = _addCandidateViewModel.ResumeLink,
                           Email = _addCandidateViewModel.Email,
                           Phone = _addCandidateViewModel.Phone,
                           Status = _addCandidateViewModel.Status,
                           
                       };
                     await  _addCandidateViewModel.AddCandidate(candidate);
                       
                       _navigationService.Navigate(); 
              
        }

        private void AddCandidateViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
    }
}