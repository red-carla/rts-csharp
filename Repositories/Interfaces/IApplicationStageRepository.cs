namespace DefaultNamespace;

public interface IApplicationStageRepository
{
    IEnumerable<ApplicationStage> GetAll();
    ApplicationStage GetById(int id);
    void Add(ApplicationStage applicationStage);
    void Update(ApplicationStage applicationStage);
    void Delete(int id);
}