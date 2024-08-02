namespace DefaultNamespace;
using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public interface IApplicationRepository
{
    IEnumerable<Application> GetAll();
    Application GetById(int id);
    void Add(Application application);
    void Update(Application application);
    void Delete(int id);
}