using Common.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Api.Extensions;

namespace Tm.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public Type[] Types { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            Types = AssemblyTypesBuilder.GetAllExecutingContextTypes();

            _hostingEnv.SetDefault();
            var isDebug = _hostingEnv.IsDev();

            var conStr = Configuration.GetConnectionString("SqlServer");
            services.AddAppSettings(Configuration)
                .RegisterCommandQueryDbContext(conStr)
                .RegisterCommandsAndQueryServices(Types)
                .RegisterRepositories(Types);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
