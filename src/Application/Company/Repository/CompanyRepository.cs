using Core.Mananger.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Company.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IBaseCommandContext _commandContext;
        private readonly DbSet<Core.Model.Company> _companies;
        public CompanyRepository(IBaseCommandContext commandContext)
        {
            _commandContext = commandContext;
            _companies = commandContext.Company;
        }
        public Task<List<Core.Model.Company>> GetAll()
            => _companies.ToListAsync();
        public Task<Core.Model.Company> GetFirstOrDefault(int id)
            => _companies.FirstOrDefaultAsync(x => x.Id.Equals(id));
        public Task<bool> IsNameDuplicate(string name)
            => _companies.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        public Task<bool> IsNameDuplicate(int id, string name)
             => _companies.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()) && !x.Id.Equals(id));
        public void Add(Core.Model.Company company)
            => _companies.Add(company);

        public Task<int> SaveChanges(CancellationToken cancellationToken = default)
             => _commandContext.SaveChangesAsync(cancellationToken);


    }
}
