using Application.Company.Commands.ViewModel;
using Common;
using System.Threading.Tasks;

namespace Application.Company.Commands
{
    public interface ICompanyCommandService : ICommandService
    {
        Task<int> Create(CreateCompanyVm vm);
        Task<int> Update(UpdateCompanyVm vm);
        Task<int> Activate(int id);
        Task<int> Inactivate(int id);
    }

}
