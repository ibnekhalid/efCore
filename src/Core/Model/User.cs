using Common;
using System.Collections.Generic;

namespace Core.Model
{
    public class User : Entity<int>
    {
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string username { get; set; }
        public State Status { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
