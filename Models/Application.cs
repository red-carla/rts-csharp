using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int ApplicationStageId { get; set; }
        public ApplicationStage Stage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
