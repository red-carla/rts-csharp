using RecruitmentApp.Models;

namespace DefaultNamespace;

public interface IJobRepository
{
    IEnumerable<Job> GetAll();
    Job GetById(int id);
    void Add(Job job);
    void Update(Job job);
    void Delete(int id);
}