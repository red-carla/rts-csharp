namespace RTS.Models;

public class Vacancy : DomainObject
{
    public string JobTitle { get; }
    public string Description { get; }
    public string EducationRequirements { get; }
    public string ExperienceRequirements { get; }
    public string Location { get; }
    public string EmploymentType { get; }
    public string VacancyStatus { get; }
    private readonly ICollection<JobApplication> _applications;
}