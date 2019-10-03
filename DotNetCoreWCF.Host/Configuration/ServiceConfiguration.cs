using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Data.Store;
using DotNetCoreWCF.Host.Services;
using Unity;
using Unity.Lifetime;

namespace DotNetCoreWCF.Host.Configuration
{
	public static class ServiceConfiguration
	{
		public static IUnityContainer ConfigureServices(this IUnityContainer container)
		{
			container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());
			container.RegisterType<IEmployeeStore, EmployeeStore>(new SingletonLifetimeManager());

			return container;
		}
	}
}
