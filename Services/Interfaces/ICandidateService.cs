public interface ICandidateService
{
    IEnumerable<Candidate> GetAllCandidates();
    Candidate GetCandidateById(int id);
    void CreateCandidate(Candidate candidate);
    void UpdateCandidate(Candidate candidate);
    void DeleteCandidate(int id);
}