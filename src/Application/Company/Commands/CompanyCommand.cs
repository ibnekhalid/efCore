using Application.Company.Commands.ViewModel;
using Application.Company.Repository;
using System;
using System.Threading.Tasks;

namespace Application.Company.Commands
{
    public class CompanyCommandService : ICompanyCommandService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyCommandService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<int> Create(CreateCompanyVm vm)
        {
            if (await _companyRepository.IsNameDuplicate(vm.Name)) throw new Exception($"'{vm.Name}' already exists. Please choose a different name.");
            var company = new Core.Model.Company(vm.Name);
            _companyRepository.Add(company);
            await _companyRepository.SaveChanges();
            return company.Id;
        }

        public async Task<int> Update(UpdateCompanyVm vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(vm.Id) ?? throw new Exception($"No Company found against id:'{vm.Id}'");

            if (await _companyRepository.IsNameDuplicate(vm.Id, vm.Name)) throw new Exception($"'{vm.Name}' already exists. Please choose a different name.");

            company.Update(vm.Name);
            await _companyRepository.SaveChanges();
            return company.Id;
        }
        public async Task<int> Inactivate(int id)
        {
            var company = await _companyRepository.GetFirstOrDefault(id) ?? throw new Exception($"No Company found against id:'{id}'");

            company.Inactivate();
            await _companyRepository.SaveChanges();
            return company.Id;
        }
        public async Task<int> Activate(int id)
        {
            var company = await _companyRepository.GetFirstOrDefault(id) ?? throw new Exception($"No Company found against id:'{id}'");

            company.Activate();
            await _companyRepository.SaveChanges();
            return company.Id;
        }
    }

}
