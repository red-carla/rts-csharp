public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public IEnumerable<Job> GetAllJobs()
    {
        return _jobRepository.GetAll();
    }

    public Job GetJobById(int id)
    {
        return _jobRepository.GetById(id);
    }

    public void CreateJob(Job job)
    {
        _jobRepository.Add(job);
    }

    public void UpdateJob(Job job)
    {
        _jobRepository.Update(job);
    }

    public void DeleteJob(int id)
    {
        _jobRepository.Delete(id);
    }
}