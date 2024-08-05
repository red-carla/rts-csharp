﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    public int? EmployerId { get; set; }

    [ForeignKey("Recruiter")] public int? RecruiterId { get; set; }

    public virtual Employer Employer { get; set; } = null!;

    public virtual Recruiter? Recruiter { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}