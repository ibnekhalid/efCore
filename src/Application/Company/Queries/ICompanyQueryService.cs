using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Company.Queries
{
    public interface ICompanyQueryService: IQueryService
    {
        Task<List<Core.Model.Company>> Get();
        Task<Core.Model.Company> Get(string id);
        Task<Core.Model.Company> GetByUser(string userId);
    }
}
