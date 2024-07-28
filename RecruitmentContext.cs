using Microsoft.EntityFrameworkCore;
using RecruitmentApp.Models;
using User = RecruitmentApp.Models.User;

namespace RecruitmentApp.Data
{
    public class RecruitmentContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationStage> ApplicationStages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=recruitmentDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PKs
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Employer>().HasKey(e => e.EmployerId);
            modelBuilder.Entity<Job>().HasKey(j => j.JobId);
            modelBuilder.Entity<Candidate>().HasKey(c => c.CandidateId);
            modelBuilder.Entity<Application>().HasKey(a => a.ApplicationId);
            modelBuilder.Entity<ApplicationStage>().HasKey(s => s.StageId);

//TODO: FKs etc...
        }
    }
}


