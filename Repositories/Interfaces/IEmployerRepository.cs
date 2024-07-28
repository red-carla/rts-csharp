namespace DefaultNamespace;

public interface IEmployerRepository
{
    IEnumerable<Employer> GetAll();
    Employer GetById(int id);
    void Add(Employer employer);
    void Update(Employer employer);
    void Delete(int id);
}