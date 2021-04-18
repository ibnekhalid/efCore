using Microsoft.Extensions.Hosting;

namespace Common.Environment
{
    public static class ApplicationEnvironment
    {
        public static bool IsProd { get; private set; }
        public static bool IsQa { get; private set; }
        public static bool IsDev { get; private set; }
        public static bool IsTest { get; private set; }
        public static string Name { get; private set; }
        public static void SetAppEnvironment(IHostEnvironment hostEnvironment)
        {
            Name = hostEnvironment?.EnvironmentName;
            IsProd = hostEnvironment.IsProd();
            IsQa = hostEnvironment.IsQa();
            IsDev = hostEnvironment.IsDev();
            IsTest = hostEnvironment.IsTest();
        }
        public static void SetAppEnvironment(string env)
        {
            Name = env;
            IsProd = env == "prod";
            IsQa = env == "qa";
            IsTest = env == "test";
            IsDev = env == "Development";
        }
    }
}
