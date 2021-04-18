using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Common.Environment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Persistent;
using Tm.Api.Extensions;

namespace Test
{
    public class Fixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public BaseCommandContext CommandContext { get; private set; }
        public Fixture()
        {
            var path = Environment.CurrentDirectory;
            var services = new ServiceCollection();
            var types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies();

            // Todo Fix explicit adding of Qf.Persistence assembly
            //assemblies.Push(typeof(AccountsManager).Assembly);
            //assemblies.Push(typeof(AdminCommandService).Assembly);

            types.AddRange(AssemblyTypesBuilder.GetCommonExecutingContextTypes(assemblies));
            
            var allTypes = types.ToArray();
            var connectionString = Guid.NewGuid().ToString();
            // Initialized in memory database
            services.RegisterCommandQueryDbContext(connectionString, true);

            // register repositories
            services.RegisterRepositories(allTypes);

            // register services
            services.RegisterCommandsAndQueryServices(allTypes);
           
            
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton<IHostEnvironment, MockWebHostEnvironment>();
           
            // IOC container
            
            
            
            services.AddScoped<IHttpContextAccessor, MockHttpContextAccessor>();
            services.AddLogging(builder => builder.AddConsole());
            
            ServiceProvider = services.BuildServiceProvider();
            CommandContext = ServiceProvider.GetService<BaseCommandContext>();
            var hostEnvironment = ServiceProvider.GetService<IHostEnvironment>();
            hostEnvironment.SetDefault();
        }

        public class MockHttpContextAccessor : IHttpContextAccessor
        {
            private static readonly IEnumerable<Claim> _claims;

            public HttpContext HttpContext { get; set; } = new DefaultHttpContext() { User = new ClaimsPrincipal(new ClaimsIdentity(_claims, "TestAuthType")) };
        }


        public class MockWebHostEnvironment : IHostEnvironment
        {
            public string EnvironmentName { get; set; } = "test";
            public string ApplicationName { get; set; }
            public string ContentRootPath { get; set; }
            public IFileProvider ContentRootFileProvider { get; set; }
        }

    }
}
