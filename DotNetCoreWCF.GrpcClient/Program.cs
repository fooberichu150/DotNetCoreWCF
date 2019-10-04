using System;
using System.Threading.Tasks;
using DotNetCoreWCF.GrpcSample.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using DotNetCoreWCF.GrpcClient.Configuration;
using AutoMapper;
using DotNetCoreWCF.Logic.Configuration;

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
					.RegisterEmployeeService(configuration);

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
