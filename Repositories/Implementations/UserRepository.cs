using DefaultNamespace;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public class UserRepository : IUserRepository
{
    private readonly RecruitmentContext _context;

    public UserRepository(RecruitmentContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        try
        {
            return _context.Users.ToList();
        }
        catch (Exception ex)
        {
            // Log the exception (e.g., to a file, console, etc.)
            Console.WriteLine(ex.Message);
            throw; // Re-throw the exception to be handled elsewhere if needed
        }
       
    }


    public User GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}