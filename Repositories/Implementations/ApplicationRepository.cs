using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public class ApplicationRepository : IApplicationRepository
{
    private readonly RecruitmentContext _context;

    public ApplicationRepository(RecruitmentContext context)
    {
        _context = context;
    }

    public IEnumerable<Application> GetAll()
    {
        return _context.Applications.ToList();
    }

    public Application GetById(int id)
    {
        return _context.Applications.Find(id);
    }

    public void Add(Application application)
    {
        _context.Applications.Add(application);
        _context.SaveChanges();
    }

    public void Update(Application application)
    {
        _context.Applications.Update(application);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var application = _context.Applications.Find(id);
        if (application != null)
        {
            _context.Applications.Remove(application);
            _context.SaveChanges();
        }
    }
}