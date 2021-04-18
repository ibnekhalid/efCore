using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Application.Company.Repository
{
    public interface ICompanyRepository:IRepository
    {
        Task<List<Core.Model.Company>> GetAll();
        Task<Core.Model.Company> GetFirstOrDefault(int id);
        Task<bool> IsNameDuplicate(string name);
        Task<bool> IsNameDuplicate(int id, string name);
        void Add(Core.Model.Company company);
    }
}
