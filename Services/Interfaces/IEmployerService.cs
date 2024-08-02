using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public interface IEmployerService
{
    IEnumerable<Employer> GetAllEmployers();
    Employer GetEmployerById(int id);
    void CreateEmployer(Employer employer);
    void UpdateEmployer(Employer employer);
    void DeleteEmployer(int id);
}