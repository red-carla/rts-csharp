using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public class JobRepository : IJobRepository
{
    private readonly RecruitmentContext _context;

    public JobRepository(RecruitmentContext context)
    {
        _context = context;
    }

    public IEnumerable<Job> GetAll()
    {
        return _context.Jobs.ToList();
    }

    public Job GetById(int id)
    {
        return _context.Jobs.Find(id);
    }

    public void Add(Job job)
    {
        _context.Jobs.Add(job);
        _context.SaveChanges();
    }

    public void Update(Job job)
    {
        _context.Jobs.Update(job);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var job = _context.Jobs.Find(id);
        if (job != null)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
        }
    }
}