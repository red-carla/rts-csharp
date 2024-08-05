using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RTS.Models;

namespace RTS.EntityFramework;

public class RecruitmentDbContext : DbContext
{
    public virtual DbSet<Employer> Employers { get; set; } = null!;
    public virtual DbSet<Recruiter> Recruiters { get; set; } = null!;
    public virtual DbSet<Candidate> Candidates { get; set; } = null!;
    public virtual DbSet<Vacancy> Vacancies { get; set; } = null!;
    public virtual DbSet<ApplicationStage> ApplicationStages { get; set; } = null!;
    public virtual DbSet<JobApplication> JobApplications { get; set; } = null!;
    public DbSet<SeedHistory> SeedHistory { get; set; }

    public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : base(options)
    {
   
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employer>(entity =>
        {
            entity.ToTable("Employer");

            entity.HasKey(e => e.Id)
                .HasName("PK_Employer");

            entity.Property(e => e.ContactInfo).HasMaxLength(100);

            entity.Property(e => e.Location).HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });
        modelBuilder.Entity<Recruiter>(entity =>
        {
            entity.ToTable("Recruiter");

            entity.HasKey(e => e.Id)
                .HasName("PK_RecruiterId");

            entity.Property(e => e.Avatar).HasMaxLength(255);

            entity.Property(e => e.Email).HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.HasOne(d => d.Employer)
                .WithMany(p => p.Recruiters)
                .HasForeignKey(d => d.EmployerId)
                .HasConstraintName("FK_Recruiter_Employer");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.ToTable("Candidate");

            entity.HasKey(e => e.Id)
                .HasName("PK_Candidate");

            entity.Property(e => e.Address).HasMaxLength(255);

            entity.Property(e => e.Avatar).HasMaxLength(255);

            entity.Property(e => e.Email).HasMaxLength(100);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.Property(e => e.ResumeLink).HasMaxLength(255);

            entity.Property(e => e.Status).HasMaxLength(50);

            entity.Property(e => e.Title).HasMaxLength(20);
        });

        modelBuilder.Entity<ApplicationStage>(entity =>
        {
            entity.ToTable("ApplicationStage");

            entity.HasKey(e => e.Id)
                .HasName("PK_ApplicationStage");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.ApplicationStageName)
                .IsRequired()
                .HasMaxLength(50);
        });
        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.ToTable("Vacancy");

            entity.HasKey(e => e.Id)
                .HasName("PK_Vacancy");

            entity.Property(e => e.DatePosted).HasColumnType("datetime");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.EducationReq).HasMaxLength(255);

            entity.Property(e => e.EmploymentType).HasMaxLength(100);

            entity.Property(e => e.ExperienceReq).HasMaxLength(255);

            entity.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Location).HasMaxLength(255);

            entity.Property(e => e.Status).HasMaxLength(50);


            entity.HasOne(d => d.Recruiter)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.RecruiterId)
                .HasConstraintName("FK_Vacancy_Recruiter");

            entity.HasOne(d => d.Employer)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.EmployerId)
                .HasConstraintName("FK_Vacancy_Employer");
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.ToTable("JobApplication");

            entity.HasKey(e => e.Id)
                .HasName("PK_ApplicationId");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationStage)
                .WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.ApplicationStageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Stage");

            entity.HasOne(d => d.Candidate)
                .WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Candidate");

            entity.HasOne(d => d.Vacancy)
                .WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Vacancy");

        });


       
    }
}