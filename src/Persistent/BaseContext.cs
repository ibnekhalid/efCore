using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Core.Model;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Core.Mananger.DBContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistent
{
    public class BaseContext : IdentityDbContext<User,Role,string>
    {
       // protected readonly ILoggerFactory LoggerFactory;
        protected readonly IHostEnvironment Environment;
        public BaseContext(DbContextOptions<BaseContext> options, 
            IHostEnvironment environment) : base(options)
        {
            
            Environment = environment;
        }
        protected BaseContext(DbContextOptions options) : base(options) { }
        protected BaseContext(DbContextOptions options,
            IHostEnvironment environment) : base(options)
        {
           
            Environment = environment;
        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
            base.OnModelCreating(modelBuilder);
        }
    }
   
    public class BaseCommandContext : BaseContext,IBaseCommandContext
    {

        public BaseCommandContext(DbContextOptions<BaseCommandContext> options,  IHostEnvironment environment) : base(options, environment)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }


    }
}
