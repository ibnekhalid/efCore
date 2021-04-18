using Microsoft.Extensions.Hosting;
using Serilog;

namespace Tm.Api.Extensions
{
    public static class SerilogServiceExtension
    {
        public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration);
            });
            return builder;
        }
    }
}
