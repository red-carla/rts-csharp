using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public IEnumerable<Application> GetAllApplications()
    {
        return _applicationRepository.GetAll();
    }

    public Application GetApplicationById(int id)
    {
        return _applicationRepository.GetById(id);
    }

    public void CreateApplication(Application application)
    {
        _applicationRepository.Add(application);
    }

    public void UpdateApplication(Application application)
    {
        _applicationRepository.Update(application);
    }

    public void DeleteApplication(int id)
    {
        _applicationRepository.Delete(id);
    }
}