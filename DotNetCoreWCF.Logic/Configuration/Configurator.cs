using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DotNetCoreWCF.Logic.Adapters;
using Microsoft.Extensions.DependencyInjection;

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
	}
}
