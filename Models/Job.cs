using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string JobStatus { get; set; }
        public DateTime DatePosted { get; set; }
        public string EducationReq { get; set; }
        public string ExperienceReq { get; set; }
        public string JobLocation { get; set; }
        public string EmploymentType { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
