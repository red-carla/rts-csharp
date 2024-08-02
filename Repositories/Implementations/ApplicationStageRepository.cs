using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public class ApplicationStageRepository : IApplicationStageRepository
{
    private readonly RecruitmentContext _context;

    public ApplicationStageRepository(RecruitmentContext context)
    {
        _context = context;
    }

    public IEnumerable<ApplicationStage> GetAll()
    {
        return _context.ApplicationStages.ToList();
    }

    public ApplicationStage GetById(int id)
    {
        return _context.ApplicationStages.Find(id);
    }

    public void Add(ApplicationStage applicationStage)
    {
        _context.ApplicationStages.Add(applicationStage);
        _context.SaveChanges();
    }

    public void Update(ApplicationStage applicationStage)
    {
        _context.ApplicationStages.Update(applicationStage);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var applicationStage = _context.ApplicationStages.Find(id);
        if (applicationStage != null)
        {
            _context.ApplicationStages.Remove(applicationStage);
            _context.SaveChanges();
        }
    }
}