using Application.Company.Commands;
using Core.Mananger.DBContext;
using Xunit;
using Xunit.Priority;
using Microsoft.Extensions.DependencyInjection;
using Application.Company.Commands.ViewModel;
using System.Linq;
using FluentAssertions;

namespace Test.Company
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class CompanyCommandTest : TestBase
    {
        private readonly int _companyId = SeedData.CompanyId;
        private readonly ICompanyCommandService _commandService;
        private readonly IBaseCommandContext _context;
        public CompanyCommandTest(Fixture fixture) : base(fixture)
        {
            _commandService = Services.GetService<ICompanyCommandService>();
            _context = Services.GetService<IBaseCommandContext>();
        }
        [Theory]
        [InlineData("Test Company")]
        public void Add_Business(string name)
        {
            // Arrange

            var model = new CreateCompanyVm
            {
                Name = name,
            };

            // Act
            var id = _commandService.Create(model).Result;

            var result = _context.Company.First(x => x.Id == id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(model.Name);
        }
    }
}
