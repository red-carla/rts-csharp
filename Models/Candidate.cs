﻿namespace RTS.Models;

public class Candidate : DomainObject
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public List<JobApplication> Applications { get; set; }
}