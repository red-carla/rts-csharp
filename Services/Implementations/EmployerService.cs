public class EmployerService : IEmployerService
{
    private readonly IEmployerRepository _employerRepository;

    public EmployerService(IEmployerRepository employerRepository)
    {
        _employerRepository = employerRepository;
    }

    public IEnumerable<Employer> GetAllEmployers()
    {
        return _employerRepository.GetAll();
    }

    public Employer GetEmployerById(int id)
    {
        return _employerRepository.GetById(id);
    }

    public void CreateEmployer(Employer employer)
    {
        _employerRepository.Add(employer);
    }

    public void UpdateEmployer(Employer employer)
    {
        _employerRepository.Update(employer);
    }

    public void DeleteEmployer(int id)
    {
        _employerRepository.Delete(id);
    }
}