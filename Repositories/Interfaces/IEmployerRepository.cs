namespace DefaultNamespace;
using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public interface IEmployerRepository
{
    IEnumerable<Employer> GetAll();
    Employer GetById(int id);
    void Add(Employer employer);
    void Update(Employer employer);
    void Delete(int id);
}