using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace hello.configuration
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class Configuration_Should
    {
        [Test]
        public void Support_Command_Line_Args()
        {
            var args = new[]
            {
                "CmdArgKey1=Value1", "--CmdArgKey2=Value2", "/CmdArgKey3=Value3", "-CmdArgKey4=Value4", "--CmdArgKey5",
                "Value5"
            };
            var switchMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"--CmdArgKey2", "cak2"},
                {"-CmdArgKey4", "cak4"},
            };
            var config = new ConfigurationBuilder().AddCommandLine(args, switchMappings).Build();

            config["CmdArgKey1"].Should().Be("Value1");
            //Config["CmdArgKey2"].Should().Be("Value2");
            config["cak2"].Should().Be("Value2");

            config["CmdArgKey3"].Should().Be("Value3");
            //Config["CmdArgKey4"].Should().Be("Value4");
            config["cak4"].Should().Be("Value4");
            config["CmdArgKey5"].Should().Be("Value5");
        }

        [Test]
        public void Support_Environment_Variables()
        {
            Environment.SetEnvironmentVariable("env_var_key1", "env_var_value1");
            Environment.SetEnvironmentVariable("env_var_key2", "env_var_value2");

            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            config["env_var_key1"].Should().Be("env_var_value1");
            config["env_var_key2"].Should().Be("env_var_value2");
        }

        [Test]
        public void Support_InMemory()
        {
            var initialConfig = new Dictionary<string, string>
            {
                {"mem_key1", "mem_value1"},
                {"mem_key2", "mem_value2"}
            };

            var config = new ConfigurationBuilder().AddInMemoryCollection(initialConfig).Build();
            config["mem_key1"].Should().Be("mem_value1");
            config["mem_key2"].Should().Be("mem_value2");
        }

        [Test]
        public void Support_Ini_files()
        {
            var config = new ConfigurationBuilder().AddIniFile("settings.ini").Build();
            config["ini-sec:ini-key1"].Should().Be("ini-value1");
            config["ini-sec:ini-key2"].Should().Be("ini-value2");

            var section = config.GetSection("ini-sec");
            section["ini-key1"].Should().Be("ini-value1");
            section["ini-key2"].Should().Be("ini-value2");
        }

        [Test]
        public void Support_Json_files()
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            config["Application:Mode"].Should().Be("SampleJsonValue");
            config["Application:Width"].Should().Be("42");
        }

        [Test]
        public void Support_Xml_files()
        {
            var config = new ConfigurationBuilder().AddXmlFile("settings.xml").Build();
            config["from"].Should().Be("Jsinh");
            config["body:subject"].Should().Be("Hello world");
            config["body2:subject"].Should().Be("Hello Universe");
        }

        [Test]
        public void Override_keys_case_insensitive()
        {
            var first = new Dictionary<string, string> {{"key", "foo"}};
            var second = new Dictionary<string, string> {{"KeY", "bar"}};
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(first)
                .AddInMemoryCollection(second).Build();
            config["key"].Should().Be("bar");
        }
    }
}