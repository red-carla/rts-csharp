﻿using RTS.Stores;
using RTS.ViewModels;

namespace RTS.Services;

public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;
    private readonly Func<TViewModel> _createViewModel;
    private readonly NavigationStore _navigationStore;

    public LayoutNavigationService(NavigationStore navigationStore,
        Func<TViewModel> createViewModel,
        Func<NavigationBarViewModel> createNavigationBarViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
        _createNavigationBarViewModel = createNavigationBarViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = new LayoutViewModel(_createNavigationBarViewModel(), _createViewModel());
    }
}