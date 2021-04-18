using Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Tm.Api.Extensions
{
    public static class CommandQueryBuilderExtension
    {
        public static IServiceCollection RegisterCommandsAndQueryServices(this IServiceCollection services, Type[] types)
        {
            
            // Register Query Service
            types
                .Where(type => typeof(IQueryService).IsAssignableFrom(type) && !type.IsInterface).ToList()
                .ForEach(type => services.AddSingleton(type.GetInterface($"I{type.Name}")!, type));

            // Register Command Service
            
            types
                .Where(type => typeof(ICommandService).IsAssignableFrom(type) && !type.IsInterface).ToList()
                .ForEach(type =>  services.AddScoped(type.GetInterface($"I{type.Name}")!, type));

            return services;
        }
    }
}
