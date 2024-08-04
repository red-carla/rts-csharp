using RTS.ViewModels.Navigators;

namespace RTS.ViewModels;

public interface IViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}