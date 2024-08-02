using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public class ApplicationStageService : IApplicationStageService
{
    private readonly IApplicationStageRepository _applicationStageRepository;

    public ApplicationStageService(IApplicationStageRepository applicationStageRepository)
    {
        _applicationStageRepository = applicationStageRepository;
    }

    public IEnumerable<ApplicationStage> GetAllApplicationStages()
    {
        return _applicationStageRepository.GetAll();
    }

    public ApplicationStage GetApplicationStageById(int id)
    {
        return _applicationStageRepository.GetById(id);
    }

    public void CreateApplicationStage(ApplicationStage applicationStage)
    {
        _applicationStageRepository.Add(applicationStage);
    }

    public void UpdateApplicationStage(ApplicationStage applicationStage)
    {
        _applicationStageRepository.Update(applicationStage);
    }

    public void DeleteApplicationStage(int id)
    {
        _applicationStageRepository.Delete(id);
    }
}