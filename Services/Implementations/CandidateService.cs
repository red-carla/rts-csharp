using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;

    public CandidateService(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public IEnumerable<Candidate> GetAllCandidates()
    {
        return _candidateRepository.GetAll();
    }

    public Candidate GetCandidateById(int id)
    {
        return _candidateRepository.GetById(id);
    }

    public void CreateCandidate(Candidate candidate)
    {
        _candidateRepository.Add(candidate);
    }

    public void UpdateCandidate(Candidate candidate)
    {
        _candidateRepository.Update(candidate);
    }

    public void DeleteCandidate(int id)
    {
        _candidateRepository.Delete(id);
    }
}