using System.ComponentModel.DataAnnotations;

namespace RTS.Models;

public class Recruiter : BaseEntity
{
    [StringLength(100)] public string? Email { get; set; }

    [Required] [StringLength(255)] public string PasswordHash { get; set; } = null!;
    
    [StringLength(255)] public string? Name { get; set; }

    [StringLength(255)] public string? Avatar { get; set; }

    public int? RandomId { get; set; }
    
   //  public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>(); 
     
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}