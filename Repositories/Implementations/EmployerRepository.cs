using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public class EmployerRepository : IEmployerRepository
{
    private readonly RecruitmentContext _context;

    public EmployerRepository(RecruitmentContext context)
    {
        _context = context;
    }

    public IEnumerable<Employer> GetAll()
    {
        return _context.Employers.ToList();
    }

    public Employer GetById(int id)
    {
        return _context.Employers.Find(id);
    }

    public void Add(Employer employer)
    {
        _context.Employers.Add(employer);
        _context.SaveChanges();
    }

    public void Update(Employer employer)
    {
        _context.Employers.Update(employer);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var employer = _context.Employers.Find(id);
        if (employer != null)
        {
            _context.Employers.Remove(employer);
            _context.SaveChanges();
        }
    }
}