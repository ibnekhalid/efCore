using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistent;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.Destructurers;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.MsSqlServer.Destructurers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Api.Extensions;

namespace Tm.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers()
                        .WithDestructurers(
                            new List<IExceptionDestructurer>
                            {
                                new SqlExceptionDestructurer(),
                                new DbUpdateExceptionDestructurer()
                            }))
                .CreateLogger();
            try
            {
                Log.Information("------------[ Application Starting Up ]------------");
                var host = CreateHostBuilder(args).Build();

                // Apply migration if staging or Prod
                ApplyMigrations(host.Services).Wait();

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "------------[ Application Failed to start ]------------");
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                    throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
            .UseSerilogLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
        private static async Task ApplyMigrations(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var services = serviceScope.ServiceProvider;
            var env = services.GetRequiredService<IWebHostEnvironment>();
            if (!(env.IsStaging() || env.IsProduction())) return;
            var logger = services.GetService<ILogger<BaseContext>>();
            logger.LogInformation($@"------------[ Applying Migration on Environment: '{env.EnvironmentName}' ]-----------");
            var context = services.GetRequiredService<BaseContext>();
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
                await context.Database.MigrateAsync();
        }
    }
}
