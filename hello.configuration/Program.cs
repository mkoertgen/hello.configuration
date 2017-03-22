using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace hello.configuration
{
    internal static class Program
    {
        // cf.: https://blog.jsinh.in/asp-net-5-configuration-microsoft-framework-configurationmodel/
        private static void Main(string[] args)
        {
            var initialConfig = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key2", "value2"}
            };

            var config = GetConfig(args, initialConfig);

            Console.WriteLine("Configuration:");
            foreach (var kvp in config.AsEnumerable())
                Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
        }

        private static IConfiguration GetConfig(string[] args, IDictionary<string,string> initialConfig)
        {
            var builder = new ConfigurationBuilder();

            builder
                .AddInMemoryCollection(initialConfig)
                .AddXmlFile("settings.xml")
                .AddJsonFile("settings.json")
                .AddEnvironmentVariables("prefix_")
                .AddCommandLine(args);

            var config = builder.Build();

            return config;
        }
    }
}
