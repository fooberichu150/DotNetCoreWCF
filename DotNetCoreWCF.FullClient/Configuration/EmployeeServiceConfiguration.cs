using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Model.Configuration;
using DotNetCoreWCF.Proxies.Factories;
using Unity;

namespace DotNetCoreWCF.Client.Configuration
{
	public static class EmployeeServiceConfiguration
	{
		public static IUnityContainer RegisterEmployeeService(this IUnityContainer container)
		{
			container.RegisterSingleton<IClientProxySettings, DefaultClientProxySettings>();
			container.RegisterSingleton<IEmployeeClientProxyFactory, EmployeeClientProxyFactory>();

			container.RegisterType<EmployeeClientDemo>();

			return container;
		}
	}
}
