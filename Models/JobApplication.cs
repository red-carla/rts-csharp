namespace RTS.Models;

public class JobApplication : DomainObject  
{
    public DateTime DateApplied { get; set; }
    public DateTime DateUpdated { get; set; }
    public Candidate Candidate { get; set; }
    public Vacancy Vacancy { get; set; }
    public ApplicationStage JobStage { get; set; }
    
}