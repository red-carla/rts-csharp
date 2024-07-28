namespace DefaultNamespace;

public interface ICandidateRepository
{
    IEnumerable<Candidate> GetAll();
    Candidate GetById(int id);
    void Add(Candidate candidate);
    void Update(Candidate candidate);
    void Delete(int id);
}