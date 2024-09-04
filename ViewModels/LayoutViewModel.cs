namespace RTS.ViewModels;

public class LayoutViewModel : ViewModelBase
{
    private string _currentViewName;
   
    public string CurrentViewName
    {
        get => _currentViewName;
        set
        {
            _currentViewName = value;
            OnPropertyChanged(nameof(CurrentViewName.ToString));
        }
    }
     public LayoutViewModel(NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
            _currentViewName = contentViewModel.GetType().Name;
            CurrentViewName = _currentViewName;
            

        }
    public NavigationBarViewModel NavigationBarViewModel { get; }
    public ViewModelBase ContentViewModel { get; }

    public override void Dispose()
    {
        NavigationBarViewModel.Dispose();
        ContentViewModel.Dispose();

        base.Dispose();
    }
}