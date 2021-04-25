using Application.Company.Commands.ViewModel;
using Common;
using System.Threading.Tasks;

namespace Application.Company.Commands
{
    public interface ICompanyCommandService : ICommandService
    {
        Task<string> Create(CreateCompanyVm vm);
        Task<string> Update(UpdateCompanyVm vm);
        Task<string> Activate(string id);
        Task<string> Inactivate(string id);
    }

}
