using System.Threading.Tasks;
using DotNetCoreWCF.Host.Configuration;
using DotNetCoreWCF.Host.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace DotNetCoreWCF.Host
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var builder = new HostBuilder()
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddJsonFile("appsettings.json", optional: true);
					config.AddEnvironmentVariables();

					if (args != null)
					{
						config.AddCommandLine(args);
					}
				})
				.UseUnityServiceProvider()
				.ConfigureServices((hostContext, services) =>
				{
					services.AddOptions();
					services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

					services.AddSingleton<IHostedService, GrpcServiceHost>();
					services.AddSingleton<IHostedService, EmployeeServiceManager>();
				})
				.ConfigureContainer<IUnityContainer>(c =>
				{
					c.RegisterHostedServices();
				});

			await builder.RunConsoleAsync();
		}
	}
}