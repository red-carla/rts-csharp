using Microsoft.EntityFrameworkCore;
using RTS.Models;

namespace RTS.EntityFramework;

public class RecruitmentDbContext :DbContext
{
    public DbSet<ApplicationStage> ApplicationStages { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Recruiter> Recruiters { get; set; }
    
    public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : base(options)
    {
    }
    
    
}