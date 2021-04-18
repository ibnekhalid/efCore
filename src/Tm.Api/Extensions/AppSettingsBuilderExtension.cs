using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tm.Api.Extensions
{
    public static class AppSettingBuilderExtension
    {
        public static Exception AppSettingsNotFoundException => new Exception("AppSettings not found in Configuration. Maybe appsettings.json not added or section did not exist");
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);
            var settings = appSettingsSection.Get<AppSettings>();
            settings.Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower();
            return services;
        }
    }
    
}
