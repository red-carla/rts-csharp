﻿using System.Windows.Input;
using RTS.State.Navigators;
using RTS.ViewModels;

namespace RTS.Commands;

public class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler CanExecuteChanged;
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter is ViewType)
        {
            ViewType viewType = (ViewType)parameter;

            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
}