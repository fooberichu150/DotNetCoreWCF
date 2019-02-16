using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Client.Configuration.Settings;
using DotNetCoreWCF.Client.Model.Configuration;
using DotNetCoreWCF.Contracts.Model.Configuration;
using DotNetCoreWCF.Proxies.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace DotNetCoreWCF.Client.Configuration
{
	internal static class DemoConstants
	{
		public const string AutomaticConfigurationEmployeeClientDemo = "AutomaticConfigurationEmployeeClientDemo";
		public const string EmployeeClientDemo = "EmployeeClientDemo";
		public const string FactoryEmployeeClientDemo = "FactoryEmployeeClientDemo";
		public const string WcfGeneratedServiceDemo = "WcfGeneratedServiceDemo";
	} 

	public static class EmployeeServiceConfiguration
	{
		public static IServiceCollection RegisterEmployeeService(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ServiceSettings>(Settings.ServiceSettingConstants.EmployeeService,
				configuration.GetSection("Services:NetTcp:EmployeeService"));

			services.Configure<ServiceSettings>(configuration.GetSection("Services:NetTcp:Default"));

			services.AddSingleton<IEmployeeClientProxyFactory>(provider =>
			{
				var logger = provider.GetRequiredService<ILogger>();
				var options = provider.GetRequiredService<Microsoft.Extensions.Options.IOptionsSnapshot<ServiceSettings>>();
				var proxySettings = new ClientProxySettings(ServiceSettingConstants.EmployeeService, options);

				return new EmployeeClientProxyFactory(proxySettings, logger);
			});

			services.AddTransient<EmployeeClientDemo>();
			services.AddTransient<FactoryEmployeeClientDemo>();
			services.AddTransient<AutomaticConfigurationEmployeeClientDemo>();
			services.AddTransient<WcfGeneratedServiceDemo>();
			services.AddTransient<Func<string, IEmployeeClientDemo>>(provider => key =>
			{
				if (key == DemoConstants.EmployeeClientDemo)
					return provider.GetService<EmployeeClientDemo>();
				else if (key == DemoConstants.FactoryEmployeeClientDemo)
					return provider.GetService<FactoryEmployeeClientDemo>();
				else if (key == DemoConstants.AutomaticConfigurationEmployeeClientDemo)
					return provider.GetService<AutomaticConfigurationEmployeeClientDemo>();
				else if (key == DemoConstants.WcfGeneratedServiceDemo)
					return provider.GetService<WcfGeneratedServiceDemo>();

				throw new KeyNotFoundException();
			});

			return services;
		}
	}
}
