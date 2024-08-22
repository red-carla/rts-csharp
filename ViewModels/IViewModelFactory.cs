using RTS.State.Navigators;

namespace RTS.ViewModels;

public interface IViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}