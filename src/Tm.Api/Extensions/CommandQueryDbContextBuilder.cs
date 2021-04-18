using Common.Environment;
using Core;
using Core.Mananger.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistent;

namespace Tm.Api.Extensions
{
    public static class CommandQueryDbContextBuilder
    {
        public static IServiceCollection RegisterCommandQueryDbContext(this IServiceCollection services, string connectionString, bool isInMemory = false)
        {
            services.AddDbContext<BaseContext>(QueryConfigurations);
            services.AddDbContextPool<BaseQueryContext>(QueryConfigurations);
           
            #region Commands
            services.AddDbContext<BaseCommandContext>(Configurations);
            services.AddScoped<IBaseCommandContext>(provider => provider.GetService<BaseCommandContext>());
            #endregion


            void Configurations(DbContextOptionsBuilder options)
            {
                _ = isInMemory ? options.UseInMemoryDatabase("InMemoryDB") :
                options.UseSqlServer(connectionString);
                               
                if (ApplicationEnvironment.IsDev)
                    options.EnableDetailedErrors();

                if (!ApplicationEnvironment.IsProd)
                    options.EnableSensitiveDataLogging();
            }
            void QueryConfigurations(DbContextOptionsBuilder options)
            {
                Configurations(options);
                options.UseLazyLoadingProxies(false);
                options.UseChangeTrackingProxies(false);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            return services;
        }
    }
}
