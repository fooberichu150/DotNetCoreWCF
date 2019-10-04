using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DotNetCoreWCF.Logic.Adapters;
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace DotNetCoreWCF.Logic.Configuration
{
	public static class Configurator
	{
		public static IServiceCollection RegisterAdapters(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Configurator));
			services.AddSingleton<IDeleteEmployeeRequestAdapter, DeleteEmployeeRequestAdapter>();
			services.AddSingleton<IDeleteEmployeeResponseAdapter, DeleteEmployeeResponseAdapter>();
			services.AddSingleton<IEmployeeAdapter, EmployeeAdapter>();
			services.AddSingleton<IEmployeeRequestAdapter, EmployeeRequestAdapter>();
			services.AddSingleton<IEmployeeResponseAdapter, EmployeeResponseAdapter>();

			return services;
		}

		public static IUnityContainer RegisterAdapters(this IUnityContainer container)
		{
			var config = new MapperConfiguration(cfg => { cfg.AddProfile<Profiles.EmployeeProfile>(); });
			container.RegisterInstance(config.CreateMapper());
			container.RegisterSingleton<IDeleteEmployeeRequestAdapter, DeleteEmployeeRequestAdapter>();
			container.RegisterSingleton<IDeleteEmployeeResponseAdapter, DeleteEmployeeResponseAdapter>();
			container.RegisterSingleton<IEmployeeAdapter, EmployeeAdapter>();
			container.RegisterSingleton<IEmployeeRequestAdapter, EmployeeRequestAdapter>();
			container.RegisterSingleton<IEmployeeResponseAdapter, EmployeeResponseAdapter>();

			return container;
		}
	}
}
