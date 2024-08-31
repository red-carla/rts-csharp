/*
using System.ComponentModel.DataAnnotations;

namespace RTS.Models;


public class Employer : BaseEntity
{

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(255)]
    public string? Location { get; set; }

    [MaxLength(100)]
    public string? ContactInfo { get; set; }

    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();

    public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
}
*/

