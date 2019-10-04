using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Service.Core.Handlers;
using DotNetCoreWCF.Service.Core.Store;
using Microsoft.Extensions.DependencyInjection;
using Unity;
using Unity.Lifetime;

namespace DotNetCoreWCF.Service.Core.Configuration
{
	public static class Configurator
	{
		public static IServiceCollection RegisterHandlers(this IServiceCollection services)
		{
			services.AddTransient<IDeleteEmployeeRequestHandler, DeleteEmployeeRequestHandler>();
			services.AddTransient<IGetEmployeeRequestHandler, GetEmployeeRequestHandler>();
			services.AddTransient<IUpdateEmployeeRequestHandler, UpdateEmployeeRequestHandler>();

			services.AddSingleton<IEmployeeStore, EmployeeStore>();

			return services;
		}

		public static IUnityContainer RegisterHandlers(this IUnityContainer container)
		{
			container.RegisterType<IDeleteEmployeeRequestHandler, DeleteEmployeeRequestHandler>(new HierarchicalLifetimeManager());
			container.RegisterType<IGetEmployeeRequestHandler, GetEmployeeRequestHandler>(new HierarchicalLifetimeManager());
			container.RegisterType<IUpdateEmployeeRequestHandler, UpdateEmployeeRequestHandler>(new HierarchicalLifetimeManager());

			container.RegisterType<IEmployeeStore, EmployeeStore>(new SingletonLifetimeManager());

			return container;
		}
	}
}
