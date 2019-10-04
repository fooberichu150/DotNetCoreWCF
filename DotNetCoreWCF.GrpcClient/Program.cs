using System;
using System.IO;
using System.Threading.Tasks;
using DotNetCoreWCF.GrpcClient.Configuration;
using DotNetCoreWCF.Logic.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWCF.GrpcClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
				string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

				if (string.IsNullOrWhiteSpace(env))
				{
					env = "Development";
				}

				var builder = new ConfigurationBuilder()
								.SetBasePath(Directory.GetCurrentDirectory())
								.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
								.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
								.AddEnvironmentVariables();

				IConfigurationRoot configuration = builder.Build();

				var services = new ServiceCollection();

				services.AddTransient<Application>();
				services
					.RegisterAdapters()
					.ConfigureLogging()
					.RegisterEmployeeService();

				var provider = services.BuildServiceProvider();

				var application = provider.GetService<Application>();
				await application.Run();
			}
			finally
			{
				Console.WriteLine("\r\nPress any key to exit...");
				Console.ReadKey();
			}
		}
	}
}
