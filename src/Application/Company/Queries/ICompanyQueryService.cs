using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Company.Queries
{
    public interface ICompanyQueryService: IQueryService
    {
        Task<List<Core.Model.Company>> Get();
        Task<Core.Model.Company> Get(int id);
        Task<Core.Model.Company> GetByUser(int userId);
    }
}
