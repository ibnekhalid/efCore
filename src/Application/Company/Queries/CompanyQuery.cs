using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Company.Queries
{
    public class CompanyQueryService : ICompanyQueryService
    {
        private readonly IServiceProvider _sp;
        public CompanyQueryService(IServiceProvider sp)
        {
            _sp = sp;
        }
        public async Task<List<Core.Model.Company>> Get()
        {
            Console.WriteLine("---------------------------------");
            using IServiceScope serviceScope = _sp.CreateScope();
            var context = (BaseQueryContext)serviceScope.ServiceProvider.GetService<BaseQueryContext>();
            Console.Write(context);
            Console.WriteLine("---------------------------------");
            //context.Database.EnsureCreated();
            return await context.Company.ToListAsync();

        }
        public Task<Core.Model.Company> Get(int id)
        {
            using var serviceScope = _sp.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BaseQueryContext>();
            context.Database.EnsureCreated();
            return context.Company.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public Task<Core.Model.Company> GetByUser(int userId)
        {
            using var serviceScope = _sp.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BaseQueryContext>();
            context.Database.EnsureCreated();
            return context.Company.Include(x => x.Users).FirstOrDefaultAsync(x => x.Users.Any(u => u.Id.Equals(userId)));
        }

    }
}
