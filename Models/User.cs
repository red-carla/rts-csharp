using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bit IsAdmin { get; set; }
        public string Avatar { get; set; }
        public int EmployerId { get; set; }
        public ICollection<Employer> Employers { get; set; }
    }
}
