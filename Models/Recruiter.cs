namespace RTS.Models;

public class Recruiter : DomainObject
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; } 
    public string PasswordHash { get; set; }
    public List<Vacancy> Vacancies { get; set; }
}