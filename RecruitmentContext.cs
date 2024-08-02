using Microsoft.EntityFrameworkCore;
using RecruitmentApp.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;

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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("RecruitmentContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PKs
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Employer>().HasKey(e => e.EmployerId);
            modelBuilder.Entity<Job>().HasKey(j => j.JobId);
            modelBuilder.Entity<Candidate>().HasKey(c => c.CandidateId);
            modelBuilder.Entity<Application>().HasKey(a => a.ApplicationId);
            modelBuilder.Entity<ApplicationStage>().HasKey(s => s.ApplicationStageId);
            
            //1..*
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employer)
                .WithMany(e => e.Users)
                .HasForeignKey(u => u.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Jobs)
                .WithOne(j => j.User)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Applications)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
            
            modelBuilder.Entity<Employer>()
                .HasMany(e => e.Jobs)
                .WithOne(j => j.Employer)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Applications)
                .WithOne(a => a.Job)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1010,
                Email = "test@example.com",
                Password = "testpassword",
                EmployerId = 1,
                Avatar = "https://robohash.org/magnicorporispariatur.png?size=50x50&set=set1",
                FirstName = "test",
                LastName = "test",
                Role = "test",
                IsAdmin = true
            }
        );

            modelBuilder.Entity<Employer>().HasData(
                new Employer
                {
                    EmployerId = 10,
                    Name = "Test Employer",
                    ContactInfo = "test@example.com",
                    Location = "Singapore"
                }
            );
        }

    }
}


