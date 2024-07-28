using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class UserViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private ObservableCollection<User> _users;
    private User _selectedUser;

    public UserViewModel(IUserService userService)
    {
        _userService = userService;
        LoadUsers();
        SaveCommand = new RelayCommand(SaveUser);
        DeleteCommand = new RelayCommand(DeleteUser);
    }

    public ObservableCollection<User> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged();
        }
    }

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public void LoadUsers()
    {
        Users = new ObservableCollection<User>(_userService.GetAllUsers());
    }

    public void SaveUser()
    {
        if (SelectedUser.UserId == 0)
        {
            _userService.CreateUser(SelectedUser);
        }
        else
        {
            _userService.UpdateUser(SelectedUser);
        }
        LoadUsers();
    }

    public void DeleteUser()
    {
        _userService.DeleteUser(SelectedUser.UserId);
        LoadUsers();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}