using System.ComponentModel;

namespace RTS.ViewModels;

public class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public virtual void Dispose()
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}