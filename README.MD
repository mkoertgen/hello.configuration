# hello.configuration

Evaluating [aspnet/Configuration](https://github.com/aspnet/Configuration)

## Usage

Create a new dotnet project and install the following NuGet packages

    Install-Package Microsoft.Extensions.Configuration
    Install-Package Microsoft.Extensions.Configuration.CommandLine
    Install-Package Microsoft.Extensions.Configuration.EnvironmentVariables
    Install-Package Microsoft.Extensions.Configuration.Ini
    Install-Package Microsoft.Extensions.Configuration.Json
    Install-Package Microsoft.Extensions.Configuration.Xml

For the sample application, build

    build /v/m

and run with some customization

    set prefix_key2=42
	hello.configuration\bin\Release\hello.configuration.exe --key1=foobar

should give the following output

	Configuration:
	  key2 = 42
	  key1 = foobar
	  from = Jsinh
	  body2 =
	  body2:subject = Hello Universe
	  body =
	  body:subject = Hello world
	  Application =
	  Application:Width = 42
	  Application:Mode = SampleJsonValue
    
Note that `key2` has been overriden using the environment variable `prefix_key2` and 
`key1` has been overriden using the command line argument `--key1=foobar`

## References

- [aspnet/Configuration](https://github.com/aspnet/Configuration) (GitHub)
- [ASP.NET 5 Configuration - Microsoft Framework ConfigurationModel](https://blog.jsinh.in/asp-net-5-configuration-microsoft-framework-configurationmodel/) (Blogpost)