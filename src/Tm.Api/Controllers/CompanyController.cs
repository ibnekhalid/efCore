using Application.Company.Commands;
using Application.Company.Commands.ViewModel;
using Application.Company.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tm.Api.Controllers
{
    [ApiController]
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyCommandService _command;
        private readonly ICompanyQueryService _query;
        public CompanyController(ICompanyCommandService command, ICompanyQueryService query)
        {
            _command = command;
            _query = query;
        }
        [HttpPost("create")]
        public async Task<int> Create(CreateCompanyVm vm)
        {
            return await _command.Create(vm);
        }
        [HttpGet("all")]
        public async Task<List<CompanyVm>> GetAll()
        {
            var result = await _query.Get();
            return CompanyVm.Map(result);
        }
    }
}
