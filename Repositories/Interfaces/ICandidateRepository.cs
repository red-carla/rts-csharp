namespace DefaultNamespace;
using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public interface ICandidateRepository
{
    IEnumerable<Candidate> GetAll();
    Candidate GetById(int id);
    void Add(Candidate candidate);
    void Update(Candidate candidate);
    void Delete(int id);
}