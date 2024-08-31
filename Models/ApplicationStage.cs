using System.ComponentModel.DataAnnotations;

namespace RTS.Models;

public class ApplicationStage : BaseEntity
{
    [Required] [MaxLength(50)] public string ApplicationStageName { get; set; } = null!;

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}