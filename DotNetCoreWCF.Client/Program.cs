using System;
using System.IO;
using DotNetCoreWCF.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace DotNetCoreWCF.Client
{
	class Program
	{
		static void Main(string[] args)
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

				//var container = new Unity.UnityContainer();
				//container.RegisterType<Application>();
				//container
				//	.RegisterEmployeeService();
				//var application = container.Resolve<Application>();

				var services = new ServiceCollection();
				services.AddTransient<Application>();
				services
					.ConfigureLogging()
					.RegisterEmployeeService(configuration);

				var provider = services.BuildServiceProvider();

				var application = provider.GetService<Application>();
				application.Run();
			}
			finally
			{
				Console.WriteLine("\r\nPress any key to exit...");
				Console.ReadKey();
			}
		}
	}
}
