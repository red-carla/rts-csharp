namespace DefaultNamespace;
using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

public interface IApplicationStageRepository
{
    IEnumerable<ApplicationStage> GetAll();
    ApplicationStage GetById(int id);
    void Add(ApplicationStage applicationStage);
    void Update(ApplicationStage applicationStage);
    void Delete(int id);
}