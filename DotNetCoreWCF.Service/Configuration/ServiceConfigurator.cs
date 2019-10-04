using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Host.Services;
using DotNetCoreWCF.Logic.Configuration;
using DotNetCoreWCF.Service.Core.Configuration;
using DotNetCoreWCF.Service.Core.Store;
using Unity;
using Unity.Lifetime;

namespace DotNetCoreWCF.Host.Configuration
{
	public static class ServiceConfigurator
	{
		public static IUnityContainer RegisterHostedServices(this IUnityContainer container)
		{
			container
				.ConfigureLogging()
				.RegisterAdapters()
				.RegisterHandlers();

			// used by Grpc
			container.RegisterSingleton<IGrpcServiceEndpointRegistry, GrpcServiceEndpointRegistry>();
			// used by WCF
			container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());

			return container;
		}
	}
}
