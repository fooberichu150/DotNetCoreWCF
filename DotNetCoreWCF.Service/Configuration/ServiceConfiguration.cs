using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Host.Services;
using DotNetCoreWCF.Service.Core.Configuration;
using DotNetCoreWCF.Service.Core.Store;
using Unity;
using Unity.Lifetime;

namespace DotNetCoreWCF.Host.Configuration
{
	public static class ServiceConfiguration
	{
		public static IUnityContainer ConfigureServices(this IUnityContainer container)
		{
			container
				.RegisterServices();

			container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());

			return container;
		}
	}
}
