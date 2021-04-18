using Microsoft.Extensions.Hosting;
using System;

namespace Common.Environment
{
    public static class EnvironmentExtensions
    {
        public static bool IsProd(this IHostEnvironment hosting) =>
            hosting?.EnvironmentName == "prod";
        public static bool IsQa(this IHostEnvironment hosting) =>
            hosting?.EnvironmentName == "qa";
        public static bool IsTest(this IHostEnvironment hosting) =>
            hosting?.EnvironmentName == "test";
        public static bool IsDev(this IHostEnvironment hosting) =>
            hosting?.EnvironmentName == "Development";

        public static void Set(this IHostEnvironment hosting, string env)
        {
            hosting.EnvironmentName = env;
            ApplicationEnvironment.SetAppEnvironment(env);
        }
        public static void SetDefault(this IHostEnvironment hosting)
        {
            if (hosting.EnvironmentName?.Length == 0)
                throw new Exception("Environment variable not defined");
            ApplicationEnvironment.SetAppEnvironment(hosting);
        }
    }
}
