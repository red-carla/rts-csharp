﻿using System.Windows.Input;
using RTS.Commands;
using RTS.State.Navigators;

namespace RTS.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IViewModelFactory _viewModelFactory;
    private readonly INavigator _navigator;

    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

    public ICommand UpdateCurrentViewModelCommand { get; }

    public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;

        _navigator.StateChanged += Navigator_StateChanged;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
    }


    private void Navigator_StateChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
   
    public override void Dispose()
    {
        _navigator.StateChanged -= Navigator_StateChanged;

        base.Dispose();
    }
}