using System.ComponentModel.DataAnnotations;
namespace RTS.Models;

public class Candidate : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string? Title { get; set; }

    [StringLength(255)]
    public string? Avatar { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(255)]
    public string? ResumeLink { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
