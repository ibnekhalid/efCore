using Core.Model;
using Persistent;

namespace Test
{
    public static class SeedData
    {
        public static string CompanyId;
        public static string ProjectId;
        public static string UserId;


        public static void SeedSampleData(this BaseCommandContext context)
        {
            var company = new Core.Model.Company("Test");
            CompanyId = company.Id;

            

            context.Company.Add(company);

            context.SaveChangesAsync().Wait();
        }

    }
}
