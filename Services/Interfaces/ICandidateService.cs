using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public interface ICandidateService
{
    IEnumerable<Candidate> GetAllCandidates();
    Candidate GetCandidateById(int id);
    void CreateCandidate(Candidate candidate);
    void UpdateCandidate(Candidate candidate);
    void DeleteCandidate(int id);
}