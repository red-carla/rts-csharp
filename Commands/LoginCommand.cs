﻿using RTS.Models;
using RTS.Services;
using RTS.Stores;
using RTS.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RTS.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly AccountStore _accountStore;
        private readonly INavigationService _navigationService;

        public LoginCommand(LoginViewModel viewModel, AccountStore accountStore, INavigationService navigationService)
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            Recruiter account = new Recruiter()
            {
                Email = $"{_viewModel.Username}@test.com",
            };

            _accountStore.CurrentAccount = account;

            _navigationService.Navigate();
        }
    }
}
