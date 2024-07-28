namespace DefaultNamespace;

public interface IApplicationRepository
{
    IEnumerable<Application> GetAll();
    Application GetById(int id);
    void Add(Application application);
    void Update(Application application);
    void Delete(int id);
}