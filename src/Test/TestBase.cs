using Microsoft.Extensions.DependencyInjection;
using Persistent;
using Xunit;

namespace Test
{
    public class TestBase : IClassFixture<Fixture>
    {
        protected ServiceProvider Services; // IOC container to get the required service
        protected BaseCommandContext CommandContext { get; private set; } // Unit of work
        protected TestBase(Fixture fixture)
        {
            Services = fixture.ServiceProvider;
            CommandContext = Services.GetService<BaseCommandContext>();
            if (CommandContext != null && CommandContext.Database.EnsureCreated())
                CommandContext.SeedSampleData();
        }
        public static void Destroy(BaseCommandContext financeCommandContext)
        {
            financeCommandContext.Database.EnsureDeleted();

            financeCommandContext.Dispose();
        }
    }
}
