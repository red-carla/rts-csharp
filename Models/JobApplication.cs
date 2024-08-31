using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTS.Models;

public class JobApplication : BaseEntity
{
    [ForeignKey("Vacancy")] public int VacancyId { get; set; }

    public virtual Vacancy Vacancy { get; set; } = null!;

    [ForeignKey("Candidate")] public int CandidateId { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    [ForeignKey("ApplicationStage")] public int ApplicationStageId { get; set; }

    public virtual ApplicationStage ApplicationStage { get; set; } = null!;

    [Required] public DateTime? CreatedAt { get; set; }

    [Required] public DateTime? UpdatedAt { get; set; }
}