using Common;
using System.Collections.Generic;

namespace Core.Model
{
    public class Project : StringEntity
    {
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public State Status { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }

}
