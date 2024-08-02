using RecruitmentApp.Models;

public interface IJobService
{
    IEnumerable<Job> GetAllJobs();
    Job GetJobById(int id);
    void CreateJob(Job job);
    void UpdateJob(Job job);
    void DeleteJob(int id);
}