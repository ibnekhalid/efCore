﻿using Core.Mananger.DBContext;
using Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Core
{
    public sealed class BaseQueryContext : IdentityDbContext<User>, IBaseQueryContext
    {

        public BaseQueryContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var asm = assemblies.FirstOrDefault(x => x.FullName.Contains("Persistent"));
            var assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(asm);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.EnableSensitiveDataLogging();
        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
    }
}
