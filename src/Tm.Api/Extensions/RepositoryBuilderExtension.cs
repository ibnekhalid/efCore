using Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Tm.Api.Extensions
{
    public static class RepositoryBuilderExtension
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, Type[] types)
        {
            // Register Repositories
            types
                .Where(type => typeof(IRepository).IsAssignableFrom(type) && !type.IsInterface).ToList()
                .ForEach(type => services.AddScoped(type.GetInterface($"I{type.Name}")!, type));
            return services;
        }
    }
}
