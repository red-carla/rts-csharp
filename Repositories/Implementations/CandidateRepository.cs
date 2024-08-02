using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public class CandidateRepository : ICandidateRepository
{
    private readonly RecruitmentContext _context;

    public CandidateRepository(RecruitmentContext context)
    {
        _context = context;
    }

    public IEnumerable<Candidate> GetAll()
    {
        return _context.Candidates.ToList();
    }

    public Candidate GetById(int id)
    {
        return _context.Candidates.Find(id);
    }

    public void Add(Candidate candidate)
    {
        _context.Candidates.Add(candidate);
        _context.SaveChanges();
    }

    public void Update(Candidate candidate)
    {
        _context.Candidates.Update(candidate);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var candidate = _context.Candidates.Find(id);
        if (candidate != null)
        {
            _context.Candidates.Remove(candidate);
            _context.SaveChanges();
        }
    }
}