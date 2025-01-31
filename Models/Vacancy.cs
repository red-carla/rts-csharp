﻿using System.ComponentModel.DataAnnotations;

namespace RTS.Models;

public class Vacancy : BaseEntity
{
    [MaxLength(100)] public string? JobTitle { get; set; }

    [MaxLength(500)] public string? Description { get; set; }

    [MaxLength(50)] public string? Status { get; set; }

    public DateTime? DatePosted { get; set; }

    [MaxLength(255)] public string? EducationReq { get; set; }

    [MaxLength(255)] public string? ExperienceReq { get; set; }

    [MaxLength(255)] public string? Location { get; set; }

    [MaxLength(100)] public string? EmploymentType { get; set; }

    public int? RandomId { get; set; }

    public int? RecruiterId { get; set; }

    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}