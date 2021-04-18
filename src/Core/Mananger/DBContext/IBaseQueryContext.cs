using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.Mananger.DBContext
{
    public interface IBaseQueryContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
    }
}
