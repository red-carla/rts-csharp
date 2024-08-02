using RecruitmentApp;
using RecruitmentApp.Models;
using RecruitmentApp.Data;
using DefaultNamespace;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetById(id);
    }

    public void CreateUser(User user)
    {
        _userRepository.Add(user);
    }

    public void UpdateUser(User user)
    {
        _userRepository.Update(user);
    }

    public void DeleteUser(int id)
    {
        _userRepository.Delete(id);
    }
    public User Authenticate(string email, string password)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
        return user;

       /* var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
        return user;*/
    }
}