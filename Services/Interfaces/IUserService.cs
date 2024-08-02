using RecruitmentApp.Models;

namespace RecruitmentApp;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
    User Authenticate(string email, string password);
}